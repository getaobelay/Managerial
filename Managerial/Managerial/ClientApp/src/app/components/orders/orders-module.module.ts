import { OrderDetailsEditorComponent } from './order-details-editor/order-details-editor.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersRoutingModule } from './orders-routing.module';
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
import { OrderManagementComponent } from './order/order-management/order-management.component';
import { OrderEditorComponent } from './order/order-editor/order-editor.component';
import { OrdersComponent } from './orders.component';
import { OrderService } from './order.service';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    OrdersRoutingModule,
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
    OrderManagementComponent, OrderEditorComponent,
    OrdersComponent,
    OrderDetailsEditorComponent
  ],
  exports: [
    OrderManagementComponent, OrderEditorComponent,
    OrdersComponent,
    OrderDetailsEditorComponent
  ],
  providers: [
    OrderService
  ]
})
export class OrdersModule { }
