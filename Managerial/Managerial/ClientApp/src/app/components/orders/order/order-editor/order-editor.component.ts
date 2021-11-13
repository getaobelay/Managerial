import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from 'src/app/models/order/Customer.model';
import { Order } from 'src/app/models/order/Order.model';
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
  columns: any[] = [];
  rows: Order[] = [];
  rowsCache: Order[] = [];


  private isNewOrder = false;
  public isSaving: boolean;
  public showValidationErrors = true;
  public orderEdit: Order = new Order();
  public order: Order = new Order();
  public allCustomers: Customer[] = [];
  public allWarehouseItems: WarehouseItem[] = [];

  private editingOrderName: string;

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @ViewChild('f')
  private form;

  constructor(private alertService: AlertService,
    private orderService: OrderService) {
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
    this.orderService.post<Order>(this.orderEdit).subscribe((warehouseItem: Order) =>
      this.saveSuccessHelper(warehouseItem), error => this.saveFailedHelper(error));
  } else {
    this.orderService.put(this.orderEdit).subscribe(() =>
      this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }
}

private saveSuccessHelper(order?: Order) {
  if (order) {
    Object.assign(this.orderEdit, order);
  }

  this.isSaving = false;
  this.alertService.stopLoadingMessage();
  this.showValidationErrors = false;

  if (this.isNewOrder) {
    this.alertService.showMessage('Success', `warehouseItem \"${this.orderEdit.id}\" was created successfully`, MessageSeverity.success);
  } else {
    this.alertService.showMessage('Success', `Changes to warehouseItem \"${this.orderEdit.id}\" was saved successfully`, MessageSeverity.success);
  }

  this.orderEdit = new Order();
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
  this.orderEdit = new Order();

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
  this.orderEdit = new Order();

  return this.orderEdit;
}

editOrder(warehouseItem: Order) {
  if (warehouseItem) {
    this.isNewOrder = false;
    this.showValidationErrors = true;
    this.orderEdit = new Order();
    Object.assign(this.orderEdit, warehouseItem);

    return this.orderEdit;
  } else {
    return this.newOrder();
  }
}


  private loadAllDropDowns() {
    this.alertService.startLoadingMessage();

      this.orderService.loadOrderEditor().subscribe(results => {
        return this.onLoadAllDropDownsDataLoadSuccessful(results[0], results[1]);
      }, error => this.onLoadAllDropDownsDataLoadFailed(error));
  }

  private onLoadAllDropDownsDataLoadSuccessful(warehouseItems: WarehouseItem[], customers: Customer[]) {
    this.alertService.stopLoadingMessage();
    this.allCustomers = [...customers];
    this.allWarehouseItems = [...warehouseItems];
  }

  private onLoadAllDropDownsDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
      MessageSeverity.error, error);

    this.allCustomers = [];
    this.allWarehouseItems = []
  }


  selectCustomerChange(value){
    this.orderEdit.Customer = this.allCustomers?.find(r => r.id === value)
  }

}
