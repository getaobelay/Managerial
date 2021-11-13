import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from 'src/app/models/order/Customer.model';
import { AccountService } from 'src/app/services/api/account.service';
import { AlertService, MessageSeverity } from 'src/app/services/notification/alert.service';
import { CustomerService } from '../../customers-service.service';

@Component({
  selector: 'app-customer-editor',
  templateUrl: './customer-editor.component.html',
  styleUrls: ['./customer-editor.component.scss']
})
export class CustomerEditorComponent {
  private isNewCustomer = false;
  public isSaving: boolean;
  public showValidationErrors = true;
  public customerEdit: Customer = new Customer();
  public customer: Customer = new Customer();
  public selectedValues: { [key: string]: boolean; } = {};
  private editingCustomerName: string;

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @ViewChild('f')
  private form;

  constructor(private alertService: AlertService,
    private accountService: AccountService,
    private customerService: CustomerService) {
  }



showErrorAlert(caption: string, message: string) {
  this.alertService.showMessage(caption, message, MessageSeverity.error);
}

save() {
  this.isSaving = true;
  this.alertService.startLoadingMessage('Saving changes...');

  if (this.isNewCustomer) {
    this.customerService.post<Customer>(this.customerEdit).subscribe((customer: Customer) =>
      this.saveSuccessHelper(customer), error => this.saveFailedHelper(error));
  } else {
    this.customerService.put(this.customerEdit).subscribe(() =>
      this.saveSuccessHelper(), error => this.saveFailedHelper(error));
  }
}

private saveSuccessHelper(customer?: Customer) {
  if (customer) {
    Object.assign(this.customerEdit, customer);
  }

  this.isSaving = false;
  this.alertService.stopLoadingMessage();
  this.showValidationErrors = false;

  if (this.isNewCustomer) {
    this.alertService.showMessage('Success', `customer \"${this.customerEdit.Name}\" was created successfully`, MessageSeverity.success);
  } else {
    this.alertService.showMessage('Success', `Changes to customer \"${this.customerEdit.Name}\" was saved successfully`, MessageSeverity.success);
  }

  this.customerEdit = new Customer();
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
  this.customerEdit = new Customer();

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

  this.editingCustomerName = null;
  this.selectedValues = {};
  this.customerEdit = new Customer();

  return this.customerEdit;
}

editCustomer(customer: Customer) {
  if (customer) {
    this.isNewCustomer = false;
    this.showValidationErrors = true;

    this.editingCustomerName = customer.Name;
    this.selectedValues = {};
    this.customerEdit = new Customer();
    Object.assign(this.customerEdit, customer);

    return this.customerEdit;
  } else {
    return this.newCustomer();
  }
}
}
