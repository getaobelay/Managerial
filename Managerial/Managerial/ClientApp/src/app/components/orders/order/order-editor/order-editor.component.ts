import { Component, OnInit, ViewChild } from '@angular/core';
import { Order } from 'src/app/models/order/Order.model';
import { OrderDetail } from 'src/app/models/order/OrderDetail.model';
import { WarehouseItem } from 'src/app/models/warehouse/WarehouseItem.model';
import { Utilities } from 'src/app/services/app/utilities';
import { AlertService, MessageSeverity } from 'src/app/services/notification/alert.service';
import { OrderService } from '../../order.service';

@Component({
  selector: 'app-order-editor',
  templateUrl: './order-editor.component.html',
  styleUrls: ['./order-editor.component.scss']
})
export class OrderEditorComponent implements OnInit {
  private isNewOrder = false;
  public isSaving: boolean;
  public showValidationErrors = true;
  public order: Order = new Order();
  public selectedValues: { [key: string]: boolean; } = {};
  private editingOrderName: string;
  public allWarehouseItems: WarehouseItem[] = [];
  public formResetToggle = true;
  public addedOrderDetial: OrderDetail;
  public allOrderDetails: OrderDetail[] = [];

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @ViewChild('f')
  private form;

  constructor(private alertService: AlertService, private orderService: OrderService) {
  }
  ngOnInit(): void {
      this.loadAllDropDowns();
  }


  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }



  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');

    if (this.isNewOrder) {
      this.orderService.post<Order>(this.order).subscribe((item: Order) =>
        this.saveSuccessHelper(item), error => this.saveFailedHelper(error));
    } else {
      this.orderService.put(this.order).subscribe(() =>
        this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(item?: Order) {
    if (item) {
      Object.assign(this.order, item);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.showValidationErrors = false;

    if (this.isNewOrder) {
      this.alertService.showMessage('Success', `item \"${this.order.id}\" was created successfully`, MessageSeverity.success);
    } else {
      this.alertService.showMessage('Success', `Changes to item \"${this.order.id}\" was saved successfully`, MessageSeverity.success);
    }

    this.order = new Order();
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
    this.order = new Order();

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

  newOrder() {
    this.isNewOrder = true;
    this.showValidationErrors = true;

    this.editingOrderName = null;
    this.selectedValues = {};
    this.order = new Order();

    return this.order;
  }

  editOrder(item: Order) {
    if (item) {
      this.isNewOrder = false;
      this.showValidationErrors = true;

      this.editingOrderName = item.id.toString();
      this.selectedValues = {};
      this.order = new Order();
      Object.assign(this.order, item);

      return this.order;
    } else {
      return this.newOrder();
    }
  }

  private loadAllDropDowns() {
    this.alertService.startLoadingMessage();

      this.orderService.loadWarehouseItemEditor().subscribe(results => {
        return this.onLoadAllDropDownsDataLoadSuccessful(results[0]);
      }, error => this.onLoadAllDropDownsDataLoadFailed(error));
  }

  private onLoadAllDropDownsDataLoadSuccessful(warehouseItems: WarehouseItem[]) {
    this.alertService.stopLoadingMessage();
    this.allWarehouseItems = [...warehouseItems];
  }

  private onLoadAllDropDownsDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
      MessageSeverity.error, error);

    this.allWarehouseItems = [];
  }




}
