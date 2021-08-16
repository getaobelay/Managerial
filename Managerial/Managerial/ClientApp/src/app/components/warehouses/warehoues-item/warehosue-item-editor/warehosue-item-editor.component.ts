import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertService, MessageSeverity } from 'src/app/services/notification/alert.service';
import { WarehouseItem } from 'src/app/models/warehouse/WarehouseItem.model';
import { WarehouseService } from '../../warehouse-service.service';
import { Location } from 'src/app/models/warehouse/Location';
import { Warehouse } from 'src/app/models/warehouse/Warehouse.model';
import { Utilities } from 'src/app/services/app/utilities';
import { WarehouseItemService } from '../warehouse-item-service.service';
import { Product } from 'src/app/models/product/Product.model';

@Component({
  selector: 'app-warehosue-item-editor',
  templateUrl: './warehosue-item-editor.component.html',
  styleUrls: ['./warehosue-item-editor.component.scss']
})
export class WarehosueItemEditorComponent implements OnInit {

  private isNewWarehouseItem = false;
  public isSaving: boolean;
  public showValidationErrors = true;
  public warehouseItemEdit: WarehouseItem = new WarehouseItem();
  public warehouseItem: WarehouseItem = new WarehouseItem();
  public allProducts: Product[] = [];
  public allWarehouses: Warehouse[] = [];

  private editingWarehouseItemName: string;

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @ViewChild('f')
  private form;

  constructor(private alertService: AlertService,
    private warehouseService: WarehouseItemService) {
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

  if (this.isNewWarehouseItem) {
    this.warehouseService.post<WarehouseItem>(this.warehouseItemEdit).subscribe((warehouseItem: WarehouseItem) =>
      this.saveSuccessHelper(warehouseItem), error => this.saveFailedHelper(error));
  } else {
    this.warehouseService.put(this.warehouseItemEdit).subscribe(() =>
      this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }
}

private saveSuccessHelper(warehouseItem?: WarehouseItem) {
  if (warehouseItem) {
    Object.assign(this.warehouseItemEdit, warehouseItem);
  }

  this.isSaving = false;
  this.alertService.stopLoadingMessage();
  this.showValidationErrors = false;

  if (this.isNewWarehouseItem) {
    this.alertService.showMessage('Success', `warehouseItem \"${this.warehouseItemEdit.id}\" was created successfully`, MessageSeverity.success);
  } else {
    this.alertService.showMessage('Success', `Changes to warehouseItem \"${this.warehouseItemEdit.id}\" was saved successfully`, MessageSeverity.success);
  }

  this.warehouseItemEdit = new WarehouseItem();
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
  this.warehouseItemEdit = new WarehouseItem();

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

newWarehouseItem() {
  this.isNewWarehouseItem = true;
  this.showValidationErrors = true;

  this.editingWarehouseItemName = null;
  this.warehouseItemEdit = new WarehouseItem();

  return this.warehouseItemEdit;
}

editWarehouseItem(warehouseItem: WarehouseItem) {
  if (warehouseItem) {
    this.isNewWarehouseItem = false;
    this.showValidationErrors = true;
    this.warehouseItemEdit = new WarehouseItem();
    Object.assign(this.warehouseItemEdit, warehouseItem);

    return this.warehouseItemEdit;
  } else {
    return this.newWarehouseItem();
  }
}


  private loadAllDropDowns() {
    this.alertService.startLoadingMessage();

      this.warehouseService.loadWarehouseItemEditor().subscribe(results => {
        return this.onLoadAllDropDownsDataLoadSuccessful(results[0], results[1]);
      }, error => this.onLoadAllDropDownsDataLoadFailed(error));
  }

  private onLoadAllDropDownsDataLoadSuccessful(warehouseItems: Product[], warehouses: Warehouse[]) {
    this.alertService.stopLoadingMessage();
    this.allProducts = [...warehouseItems];
    this.allWarehouses = [...warehouses];
  }

  private onLoadAllDropDownsDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
      MessageSeverity.error, error);

    this.allProducts = [];
    this.allWarehouses = []
  }


  selectWarehouseChange(value){
    this.warehouseItemEdit.Warehouse = this.allWarehouses?.find(r => r.id === value)
  }

  selectProductChange(value){
    this.warehouseItemEdit.Product = this.allProducts?.find(r => r.id === value)
  }

}
