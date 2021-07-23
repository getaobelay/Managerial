

import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { Category } from 'src/app/models/Category.model';
import { Product } from 'src/app/models/Product.model';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { Utilities } from 'src/app/services/utilities';
import { ProductService } from 'src/app/services/generic/product.service';
import { AccountService } from 'src/app/services/account.service';



@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss']
})
export class ProductInfoComponent implements OnInit {
  public isEditMode = false;
  public isNewProduct = false;
  public isSaving = false;
  public isChangePassword = false;
  public isEditingSelf = false;
  public showValidationErrors = false;
  public uniqueId: string = Utilities.uniqueId();
  public product: Product = new Product();
  public productEdit: Product;
  public allCategories: Category[] = [];

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @Input()
  isViewOnly: boolean;

  @Input()
  isGeneralEditor = false;

  @ViewChild('f')
  public form;

  // ViewChilds Required because ngIf hides template variables from global scope


  @ViewChild('name')
  public name;

  @ViewChild('sellingPrice')
  public sellingPrice;

  @ViewChild('buyingPrice')
  public buyingPrice;

  @ViewChild('measurement')
  public measurement;

  @ViewChild('weight')
  public weight;

  @ViewChild('description')
  public description;

  @ViewChild('height')
  public height;

  @ViewChild('categories')
  public categories;

  constructor(private alertService: AlertService, private productService: ProductService, private accountService: AccountService) {
    this.productService.endPointUrl = "products";
  }

  ngOnInit() {
    if (!this.isGeneralEditor) {
      this.loadCurrentproductData();
    }
  }

  private loadCurrentproductData() {
    this.alertService.startLoadingMessage();

      this.productService.getProductAndCategories().subscribe(results =>
        this.onCurrentProductDataLoadSuccessful(results[0], results[1]),
         error => this.onCurrentProductDataLoadFailed(error));
    }

  private onCurrentProductDataLoadSuccessful(product: Product, categories: Category[]) {
    this.alertService.stopLoadingMessage();
    this.product = product;
    this.allCategories = categories;
  }

  private onCurrentProductDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', `Unable to retrieve product data from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
      MessageSeverity.error, error);

    this.product = new Product();
  }

  getCategoryByName(name: string) {
    return this.allCategories.find((r) => r.name === name);
  }

  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }


  edit() {
    if (!this.isGeneralEditor) {
      this.isEditingSelf = true;
      this.productEdit = new Product();
      Object.assign(this.productEdit, this.product);
    } else {
      if (!this.productEdit) {
        this.productEdit = new Product();
      }
    }

    this.isEditMode = true;
    this.showValidationErrors = true;
    this.isChangePassword = false;
  }

  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');

    if (this.isNewProduct) {
      this.productService.post<Product>(this.productEdit)
      .subscribe(product =>
        this.saveSuccessHelper(product), error =>
          this.saveFailedHelper(error));
    } else {
      this.productService.put(this.productEdit).subscribe(response => this.saveSuccessHelper(),
       error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(prouduct?: Product) {
    this.testIsProductCategoryCountChanged(this.product, this.productEdit);

    if (this.product) {
      Object.assign(this.productEdit, this.product);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.isChangePassword = false;
    this.showValidationErrors = false;

    Object.assign(this.product, this.productEdit);
    this.productEdit = new Product();
    this.resetForm();

    if (this.isGeneralEditor) {
      if (this.isNewProduct) {
        this.alertService.showMessage('Success', `Product \"${this.product.name}\" was created successfully`, MessageSeverity.success);
      } else if (!this.isEditingSelf) {
        this.alertService.showMessage('Success', `Changes to product \"${this.product.name}\" was saved successfully`, MessageSeverity.success);
      }
    }

    this.isEditMode = false;

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

  private testIsProductCategoryCountChanged(currentProduct: Product, editedProduct: Product) {
    const categoriesAdded = this.isNewProduct ? editedProduct.categories : editedProduct.categories.filter(category => currentProduct.categories.indexOf(category) === -1);
    const categoriesRemoved = this.isNewProduct ? [] : currentProduct.categories.filter(category => editedProduct.categories.indexOf(category) === -1);

    const modifiedCategoreis = categoriesAdded.concat(categoriesRemoved);

    if (modifiedCategoreis.length) {
      setTimeout(() => this.productService.onCategoryCountChanged(modifiedCategoreis));
    }
  }

  cancel() {
    if (this.isGeneralEditor) {
      this.productEdit = this.product = new Product();
    } else {
      this.productEdit = new Product();
    }

    this.showValidationErrors = false;
    this.resetForm();

    this.alertService.showMessage('Cancelled', 'Operation cancelled by product', MessageSeverity.default);
    this.alertService.resetStickyMessage();

    if (!this.isGeneralEditor) {
      this.isEditMode = false;
    }

    if (this.changesCancelledCallback) {
      this.changesCancelledCallback();
    }
  }

  close() {
    this.productEdit = this.product = new Product();
    this.showValidationErrors = false;
    this.resetForm();
    this.isEditMode = false;

    if (this.changesSavedCallback) {
      this.changesSavedCallback();
    }
  }

  private refreshLoggedInUser() {
    this.accountService.refreshLoggedInUser()
      .subscribe(user => {
        this.loadCurrentproductData();
      },
        error => {
          this.alertService.resetStickyMessage();
          this.alertService.showStickyMessage('Refresh failed', 'An error occured whilst refreshing logged in user information from the server', MessageSeverity.error, error);
        });
  }


  resetForm(replace = false) {
    this.isChangePassword = false;

    if (!replace) {
      this.form.reset();
    } else {
      this.formResetToggle = false;

      setTimeout(() => {
        this.formResetToggle = true;
      });
    }
  }

  newCategory(allCategories: Category[]) {
    this.isGeneralEditor = true;
    this.isNewProduct = true;

    this.allCategories = [...allCategories];
    this.product = this.productEdit = new Product();
    this.product.isActive= true;
    this.edit();

    return this.productEdit;
  }

  editProduct(product: Product, allCategoreis: Category[]) {
    if (product) {
      this.isGeneralEditor = true;
      this.isNewProduct = false;

      this.setCategories(product, allCategoreis);
      this.product = new Product();
      this.productEdit = new Product();
      Object.assign(this.product, product);
      Object.assign(this.productEdit, product);
      this.edit();

      return this.productEdit;
    } else {
      return this.newCategory(allCategoreis);
    }
  }

  displayproduct(product: Product, allCategories?: Category[]) {
    this.product = new Product();
    Object.assign(this.product, this.product);
    this.setCategories(product, allCategories);

    this.isEditMode = false;
  }

  private setCategories(product: Product, allCategories?: Category[]) {
    this.allCategories = allCategories ? [...allCategories] : [];

    if (product.categories) {
      for (const pc of product.categories) {
        if (!this.allCategories.some(r => r.name === pc.name)) {
          this.allCategories.unshift(new Category(pc.id, pc.name, pc.description));
        }
      }
    }
  }

}
