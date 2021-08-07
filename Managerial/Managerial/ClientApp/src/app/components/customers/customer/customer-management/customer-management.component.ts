import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Customer } from 'src/app/models/order/Customer.model';
import { AccountService } from 'src/app/services/api/account.service';
import { AppTranslationService } from 'src/app/services/app/app-translation.service';
import { Utilities } from 'src/app/services/app/utilities';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/notification/alert.service';
import { CustomersService } from '../../customers-service.service';
import { CustomerEditorComponent } from '../customer-editor/customer-editor.component';

@Component({
  selector: 'app-customer-management',
  templateUrl: './customer-management.component.html',
  styleUrls: ['./customer-management.component.scss']
})
export class CustomerManagementComponent implements OnInit {

  columns: any[] = [];
  rows: Customer[] = [];
  rowsCache: Customer[] = [];
  editedCustomer: Customer;
  sourceCustomer: Customer;
  editingCustomerName: { name: string };
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

  @ViewChild('customerEditor', { static: true })
  customerEditor: CustomerEditorComponent;

  constructor(private alertService: AlertService, private translationService: AppTranslationService,
    private customerService: CustomersService,
    private accountService: AccountService) {
  }

  ngOnInit() {
      const gT = (key: string) => this.translationService.getTranslation(key);

      this.columns = [
        { prop: 'id', name:'#', width: 60, cellTemplate: this.indexTemplate, canAutoResize: false },
        { prop: 'name', name: gT('products.editor.Name'), width: 90 },
        { prop: 'email', name: 'Email', width: 90 },
        { prop: 'phoneNumber', name: 'Phone Number', width: 90 },
        { prop: 'address', name: 'Address', width: 90 },
        { prop: 'city', name: 'City', width: 90 },

      ];

      this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

      this.loadData();
  }

  ngAfterViewInit() {
    this.customerEditor.changesSavedCallback = () => {
      this.AddNewCustomerItemToList();
      this.editorModal.hide();
    };

    this.customerEditor.changesCancelledCallback = () => {
      this.editedCustomer = null;
      this.sourceCustomer = null;
      this.editorModal.hide();
    };
  }

  AddNewCustomerItemToList() {

        if (this.sourceCustomer) {
      Object.assign(this.sourceCustomer, this.editedCustomer);

      let sourceIndex = this.rowsCache.indexOf(this.sourceCustomer, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.sourceCustomer, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.sourceCustomer = null;
      this.sourceCustomer = null;
    } else {
      const product = new Customer();
      Object.assign(product, this.editedCustomer);
      this.editedCustomer = null;

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

    this.customerService.getAll<Customer>()
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
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.Name, r.Address, r.PhoneNumber));
  }

  onEditorModalHidden() {
    this.editingCustomerName = null;
    this.customerEditor.resetForm(true);
  }

  newProduct() {
    this.editingCustomerName = null;
    this.sourceCustomer = null;
    this.editedCustomer = this.customerEditor.newCustomer();
    this.editorModal.show();
  }

  editProduct(row: Customer) {
    this.editingCustomerName = { name: row.id.toString() };
    this.sourceCustomer = row;
    this.editedCustomer = this.customerEditor.editCustomer(row);
    this.editorModal.show();
  }

  deleteProduct(row: Customer) {

    this.alertService.showDialog(this.gT('products.alerts.Delete') + '\"' + row.id + '\"' + this.gT('products.alerts.Customer'),
      DialogType.confirm, () => this.deleteWarehouseItemHelper (row));
  }
a
  deleteWarehouseItemHelper (row: Customer) {
    this.alertService.startLoadingMessage(this.gT('products.alerts.Deleting'));
    this.loadingIndicator = true;

    this.customerService.delete(row.id)
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
