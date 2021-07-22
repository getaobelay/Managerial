import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/models/Product.model';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/alert.service';
import { AppTranslationService } from 'src/app/services/app-translation.service';
import { ProductService } from 'src/app/services/product/product.service';
import { Utilities } from 'src/app/services/utilities';
import { ProductFormComponent } from './product-form/product-form.component';

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
  ProductEditor: ProductFormComponent;

  constructor(private alertService: AlertService, private translationService: AppTranslationService, private productService: ProductService) {
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
    this.ProductEditor.changesSavedCallback = () => {
      this.AddNewProduct();
      this.editorModal.hide();
    };

    this.ProductEditor.changesCancelledCallback = () => {
      this.editedProduct = null;
      this.sourceProduct = null;
      this.editorModal.hide();
    };
  }

  AddNewProduct() {
    if (this.sourceProduct) {
      Object.assign(this.sourceProduct, this.editedProduct);

      let sourceIndex = this.rowsCache.indexOf(this.sourceProduct, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.editedProduct, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.editedProduct = null;
      this.editedProduct = null;
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

    this.productService.getProducts()
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        const products = results;

        this.rowsCache = [...products];
        this.rows = products;
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Load Error', `Unable to retrieve roles from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }

  onSearchChanged(value: string) {
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.name, r.description));
  }

  onEditorModalHidden() {
    this.editingProductName = null;
    this.ProductEditor.resetForm(true);
  }

  newProduct() {
    this.editingProductName = null;
    this.sourceProduct = null;
    this.editedProduct = this.ProductEditor.newProduct();
    this.editorModal.show();
  }

  editProduct(row: Product) {
    this.editingProductName = { name: row.name };
    this.sourceProduct = row;
    this.editedProduct = this.ProductEditor.editProduct(row);
    this.editorModal.show();
  }

  deleteProduct(row: any) {
    this.alertService.showDialog('Are you sure you want to delete the \"' + row.name + '\" product?',
      DialogType.confirm, () => this.deleteProductHelper(row));
  }

  deleteProductHelper(row: any) {
    this.alertService.startLoadingMessage('Deleting...');
    this.loadingIndicator = true;

    this.productService.deleteProduct(row.id)
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        this.rowsCache = this.rowsCache.filter(item => item !== row);
        this.rows = this.rows.filter(item => item !== row);
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Delete Error', `An error occured whilst deleting the deleteProduct.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }
}
