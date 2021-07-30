import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/notification/alert.service';
import { APiService } from 'src/app/services/generic/api.service';
import { Utilities } from 'src/app/services/app/utilities';
import { AppTranslationService } from 'src/app/services/app/app-translation.service';
import { Location } from 'src/app/models/warehouse/Warehouse.model'
import { LocationEditorComponent } from '../location-editor/location-editor.component';

@Component({
  selector: 'app-location-management',
  templateUrl: './location-management.component.html',
  styleUrls: ['./location-management.component.scss']
})
export class LocationManagementComponent implements OnInit, AfterViewInit {
  columns: any[] = [];
  rows: Location[] = [];
  rowsCache: Location[] = [];
  editedLocation: Location;
  sourceLocation: Location;
  editingLocationName: { name: string };
  loadingIndicator: boolean;

  @ViewChild('indexTemplate', { static: true })
  indexTemplate: TemplateRef<any>;

  @ViewChild('locationName', { static: true })
  NameTemplate: TemplateRef<any>;

  @ViewChild('type', { static: true })
  TypeTemplate: TemplateRef<any>;


  @ViewChild('isActive', { static: true })
  IsActiveTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate', { static: true })
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal', { static: true })
  editorModal: ModalDirective;

  @ViewChild('locationEditor', { static: true })
  locationEditor: LocationEditorComponent;

  constructor(private alertService: AlertService,
     private translationService: AppTranslationService,
     private locationService: APiService) {
      this.locationService.endPointUrl = 'locations';

  }

  ngOnInit() {
    const gT = (key: string) => this.translationService.getTranslation(key);

    this.columns = [
      { prop: 'index', name: '#', width: 40, cellTemplate: this.indexTemplate, canAutoResize: false },
      { prop: 'locationRow', name: 'Row', width: 50, cellTemplate: this.NameTemplate },
      { prop: 'locationColumn', name: 'Column', width: 90, cellTemplate: this.IsActiveTemplate },
      { prop: 'locationShelf', name: 'Shelf', width: 90, cellTemplate: this.IsActiveTemplate },
    ];

    this.columns.push({ name: '', width: 180, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

    this.loadData();
  }

  ngAfterViewInit() {
    this.locationEditor.changesSavedCallback = () => {
      this.AddNewLocation();
      this.editorModal.hide();
    };

    this.locationEditor.changesCancelledCallback = () => {
      this.editedLocation = null;
      this.sourceLocation = null;
      this.editorModal.hide();
    };
  }

  AddNewLocation() {
    if (this.sourceLocation) {
      Object.assign(this.sourceLocation, this.editedLocation);

      let sourceIndex = this.rowsCache.indexOf(this.sourceLocation, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.editedLocation, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.editedLocation = null;
      this.editedLocation = null;
    } else {
      const location = new Location();
      Object.assign(location, this.editedLocation);
      this.editedLocation = null;

      let maxIndex = 0;
      for (const r of this.rowsCache) {
        if ((r as any).index > maxIndex) {
          maxIndex = (r as any).index;
        }
      }

      (location as any).index = maxIndex + 1;

      this.rowsCache.splice(0, 0, location);
      this.rows.splice(0, 0, location);
      this.rows = [...this.rows];
    }
  }

  loadData() {
    this.alertService.startLoadingMessage();
    this.loadingIndicator = true;

    this.locationService.getAll<Location>()
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        const locations = results;

        this.rowsCache = [...locations];
        this.rows = locations;
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Load Error', `Unable to retrieve roles from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }

  onSearchChanged(value: string) {
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.LocationColumn, r.LocationRow));
  }

  onEditorModalHidden() {
    this.editingLocationName = null;
    this.locationEditor.resetForm(true);
  }

  newLocation() {
    this.editingLocationName = null;
    this.sourceLocation = null;
    this.editedLocation = this.locationEditor.newLocation();
    this.editorModal.show();
  }

  editLocation(row: Location) {
    this.editingLocationName = { name: row.LocationRow };
    this.sourceLocation = row;
    this.editedLocation = this.locationEditor.editLocation(row);
    this.editorModal.show();
  }

  deleteLocation(row: Location) {
    this.alertService.showDialog('Are you sure you want to delete the \"' + row.LocationRow + '\" Location?',
      DialogType.confirm, () => this.deleteLocationHelper(row));
  }
a
  deleteLocationHelper(row: Location) {
    this.alertService.startLoadingMessage('Deleting...');
    this.loadingIndicator = true;

    this.locationService.delete<Location>(row.id)
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        this.rowsCache = this.rowsCache.filter(item => item !== row);
        this.rows = this.rows.filter(item => item !== row);
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Delete Error', `An error occured whilst deleting the deleteLocation.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }
}
