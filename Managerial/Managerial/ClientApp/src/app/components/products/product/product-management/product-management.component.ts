import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/models/product/Product.model';
import { AccountService } from 'src/app/services/api/account.service';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/notification/alert.service';
import { Utilities } from 'src/app/services/app/utilities';
import { ProductEditorComponent } from '../product-editor/product-editor.component';
import { AppTranslationService } from 'src/app/services/app/app-translation.service';
import { ProductService } from '../../product.service';

@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html',
  styleUrls: ['./product-management.component.scss']
})
export class ProductManagementComponent implements OnInit, AfterViewInit {
  columns: any[] = [];
  rows: Product[] = [];
  rowsCache: Product[] = [];
  editedProduct: Product;
  sourceProduct: Product;
  editingProductName: { name: string };
  loadingIndicator: boolean;

  gT = (key: string) => this.translationService.getTranslation(key);

  @ViewChild('indexTemplate', { static: true })
  indexTemplate: TemplateRef<any>;

  @ViewChild('selectTemplate', { static: true })
  selectTemplate: TemplateRef<any>;

  @ViewChild('isActiveTemplate', { static: true })
  isActiveTemplate: TemplateRef<any>;

  @ViewChild('updatedByTemplate', { static: true })
  updatedByTemplate: TemplateRef<any>;

  @ViewChild('createdByTemplate', { static: true })
  createdByTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate', { static: true })
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal', { static: true })
  editorModal: ModalDirective;

  @ViewChild('productEditor', { static: true })
  productEditor: ProductEditorComponent;

  constructor(private alertService: AlertService, private translationService: AppTranslationService,
    private productService: ProductService,
    private accountService: AccountService) {
  }

  ngOnInit() {
      const gT = (key: string) => this.translationService.getTranslation(key);

      this.columns = [
        { prop: '', name: '', width: 60, cellTemplate: this.selectTemplate, canAutoResize: false },
        { prop: 'id', name: 'Id', width: 60, cellTemplate: this.indexTemplate, canAutoResize: false },
        { prop: 'name', name: gT('products.editor.Name'), width: 90 },
        { prop: 'sellingPrice', name: gT('products.editor.Description'), width: 50 },
        { prop: 'sellingPrice', name: gT('products.editor.SellingPrice'), width: 50 },
        { prop: 'buyingPrice', name: gT('products.editor.BuyingPrice'), width: 50 },
        { prop: 'weight', name: gT('products.editor.Weight'), width: 50 },
        { prop: 'height', name: gT('products.editor.Height'), width: 50 },
        { prop: 'isActive', name: gT('products.editor.Status'), width: 50, cellTemplate: this.isActiveTemplate},
        { prop: 'createdBy', name: 'Created By', width: 30, cellTemplate: this.createdByTemplate},
        { prop: 'updatedBy', name: 'Updated By', width: 30,cellTemplate: this.updatedByTemplate},
      ];

          this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

      this.loadData();
  }

  ngAfterViewInit() {
    this.productEditor.changesSavedCallback = () => {
      this.AddNewProductToList();
      this.editorModal.hide();
    };

    this.productEditor.changesCancelledCallback = () => {
      this.editedProduct = null;
      this.sourceProduct = null;
      this.editorModal.hide();
    };
  }

  AddNewProductToList() {

        if (this.sourceProduct) {
      Object.assign(this.sourceProduct, this.editedProduct);

      let sourceIndex = this.rowsCache.indexOf(this.sourceProduct, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.sourceProduct, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.sourceProduct = null;
      this.sourceProduct = null;
    } else {
      const product = new Product();
      Object.assign(product, this.editedProduct);
      this.editedProduct = null;

      let maxIndex = 0;
      for (const r of this.rowsCache) {
        if ((r as any).index > maxIndex) {
          maxIndex = (r as any).index;
        }
      }

      (product as any).index = maxIndex + 1;

      this.rowsCache.splice(0, 0, product);
      this.rows.splice(0, 0, product);
      this.rows = [...this.rows];
    }
  }

  loadData() {
    this.alertService.startLoadingMessage();
    this.loadingIndicator = true;

    this.productService.getAll<Product>()
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        const products = results;
        console.log(products)
        this.rowsCache = [...products];
        this.rows = products;
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage(this.gT('products.alerts.LoadError') ,this.gT('products.alerts.RetrieveError') + `: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }

  onSearchChanged(value: string) {
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.name, r.description));
  }

  onEditorModalHidden() {
    this.editingProductName = null;
    this.productEditor.resetForm(true);
  }

  newProduct() {
    this.editingProductName = null;
    this.sourceProduct = null;
    this.editedProduct = this.productEditor.newProduct();
    this.editorModal.show();
  }

  editProduct(row: Product) {
    this.editingProductName = { name: row.name };
    this.sourceProduct = row;
    this.editedProduct = this.productEditor.editProduct(row);
    this.editorModal.show();
  }

  deleteProduct(row: Product) {

    this.alertService.showDialog(this.gT('products.alerts.Delete') + '\"' + row.id + '\" ' + this.gT('products.alerts.Product'),
      DialogType.confirm, () => this.deleteProductHelper(row));
  }
a
  deleteProductHelper(row: Product) {
    this.alertService.startLoadingMessage(this.gT('products.alerts.Deleting'));
    this.loadingIndicator = true;

    this.productService.delete(row.id)
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        this.rowsCache = this.rowsCache.filter(item => item !== row);
        this.rows = this.rows.filter(item => item !== row);
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage(this.gT('products.alerts.Deleting'), this.gT("products.alerts.ErrorOccured") + `: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }
}
