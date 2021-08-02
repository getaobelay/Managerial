import { Component, ViewChild } from "@angular/core";
import { Warehouse } from "src/app/models/warehouse/Warehouse.model";
import { APiService } from "src/app/services/generic/api.service";
import { AlertService, MessageSeverity } from "src/app/services/notification/alert.service";

@Component({
  selector: 'app-warehouse-editor',
  templateUrl: './warehouse-editor.component.html',
  styleUrls: ['./warehouse-editor.component.scss']
})
export class WarehouseEditorComponent {
  private isNewWarehouse = false;
  public isSaving: boolean;
  public showValidationErrors = true;
  public warehouseEdit: Warehouse = new Warehouse();
  public selectedValues: { [key: string]: boolean; } = {};
  private editingWarehouseName: string;
  public options: {} = {true: true, false: false}

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @ViewChild('f')
  private form;

  selectedOption: string;

  onChange: (_: any) => {};


  ngOnInit() {
    this.selectedOption = this.options[0];
  }

  writeValue(value: string) {
     this.selectedOption = value;
  }

  registerOnChange(fn: (_: any) => {}) {
     this.onChange = fn;
  }

  changeSelectedOption(option: string) {
     this.selectedOption = option;
     this.onChange(option);
  }

  registerOnTouched() { }


  constructor(private alertService: AlertService, private warehouseService: APiService) {
    this.warehouseService.endPointUrl = 'warehouses';
  }

  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }

  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');

    if (this.isNewWarehouse) {
      this.warehouseService.post<Warehouse>(this.warehouseEdit).subscribe((Warehouse: Warehouse) =>
        this.saveSuccessHelper(Warehouse), error => this.saveFailedHelper(error));
    } else {
      this.warehouseService.put(this.warehouseEdit).subscribe(() =>
        this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(warehouse?: Warehouse) {
    if (Warehouse) {
      Object.assign(this.warehouseEdit, warehouse);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.showValidationErrors = false;

    if (this.isNewWarehouse) {
      this.alertService.showMessage('Success', `Warehouse \"${this.warehouseEdit.name}\" was created successfully`, MessageSeverity.success);
    } else {
      this.alertService.showMessage('Success', `Changes to Warehouse \"${this.warehouseEdit.name}\" was saved successfully`, MessageSeverity.success);
    }

    this.warehouseEdit = new Warehouse();
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
    this.warehouseEdit = new Warehouse();

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

  newWarehouse() {
    this.isNewWarehouse = true;
    this.showValidationErrors = true;

    this.editingWarehouseName = null;
    this.selectedValues = {};
    this.warehouseEdit = new Warehouse();

    return this.warehouseEdit;
  }

  editWarehouse(warehouse: Warehouse) {
    if (warehouse) {
      this.isNewWarehouse = false;
      this.showValidationErrors = true;

      this.editingWarehouseName = Warehouse.name;
      this.selectedValues = {};
      this.warehouseEdit = new Warehouse();
      Object.assign(this.warehouseEdit, Warehouse);

      return this.warehouseEdit;
    } else {
      return this.newWarehouse();
    }
  }
}
