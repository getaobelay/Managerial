import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { OAuthModule } from 'angular-oauth2-oidc';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastaModule } from 'ngx-toasta';
import { TranslateLanguageLoader } from 'src/app/services/app/app-translation.service';
import { SharedModule } from '../shared/shared.module';
import { WarehosueItemEditorComponent } from './warehoues-item/warehosue-item-editor/warehosue-item-editor.component';
import { WarhouesItemManagementComponent } from './warehoues-item/warhoues-item-management/warhoues-item-management.component';
import { WarehouseService } from './warehouse-service.service';
import { WarehousesComponent } from './warehouses.component';



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
    WarehosueItemEditorComponent,WarhouesItemManagementComponent,
    WarehousesComponent
  ],
  exports: [
    WarehosueItemEditorComponent,WarhouesItemManagementComponent,
    WarehousesComponent
  ],
  providers: [WarehouseService]
})
export class WarehousesModule { }
