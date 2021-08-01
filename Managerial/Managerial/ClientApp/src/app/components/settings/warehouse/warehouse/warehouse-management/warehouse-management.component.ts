import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Warehouse } from 'src/app/models/warehouse/Warehouse.model';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/notification/alert.service';
import { APiService } from 'src/app/services/generic/api.service';
import { Utilities } from 'src/app/services/app/utilities';
import { WarehouseEditorComponent} from '../warehouse-editor/warehouse-editor.component';
import { AppTranslationService } from 'src/app/services/app/app-translation.service';

@Component({
  selector: 'app-warehouse-management',
  templateUrl: './warehouse-management.component.html',
  styleUrls: ['./warehouse-management.component.scss']
})
export class WarehouseManagementComponent implements OnInit, AfterViewInit {
  columns: any[] = [];
  rows: Warehouse[] = [];
  rowsCache: Warehouse[] = [];
  editedWarehouse: Warehouse;
  sourceWarehouse: Warehouse;
  editingWarehouseName: { name: string };
  loadingIndicator: boolean;

  @ViewChild('indexTemplate', { static: true })
  indexTemplate: TemplateRef<any>;

  @ViewChild('isActiveTemplate', { static: true })
  IsActiveTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate', { static: true })
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal', { static: true })
  editorModal: ModalDirective;

  @ViewChild('warehouseEditor', { static: true })
  warehouseEditor: WarehouseEditorComponent;

  constructor(private alertService: AlertService,
     private translationService: AppTranslationService,
     private warehouseService: APiService) {
      this.warehouseService.endPointUrl = 'warehouses';

  }

  ngOnInit() {
    const gT = (key: string) => this.translationService.getTranslation(key);

    this.columns = [
      { prop: 'id', name: '#', width: 60, cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'name', name: 'Name', width: 50 },
      { prop: 'type', name: 'Type', width: 90 },
      { prop: 'isActive', name: 'Status', width: 90, cellTemplate: this.IsActiveTemplate },
    ];

    this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

    this.loadData();
  }

  ngAfterViewInit() {
    this.warehouseEditor.changesSavedCallback = () => {
      this.AddNewWarehouse();
      this.editorModal.hide();
    };

    this.warehouseEditor.changesCancelledCallback = () => {
      this.editedWarehouse = null;
      this.sourceWarehouse = null;
      this.editorModal.hide();
    };
  }

  AddNewWarehouse() {
    if (this.sourceWarehouse) {
      Object.assign(this.sourceWarehouse, this.editedWarehouse);

      let sourceIndex = this.rowsCache.indexOf(this.sourceWarehouse, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.editedWarehouse, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.editedWarehouse = null;
      this.editedWarehouse = null;
    } else {
      const warehouse = new Warehouse();
      Object.assign(warehouse, this.editedWarehouse);
      this.editedWarehouse = null;

      let maxIndex = 0;
      for (const r of this.rowsCache) {
        if ((r as any).index > maxIndex) {
          maxIndex = (r as any).index;
        }
      }

      (warehouse as any).index = maxIndex + 1;

      this.rowsCache.splice(0, 0, warehouse);
      this.rows.splice(0, 0, warehouse);
      this.rows = [...this.rows];
    }
  }

  loadData() {
    this.alertService.startLoadingMessage();
    this.loadingIndicator = true;

    this.warehouseService.getAll<Warehouse>()
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        const warehouses = results;

        this.rowsCache = [...warehouses];
        this.rows = warehouses;
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Load Error', `Unable to retrieve roles from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }

  onSearchChanged(value: string) {
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.name, r.type));
  }

  onEditorModalHidden() {
    this.editingWarehouseName = null;
    this.warehouseEditor.resetForm(true);
  }

  newWarehouse() {
    this.editingWarehouseName = null;
    this.sourceWarehouse = null;
    this.editedWarehouse = this.warehouseEditor.newWarehouse();
    this.editorModal.show();
  }

  editWarehouse(row: Warehouse) {
    this.editingWarehouseName = { name: row.name };
    this.sourceWarehouse = row;
    this.editedWarehouse = this.warehouseEditor.editWarehouse(row);
    this.editorModal.show();
  }

  deleteWarehouse(row: Warehouse) {
    this.alertService.showDialog('Are you sure you want to delete the \"' + row.name + '\" Warehouse?',
      DialogType.confirm, () => this.deleteWarehouseHelper(row));
  }
a
  deleteWarehouseHelper(row: Warehouse) {
    this.alertService.startLoadingMessage('Deleting...');
    this.loadingIndicator = true;

    this.warehouseService.delete<Warehouse>(row.id)
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        this.rowsCache = this.rowsCache.filter(item => item !== row);
        this.rows = this.rows.filter(item => item !== row);
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Delete Error', `An error occured whilst deleting the deleteWarehouse.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }
}
