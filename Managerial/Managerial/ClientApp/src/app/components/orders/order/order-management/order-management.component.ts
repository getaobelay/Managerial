import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Order } from 'src/app/models/order/Order.model';
import { AccountService } from 'src/app/services/api/account.service';
import { AppTranslationService } from 'src/app/services/app/app-translation.service';
import { Utilities } from 'src/app/services/app/utilities';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/notification/alert.service';
import { OrderService } from '../../order.service';
import { OrderEditorComponent } from '../order-editor/order-editor.component';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.scss']
})
export class OrderManagementComponent implements OnInit {

  columns: any[] = [];
  rows: Order[] = [];
  rowsCache: Order[] = [];
  editedOrder: Order;
  sourceOrder: Order;
  editingOrderName: { name: string };
  loadingIndicator: boolean;

  gT = (key: string) => this.translationService.getTranslation(key);

  @ViewChild('indexTemplate', { static: true })
  indexTemplate: TemplateRef<any>;

  @ViewChild('isActiveTemplate', { static: true })
  isActiveTemplate: TemplateRef<any>;

  @ViewChild('createdByTemplate', { static: true })
  createdByTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate', { static: true })
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal', { static: true })
  editorModal: ModalDirective;

  @ViewChild('orderEditor', { static: true })
  orderEditor: OrderEditorComponent;

  constructor(private alertService: AlertService, private translationService: AppTranslationService,
    private orderService: OrderService,
    private accountService: AccountService) {
  }

  ngOnInit() {
      const gT = (key: string) => this.translationService.getTranslation(key);

      this.columns = [
        { prop: 'id', name:'#', width: 60, cellTemplate: this.indexTemplate, canAutoResize: false },
        { prop: 'discount', name: "Discount", width: 90 },
        { prop: 'comments', name: 'Comments', width: 90 },
      ];

      this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

      this.loadData();
  }

  ngAfterViewInit() {
    this.orderEditor.changesSavedCallback = () => {
      this.AddNewOrderItemToList();
      this.editorModal.hide();
    };

    this.orderEditor.changesCancelledCallback = () => {
      this.editedOrder = null;
      this.sourceOrder = null;
      this.editorModal.hide();
    };
  }

  AddNewOrderItemToList() {

        if (this.sourceOrder) {
      Object.assign(this.sourceOrder, this.editedOrder);

      let sourceIndex = this.rowsCache.indexOf(this.sourceOrder, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.sourceOrder, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.sourceOrder = null;
      this.sourceOrder = null;
    } else {
      const product = new Order();
      Object.assign(product, this.editedOrder);
      this.editedOrder = null;

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

    this.orderService.getAll<Order>()
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
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.UpdatedBy, r.CreatedBy));
  }

  onEditorModalHidden() {
    this.editingOrderName = null;
    this.orderEditor.resetForm(true);
  }

  newOrder() {
    this.editingOrderName = null;
    this.sourceOrder = null;
    this.editedOrder = this.orderEditor.newOrder();
    this.editorModal.show();
  }

  editOrder(row: Order) {
    this.editingOrderName = { name: row.id.toString() };
    this.sourceOrder = row;
    this.editedOrder = this.orderEditor.editOrder(row);
    this.editorModal.show();
  }

  deleteOrder(row: Order) {

    this.alertService.showDialog(this.gT('products.alerts.Delete') + '\"' + row.id + '\"' + this.gT('products.alerts.Order'),
      DialogType.confirm, () => this.deleteOrdeHelper (row));
  }
a
  deleteOrdeHelper (row: Order) {
    this.alertService.startLoadingMessage(this.gT('products.alerts.Deleting'));
    this.loadingIndicator = true;

    this.orderService.delete(row.id)
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
