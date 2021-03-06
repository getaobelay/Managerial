// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Component, ViewChild } from '@angular/core';
import { Product } from 'src/app/models/product/Product.model';
import { AccountService } from 'src/app/services/api/account.service';
import { AlertService, MessageSeverity } from 'src/app/services/notification/alert.service';
import { Category } from 'src/app/models/product/Category.model';
import { ProductService } from '../../product.service';

@Component({
    selector: 'app-product-editor',
    templateUrl: './product-editor.component.html',
    styleUrls: ['./product-editor.component.scss']
})
export class ProductEditorComponent {
    private isNewProduct = false;
    public isSaving: boolean;
    public showValidationErrors = true;
    public productEdit: Product = new Product();
    public product: Product = new Product();
    public allCategories: Category[] = [];
    public selectedValues: { [key: string]: boolean; } = {};
    private editingProductName: string;

    public formResetToggle = true;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    @ViewChild('f')
    private form;

    constructor(private alertService: AlertService,
      private accountService: AccountService,
      private productService: ProductService) {
    }



  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }

  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');

    if (this.isNewProduct) {
      this.productService.post<Product>(this.productEdit).subscribe((product: Product) =>
        this.saveSuccessHelper(product), error => this.saveFailedHelper(error));
    } else {
      this.productService.put(this.productEdit).subscribe(() =>
        this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(product?: Product) {
    if (product) {
      Object.assign(this.productEdit, product);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.showValidationErrors = false;

    if (this.isNewProduct) {
      this.alertService.showMessage('Success', `product \"${this.productEdit.name}\" was created successfully`, MessageSeverity.success);
    } else {
      this.alertService.showMessage('Success', `Changes to product \"${this.productEdit.name}\" was saved successfully`, MessageSeverity.success);
    }

    this.productEdit = new Product();
    this.resetForm();

    if (this.changesSavedCallback) {
      this.changesSavedCallback();
    }
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Save Error', 'The below errors occured whilst saving your changes:', MessageSeverity.error, error);
    this.alertService.showStickyMessage(error, null, MessageSeverity.error);

    if (this.changesFailedCallback) {
      this.changesFailedCallback();
    }
  }

  cancel() {
    this.productEdit = new Product();

    this.showValidationErrors = false;
    this.resetForm();

    this.alertService.showMessage('Cancelled', 'Operation cancelled by user', MessageSeverity.default);
    this.alertService.resetStickyMessage();

    if (this.changesCancelledCallback) {
      this.changesCancelledCallback();
    }
  }

  resetForm(replace = false) {
    if (!replace) {
      this.form.reset();
    } else {
      this.formResetToggle = false;

      setTimeout(() => {
        this.formResetToggle = true;
      });
    }
  }

  newProduct() {
    this.isNewProduct = true;
    this.showValidationErrors = true;

    this.editingProductName = null;
    this.selectedValues = {};
    this.productEdit = new Product();

    return this.productEdit;
  }

  editProduct(product: Product) {
    if (product) {
      this.isNewProduct = false;
      this.showValidationErrors = true;

      this.editingProductName = product.name;
      this.selectedValues = {};
      this.productEdit = new Product();
      Object.assign(this.productEdit, product);

      return this.productEdit;
    } else {
      return this.newProduct();
    }
  }

}

