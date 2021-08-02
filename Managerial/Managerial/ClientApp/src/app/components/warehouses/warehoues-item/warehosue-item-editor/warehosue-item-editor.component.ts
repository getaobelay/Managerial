import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertService, MessageSeverity } from 'src/app/services/notification/alert.service';
import { WarehouseItem } from 'src/app/models/warehouse/WarehouseItem.model';
import { WarehouseService } from '../../warehouse-service.service';
import { Location } from 'src/app/models/warehouse/Location';
import { Product } from 'src/app/models/product/Product.model';
import { Warehouse } from 'src/app/models/warehouse/Warehouse.model';
import { Utilities } from 'src/app/services/app/utilities';
import { WarehouseItemService } from '../warehouse-item-service.service';

@Component({
  selector: 'app-warehosue-item-editor',
  templateUrl: './warehosue-item-editor.component.html',
  styleUrls: ['./warehosue-item-editor.component.scss']
})
export class WarehosueItemEditorComponent implements OnInit {

  private isNewWarehouseItem = false;
  public isSaving: boolean;
  public showValidationErrors = true;
  public warehouseItem: WarehouseItem = new WarehouseItem();
  public selectedValues: { [key: string]: boolean; } = {};
  private editingCategoryName: string;
  public allProducts: Product[] = [];
  public allWarehouses: Warehouse[] = [];
  public allLocations: Location[] = [];


  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @ViewChild('f')
  private form;

  constructor(private alertService: AlertService, private warehouseService: WarehouseItemService) {
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
      this.warehouseService.post<WarehouseItem>(this.warehouseItem).subscribe((item: WarehouseItem) =>
        this.saveSuccessHelper(item), error => this.saveFailedHelper(error));
    } else {
      this.warehouseService.put(this.warehouseItem).subscribe(() =>
        this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(item?: WarehouseItem) {
    if (item) {
      Object.assign(this.warehouseItem, item);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.showValidationErrors = false;

    if (this.isNewWarehouseItem) {
      this.alertService.showMessage('Success', `item \"${this.warehouseItem.id}\" was created successfully`, MessageSeverity.success);
    } else {
      this.alertService.showMessage('Success', `Changes to item \"${this.warehouseItem.id}\" was saved successfully`, MessageSeverity.success);
    }

    this.warehouseItem = new WarehouseItem();
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
    this.warehouseItem = new WarehouseItem();

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

    this.editingCategoryName = null;
    this.selectedValues = {};
    this.warehouseItem = new WarehouseItem();

    return this.warehouseItem;
  }

  editWarehouseItem(item: WarehouseItem) {
    if (item) {
      this.isNewWarehouseItem = false;
      this.showValidationErrors = true;

      this.editingCategoryName = item.id.toString();
      this.selectedValues = {};
      this.warehouseItem = new WarehouseItem();
      Object.assign(this.warehouseItem, item);

      return this.warehouseItem;
    } else {
      return this.newWarehouseItem();
    }
  }

  private loadAllDropDowns() {
    this.alertService.startLoadingMessage();

      this.warehouseService.loadWarehouseItemEditor().subscribe(results => {
        return this.onLoadAllDropDownsDataLoadSuccessful(results[0], results[1], results[2]);
      }, error => this.onLoadAllDropDownsDataLoadFailed(error));
  }

  private onLoadAllDropDownsDataLoadSuccessful(products: Product[], warehouses: Warehouse[], locations: Location[]) {
    this.alertService.stopLoadingMessage();
    this.allProducts = [...products];
    this.allWarehouses = [...warehouses];
    this.allLocations = [...locations];
  }

  private onLoadAllDropDownsDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
      MessageSeverity.error, error);

    this.allProducts = [];
    this.allLocations = [];
    this.allWarehouses = []
  }


  selectWarehouseChange(value){
    this.warehouseItem.Warehouse = this.allWarehouses?.find(r => r.name === value)
  }

  selectProductChange(value){
    this.warehouseItem.Product = this.allProducts?.find(r => r.name === value)
  }

}
