import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from 'src/app/models/order/Customer.model';
import { AlertService, MessageSeverity } from 'src/app/services/notification/alert.service';
import { CustomersService } from '../../customers-service.service';

@Component({
  selector: 'app-customer-editor',
  templateUrl: './customer-editor.component.html',
  styleUrls: ['./customer-editor.component.scss']
})
export class CustomerEditorComponent {

  private isNewCustomer = false;
  public isSaving: boolean;
  public showValidationErrors = true;
  public customer: Customer = new Customer();
  public selectedValues: { [key: string]: boolean; } = {};
  private editingCategoryName: string;

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @ViewChild('f')
  private form;

  constructor(private alertService: AlertService, private customerService: CustomersService) {
  }


  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }



  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');

    if (this.isNewCustomer) {
      this.customerService.post<Customer>(this.customer).subscribe((item: Customer) =>
        this.saveSuccessHelper(item), error => this.saveFailedHelper(error));
    } else {
      this.customerService.put(this.customer).subscribe(() =>
        this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(item?: Customer) {
    if (item) {
      Object.assign(this.customer, item);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.showValidationErrors = false;

    if (this.isNewCustomer) {
      this.alertService.showMessage('Success', `item \"${this.customer.id}\" was created successfully`, MessageSeverity.success);
    } else {
      this.alertService.showMessage('Success', `Changes to item \"${this.customer.id}\" was saved successfully`, MessageSeverity.success);
    }

    this.customer = new Customer();
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
    this.customer = new Customer();

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

  newCustomer() {
    this.isNewCustomer = true;
    this.showValidationErrors = true;

    this.editingCategoryName = null;
    this.selectedValues = {};
    this.customer = new Customer();

    return this.customer;
  }

  editCustomer(item: Customer) {
    if (item) {
      this.isNewCustomer = false;
      this.showValidationErrors = true;

      this.editingCategoryName = item.id.toString();
      this.selectedValues = {};
      this.customer = new Customer();
      Object.assign(this.customer, item);

      return this.customer;
    } else {
      return this.newCustomer();
    }
  }

}
