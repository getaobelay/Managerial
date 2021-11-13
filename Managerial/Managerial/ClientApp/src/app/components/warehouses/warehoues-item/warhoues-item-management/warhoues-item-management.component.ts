import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Warehouse } from 'src/app/models/warehouse/Warehouse.model';
import { WarehouseItem } from 'src/app/models/warehouse/WarehouseItem.model';
import { AccountService } from 'src/app/services/api/account.service';
import { AppTranslationService } from 'src/app/services/app/app-translation.service';
import { Utilities } from 'src/app/services/app/utilities';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/notification/alert.service';
import { WarehosueItemEditorComponent } from '../warehosue-item-editor/warehosue-item-editor.component';
import { WarehouseItemService } from '../warehouse-item-service.service';

@Component({
  selector: 'app-warhoues-item-management',
  templateUrl: './warhoues-item-management.component.html',
  styleUrls: ['./warhoues-item-management.component.scss']
})
export class WarhouesItemManagementComponent implements OnInit {

  columns: any[] = [];
  rows: WarehouseItem[] = [];
  rowsCache: WarehouseItem[] = [];
  editedWarehouseItem: WarehouseItem;
  sourceWarehouseItem: WarehouseItem;
  editingWarehouseItemName: { name: string };
  loadingIndicator: boolean;
  selectedWarehouseItem: WarehouseItem;
  selectedWarehouse: Warehouse;
  selectedLocation: Location;

  gT = (key: string) => this.translationService.getTranslation(key);

  @ViewChild('indexTemplate', { static: true })
  indexTemplate: TemplateRef<any>;

  @ViewChild('isActiveTemplate', { static: true })
  isActiveTemplate: TemplateRef<any>;

  @ViewChild('createdByTemplate', { static: true })
  createdByTemplate: TemplateRef<any>;

  @ViewChild('updatedByTemplate', { static: true })
  updatedByTemplate: TemplateRef<any>;

  @ViewChild('createdDateTemplate', { static: true })
  createdDateTemplate: TemplateRef<any>;

  @ViewChild('updatedDateTemplate', { static: true })
  updatedDateTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate', { static: true })
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal', { static: true })
  editorModal: ModalDirective;

  @ViewChild('WarehosueItemEditor', { static: true })
  warhouseItemEditor: WarehosueItemEditorComponent;

  constructor(private alertService: AlertService, private translationService: AppTranslationService,
    private warehouseItemService: WarehouseItemService,
    private accountService: AccountService) {
  }

  ngOnInit() {
      const gT = (key: string) => this.translationService.getTranslation(key);

      this.columns = [
        { prop: 'id', name: '#', width: 60, cellTemplate: this.indexTemplate, canAutoResize: false },
        { prop: 'productName', name: gT('warehouseItem.management.productName'), width: 90 },
        { prop: 'warehouseName', name:  gT('warehouseItem.management.warehouseName'), width: 90 },
        { prop: 'location', name: gT('warehouseItem.management.Location'), width: 90 },
        { prop: 'sellingPrice', name: gT('warehouseItem.management.Sell'), width: 90 },
        { prop: 'buyingPrice', name: gT('warehouseItem.management.Buy'), width: 90 },
        { prop: 'createdBy', name: gT('common.CreatedBy'), width: 30, cellTemplate: this.createdByTemplate},
        { prop: 'updatedBy', name: gT('common.UpdatedBy'), width: 30,cellTemplate: this.updatedByTemplate},
        { prop: 'createdDate', name: gT('common.CreatedDate'), width: 30, cellTemplate: this.createdDateTemplate},
        { prop: 'updatedDate', name: gT('common.UpdatedDate'), width: 30,cellTemplate: this.updatedDateTemplate},
      ];


      this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

      this.loadData();
  }

  ngAfterViewInit() {
    this.warhouseItemEditor.changesSavedCallback = () => {
      this.AddNewWarehouseItemToList();
      this.editorModal.hide();
    };

    this.warhouseItemEditor.changesCancelledCallback = () => {
      this.editedWarehouseItem = null;
      this.sourceWarehouseItem = null;
      this.editorModal.hide();
    };
  }

  AddNewWarehouseItemToList() {

        if (this.sourceWarehouseItem) {
      Object.assign(this.sourceWarehouseItem, this.editedWarehouseItem);

      let sourceIndex = this.rowsCache.indexOf(this.sourceWarehouseItem, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.sourceWarehouseItem, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.sourceWarehouseItem = null;
      this.sourceWarehouseItem = null;
    } else {
      const product = new WarehouseItem();
      Object.assign(product, this.editedWarehouseItem);
      this.editedWarehouseItem = null;

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

    this.warehouseItemService.getAll<WarehouseItem>()
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        const warehouseItems = results;
        console.log(warehouseItems)
        this.rowsCache = [...warehouseItems];
        this.rows = warehouseItems;
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage(this.gT('warehouseItems.alerts.LoadError') ,this.gT('warehouseItems.alerts.RetrieveError') + `: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }

  onSearchChanged(value: string) {
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.ProductName, r.Location));
  }

  onEditorModalHidden() {
    this.editingWarehouseItemName = null;
    this.warhouseItemEditor.resetForm(true);
  }

  newWarehouseItem() {
    this.editingWarehouseItemName = null;
    this.sourceWarehouseItem = null;
    this.editedWarehouseItem = this.warhouseItemEditor.newWarehouseItem();
    this.editorModal.show();
  }

  editWarehouseItem(row: WarehouseItem) {
    this.editingWarehouseItemName = { name: row.id.toString() };
    this.sourceWarehouseItem = row;
    this.editedWarehouseItem = this.warhouseItemEditor.editWarehouseItem(row);
    this.editorModal.show();
  }

  deleteWarehouseItem(row: WarehouseItem) {

    this.alertService.showDialog(this.gT('warehouseItems.alerts.Delete') + '\"' + row.id + '\"' + this.gT('warehouseItems.alerts.WarehouseItem'),
      DialogType.confirm, () => this.deleteWarehouseItemHelper (row));
  }
a
  deleteWarehouseItemHelper (row: WarehouseItem) {
    this.alertService.startLoadingMessage(this.gT('warehouseItems.alerts.Deleting'));
    this.loadingIndicator = true;

    this.warehouseItemService.delete(row.id)
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        this.rowsCache = this.rowsCache.filter(item => item !== row);
        this.rows = this.rows.filter(item => item !== row);
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage(this.gT('warehouseItems.alerts.Deleting'), this.gT("warehouseItems.alerts.ErrorOccured") + `: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }


}
