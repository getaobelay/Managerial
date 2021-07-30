import { Component, ViewChild } from '@angular/core';
import { AccountService } from 'src/app/services/api/account.service';
import { AlertService, MessageSeverity } from 'src/app/services/notification/alert.service';
import { Location } from 'src/app/models/warehouse/Warehouse.model'
import { LocationService } from '../location-service.service';
import { Category } from 'src/app/models/product/Category.model';

@Component({
  selector: 'app-location-editor',
  templateUrl: './location-editor.component.html',
  styleUrls: ['./location-editor.component.scss']
})
export class LocationEditorComponent {
    private isNewLocation = false;
    public isSaving: boolean;
    public showValidationErrors = true;
    public locationEdit: Location = new Location();
    public location: Location = new Location();
    public allCategories: Category[] = [];
    public selectedValues: { [key: string]: boolean; } = {};
    private editingLocationName: string;

    public formResetToggle = true;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    @ViewChild('f')
    private form;

    constructor(private alertService: AlertService,
      private accountService: AccountService,
       private locationService: LocationService) {
    }




  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }

  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');

    if (this.isNewLocation) {
      this.locationService.post<Location>(this.locationEdit).subscribe((location: Location) =>
        this.saveSuccessHelper(location), error => this.saveFailedHelper(error));
    } else {
      this.locationService.put(this.locationEdit).subscribe(() =>
        this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(location?: Location) {
    if (location) {
      Object.assign(this.locationEdit, location);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.showValidationErrors = false;

    if (this.isNewLocation) {
      this.alertService.showMessage('Success', `location \"${this.locationEdit.LocationRow}-\"${this.locationEdit.LocationColumn}-${this.locationEdit.LocationShelf} was created successfully`, MessageSeverity.success);
    } else {
      this.alertService.showMessage('Success', `Changes to location \"${this.locationEdit.LocationRow}-\"${this.locationEdit.LocationColumn}-${this.locationEdit.LocationShelf}\" was saved successfully`, MessageSeverity.success);
    }

    this.locationEdit = new Location();
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
    this.locationEdit = new Location();

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

  newLocation() {
    this.isNewLocation = true;
    this.showValidationErrors = true;

    this.editingLocationName = null;
    this.selectedValues = {};
    this.locationEdit = new Location();

    return this.locationEdit;
  }

  editLocation(location: Location) {
    if (location) {
      this.isNewLocation = false;
      this.showValidationErrors = true;

      this.editingLocationName = location.LocationRow;
      this.selectedValues = {};
      this.locationEdit = new Location();
      Object.assign(this.locationEdit, location);

      return this.locationEdit;
    } else {
      return this.newLocation();
    }
  }

}


