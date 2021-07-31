import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SharedModule } from '../shared/shared.module';
import { RoleEditorComponent } from './role/role-editor/role-editor.component';
import { RolesManagementComponent } from './role/role-management/roles-management.component';
import { SettingsComponent } from './settings.component';
import { UserInfoComponent } from './user/user-info/user-info.component';
import { UsersManagementComponent } from './user/user-management/users-management.component';
import { UserPreferencesComponent } from './user/user-preferences/user-preferences.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { OAuthModule } from 'angular-oauth2-oidc';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastaModule } from 'ngx-toasta';
import { TranslateLanguageLoader } from 'src/app/services/app/app-translation.service';
import { WarehouseManagementComponent } from './warehouse/warehouse/warehouse-management/warehouse-management.component';
import { LocationManagementComponent } from './warehouse/locations/location-management/location-management.component';
import { LocationEditorComponent } from './warehouse/locations/location-editor/location-editor.component';
import { BatchEditorComponent } from './product/batch/batch-editor/batch-editor.component';
import { BatchManagementComponent } from './product/batch/batch-management/batch-management.component';
import { CategoryEditorComponent } from './product/category/category-editor/category-editor.component';
import { CategoryManagementComponent } from './product/category/category-management.component';
import { WarehouseEditorComponent } from './warehouse/warehouse/warehouse-editor/warehouse-editor.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: TranslateLanguageLoader
      }
    }),
    OAuthModule.forRoot(),
    ToastaModule.forRoot(),
    NgSelectModule,
    TooltipModule.forRoot(),
    PopoverModule.forRoot(),
    BsDropdownModule.forRoot(),
    CarouselModule.forRoot(),
    ModalModule.forRoot(),
  ],
  declarations: [
    UsersManagementComponent, UserInfoComponent, UserPreferencesComponent,
    RolesManagementComponent, RoleEditorComponent,
    CategoryEditorComponent, CategoryManagementComponent,
    WarehouseManagementComponent, WarehouseEditorComponent,
    LocationManagementComponent, LocationEditorComponent,
    BatchEditorComponent, BatchManagementComponent,
    SettingsComponent,

  ],
  exports: [
    UsersManagementComponent, UserInfoComponent, UserPreferencesComponent,
    RolesManagementComponent, RoleEditorComponent,
    CategoryEditorComponent, CategoryManagementComponent,
    WarehouseManagementComponent, WarehouseEditorComponent,
    LocationManagementComponent, LocationEditorComponent,
    BatchEditorComponent, BatchManagementComponent,
    SettingsComponent,

  ]
})
export class SettingsModule { }
