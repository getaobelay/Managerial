import { ErrorHandler, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NotificationsViewerComponent } from './notifications-viewer/notifications-viewer.component';
import { SearchBoxComponent } from './search-box/search-box.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { OAuthModule } from 'angular-oauth2-oidc';
import { ChartsModule } from 'ng2-charts';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastaModule } from 'ngx-toasta';
import { AppTranslationService, TranslateLanguageLoader } from 'src/app/services/app/app-translation.service';
import { NotFoundComponent } from './not-found/not-found.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { AppErrorHandler } from 'src/app/app-error.handler';
import { AccountEndpoint } from 'src/app/services/api/account-endpoint.service';
import { AccountService } from 'src/app/services/api/account.service';
import { LocalStoreManager } from 'src/app/services/api/local-store-manager.service';
import { OidcHelperService } from 'src/app/services/api/oidc-helper.service';
import { AppTitleService } from 'src/app/services/app/app-title.service';
import { ConfigurationService } from 'src/app/services/app/configuration.service';
import { ThemeManager } from 'src/app/services/app/theme-manager';
import { ApiEndpoint } from 'src/app/services/generic/api-endpoint.service';
import { APiService } from 'src/app/services/generic/api.service';
import { ProductService } from 'src/app/services/generic/product.service';
import { AlertService } from 'src/app/services/notification/alert.service';
import { NotificationEndpoint } from 'src/app/services/notification/notification-endpoint.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { LastElementDirective } from 'src/app/directives/last-element.directive';
import { AutofocusDirective } from 'src/app/directives/autofocus.directive';
import { BootstrapTabDirective } from 'src/app/directives/bootstrap-tab.directive';
import { BootstrapToggleDirective } from 'src/app/directives/bootstrap-toggle.directive';
import { EqualValidator } from 'src/app/directives/equal-validator.directive';
import { GroupByPipe } from 'src/app/pipes/group-by.pipe';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';


@NgModule({
  declarations: [
    NotificationsViewerComponent,
    SearchBoxComponent,
    NotFoundComponent,
    EqualValidator,
    LastElementDirective,
    AutofocusDirective,
    BootstrapTabDirective,
    BootstrapToggleDirective,
    GroupByPipe,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgxDatatableModule,
    BsDropdownModule,
    ModalModule,
    ChartsModule,
    FormsModule,
    RouterModule,
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
    NgxDatatableModule
  ],
  exports:[
    NotificationsViewerComponent,
    SearchBoxComponent,
    NotFoundComponent,
    NgxDatatableModule,
    BsDropdownModule,
    ModalModule,
    ChartsModule,
    EqualValidator,
    LastElementDirective,
    AutofocusDirective,
    BootstrapTabDirective,
    BootstrapToggleDirective,
    GroupByPipe,
    FormsModule,
    RouterModule,
    NgxDatatableModule
  ],
  providers:[
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
    ProductService,
    ApiEndpoint,
    APiService
  ]

})
export class SharedModule { }
