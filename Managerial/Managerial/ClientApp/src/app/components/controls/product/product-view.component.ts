import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Category } from 'src/app/models/Category.model';
import { Product } from 'src/app/models/Product.model';
import { AccountService } from 'src/app/services/account.service';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/alert.service';
import { AppTranslationService } from 'src/app/services/app-translation.service';
import { ProductService } from 'src/app/services/generic/product.service';
import { Utilities } from 'src/app/services/utilities';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductInfoComponent } from './product-info/product-info.component';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.scss']
})
export class ProductViewComponent implements OnInit, AfterViewInit {
  columns: any[] = [];
  rows: Product[] = [];
  rowsCache: Product[] = [];
  editedProduct: Product;
  sourceProduct: Product;
  editingProductName: { name: string };
  loadingIndicator: boolean;

  allCategories: Category[] = [];

  @ViewChild('indexTemplate', { static: true })
  indexTemplate: TemplateRef<any>;

  @ViewChild('productName', { static: true })
  ProductNameTemplate: TemplateRef<any>;

  @ViewChild('sellingPrice', { static: true })
  SellingPriceTemplate: TemplateRef<any>;

  @ViewChild('buyingPrice', { static: true })
  BuyingPriceTemplate: TemplateRef<any>;

  @ViewChild('measurement', { static: true })
  MeasurementTemplate: TemplateRef<any>;

  @ViewChild('weight', { static: true })
  WeightTemplate: TemplateRef<any>;

  @ViewChild('height', { static: true })
  HeightTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate', { static: true })
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal', { static: true })
  editorModal: ModalDirective;

  @ViewChild('productEditor', { static: true })
  productEditor: ProductInfoComponent;

  constructor(private alertService: AlertService, private translationService: AppTranslationService,
    private productService: ProductService,
    private accountService: AccountService) {
  }

  ngOnInit() {
      const gT = (key: string) => this.translationService.getTranslation(key);

      this.columns = [
        { prop: 'name', name: 'name', width: 50, cellTemplate: this.ProductNameTemplate },
        { prop: 'sellingPrice', name: 'Sell', width: 90, cellTemplate: this.SellingPriceTemplate },
        { prop: 'buyingPrice', name: 'Buy', width: 90, cellTemplate: this.BuyingPriceTemplate },
        { prop: 'measurement', name: 'Mesasurement', width: 120 },
        { prop: 'weight', name: 'Weight', width: 140, cellTemplate: this.WeightTemplate },
        { prop: 'height', name: 'Heigher', width: 120, cellTemplate: this.HeightTemplate },
      ];

          this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

      this.loadData();
  }

  ngAfterViewInit() {
      this.productEditor.changesSavedCallback = () => {
          this.addNewProductToList();
          this.editorModal.hide();
      };

      this.productEditor.changesCancelledCallback = () => {
          this.editedProduct = null;
          this.sourceProduct = null;
          this.editorModal.hide();
      };
  }

  addNewProductToList() {
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

          this.editedProduct = null;
          this.sourceProduct = null;
      } else {
          const product = new Product();
          Object.assign(product, this.editedProduct);
          this.editedProduct = null;

          let maxIndex = 0;
          for (const u of this.rowsCache) {
              if ((u as any).index > maxIndex) {
                  maxIndex = (u as any).index;
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

          this.productService.getProductsAndCategories().subscribe(results => this.onDataLoadSuccessful(results[0] , results[1]),
           error => this.onDataLoadFailed(error));

  }

  onDataLoadSuccessful(products: Product[], categories: Category[]) {
      this.alertService.stopLoadingMessage();
      this.loadingIndicator = false;

      products.forEach((product, index) => {
          (product as any).index = index + 1;
      });

      this.rowsCache = [...products];
      this.rows = products;

      this.allCategories = categories;
  }

  onDataLoadFailed(error: any) {
      this.alertService.stopLoadingMessage();
      this.loadingIndicator = false;

      this.alertService.showStickyMessage('Load Error', `Unable to retrieve products from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
          MessageSeverity.error, error);
  }

  onSearchChanged(value: string) {
      this.rows = this.rowsCache.filter(r =>
        Utilities.searchArray(value, false, r.name, r.quantityPerUnit, r.buyingPrice, r.sellingPrice, r.categories));
  }

  onEditorModalHidden() {
      this.editingProductName = null;
      this.productEditor.resetForm(true);
  }

  newProduct() {
      this.editingProductName = null;
      this.sourceProduct = null;
      this.editorModal.show();
  }

  editProduct(row: Product) {
      this.editingProductName = { name: row.name };
      this.sourceProduct = row;
      this.editedProduct = this.productEditor.editProduct(row, this.allCategories);
      this.editorModal.show();
  }

  deleteProduct(row: Product) {
      this.alertService.showDialog('Are you sure you want to delete \"' + row.name + '\"?', DialogType.confirm,
      () => this.deleteProductHelper(row));
  }

  deleteProductHelper(row: Product) {
      this.alertService.startLoadingMessage('Deleting...');
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

                  this.alertService.showStickyMessage('Delete Error', `An error occured whilst deleting the product.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`,
                      MessageSeverity.error, error);
              });
  }
}
