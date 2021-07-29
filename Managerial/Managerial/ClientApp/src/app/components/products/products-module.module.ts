import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductEditorComponent } from './product/product-editor/product-editor.component';
import { ProductManagementComponent } from './product/product-management/product-management.component';
import { SharedModule } from '../shared/shared.module';
import { ProductsComponent } from './products.component';
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

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ProductsRoutingModule,
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
    ProductEditorComponent, ProductManagementComponent,
    ProductsComponent,
  ],
  exports: [
    ProductEditorComponent, ProductManagementComponent,
    ProductsComponent
  ]
})
export class ProductsModule { }
