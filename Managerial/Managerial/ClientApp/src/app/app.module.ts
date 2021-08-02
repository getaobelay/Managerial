// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { OAuthModule } from 'angular-oauth2-oidc';
import { ToastaModule } from 'ngx-toasta';
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ChartsModule } from 'ng2-charts';

import { AppRoutingModule } from './app-routing.module';
import { AppErrorHandler } from './app-error.handler';
import { AppTitleService } from './services/app/app-title.service';
import { ConfigurationService } from './services/app/configuration.service';
import { AlertService } from './services/notification/alert.service';
import { ThemeManager } from './services/app/theme-manager';
import { LocalStoreManager } from './services/api/local-store-manager.service';
import { OidcHelperService } from './services/api/oidc-helper.service';

import { AccountService } from './services/api/account.service';
import { AccountEndpoint } from './services/api/account-endpoint.service';


import { AppComponent } from './components/app.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from './components/about/about.component';

import { BannerDemoComponent } from './components/controls/banner-demo.component';
import { TodoDemoComponent } from './components/controls/todo-demo.component';
import { StatisticsDemoComponent } from './components/controls/statistics-demo.component';
import { AppTranslationService, TranslateLanguageLoader } from './services/app/app-translation.service';
import { NotificationEndpoint } from './services/notification/notification-endpoint.service';
import { NotificationService } from './services/notification/notification.service';
import { ProductsModule } from './components/products/products-module.module';
import { WarehousesModule } from './components/warehouses/warehouses-module.module';
import { SettingsModule } from './components/settings/settings-module.module';
import { SharedModule } from './components/shared/shared.module';
import { WarehouseService } from './components/warehouses/warehouse-service.service';
import { OrdersModule } from './components/orders/orders-module.module';
import { CustomersModule } from './components/customers/customers-module.module';
import { ApiEndpoint } from './services/generic/api-endpoint.service';
import { APiService } from './services/generic/api.service';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: TranslateLanguageLoader
      }
    }),
    NgxDatatableModule,
    OAuthModule.forRoot(),
    ToastaModule.forRoot(),
    NgSelectModule,
    TooltipModule.forRoot(),
    PopoverModule.forRoot(),
    BsDropdownModule.forRoot(),
    CarouselModule.forRoot(),
    ModalModule.forRoot(),
    ChartsModule,

    ProductsModule,
    WarehousesModule,
    CustomersModule,
    OrdersModule,
    SettingsModule,
    SharedModule
  ],
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AboutComponent,
    StatisticsDemoComponent, TodoDemoComponent, BannerDemoComponent,

  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    AlertService,
    ThemeManager,
    ConfigurationService,
    AppTitleService,
    AppTranslationService,
    NotificationService,
    NotificationEndpoint,
    AccountService,
    AccountEndpoint,
    LocalStoreManager,
    OidcHelperService,
    WarehouseService,
    ApiEndpoint,
    APiService
  ],
  exports:[TodoDemoComponent],
  bootstrap: [AppComponent]
})
export class AppModule {
}
