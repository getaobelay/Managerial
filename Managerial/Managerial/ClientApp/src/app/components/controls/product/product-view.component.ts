import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/models/Product.model';
import { AccountService } from 'src/app/services/account.service';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/alert.service';
import { AppTranslationService } from 'src/app/services/app-translation.service';
import { ProductService } from 'src/app/services/generic/product.service';
import { Utilities } from 'src/app/services/utilities';
import { ProductEditorComponent } from './product-editor/product-editor.component';

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

  @ViewChild('quantityPerUnit', { static: true })
  QuantityPerUnitTemplate: TemplateRef<any>;


  @ViewChild('isActive', { static: true })
  isActiveTemplate: TemplateRef<any>;

  @ViewChild('height', { static: true })
  HeightTemplate: TemplateRef<any>;

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
        { prop: 'name', name: 'Name', width: 90, cellTemplate: this.ProductNameTemplate },
        { prop: 'sellingPrice', name: 'Sell', width: 50, cellTemplate: this.SellingPriceTemplate },
        { prop: 'buyingPrice', name: 'Buy', width: 50, cellTemplate: this.BuyingPriceTemplate },
        { prop: 'measurement', name: 'Mesasurement', width: 30 , cellTemplate: this.MeasurementTemplate},
        { prop: 'quantityPerUnit', name: 'Quantity', width: 30 , cellTemplate: this.QuantityPerUnitTemplate},
        { prop: 'weight', name: 'Weight', width: 50, cellTemplate: this.WeightTemplate },
        { prop: 'height', name: 'Heighet', width: 50, cellTemplate: this.HeightTemplate },
        { prop: 'isActive', name: 'Active', width: 50, cellTemplate: this.isActiveTemplate },

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
    this.alertService.showDialog('Are you sure you want to delete the \"' + row.name + '\" product?',
      DialogType.confirm, () => this.deleteProductHelper(row));
  }
a
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

          this.alertService.showStickyMessage('Delete Error', `An error occured whilst deleting the deleteproduct.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }
}
