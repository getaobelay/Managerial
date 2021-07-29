// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from '../components/app.component';
import { LoginComponent } from '../components/login/login.component';
import { NotificationsViewerComponent } from '../components/controls/notifications-viewer.component';

import { OAuthModule } from 'angular-oauth2-oidc';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ToastaModule } from 'ngx-toasta';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PopoverModule } from 'ngx-bootstrap/popover';

import { AuthService } from '../services/auth.service';
import { AppTitleService } from '../services/app/app-title.service';
import { AppTranslationService, TranslateLanguageLoader } from '../services/app-translation.service';
import { ConfigurationService } from '../services/app/configuration.service';
import { ThemeManager } from '../services/app/theme-manager';
import { AlertService } from '../services/notification/alert.service';
import { LocalStoreManager } from '../services/api/local-store-manager.service';
import { OidcHelperService } from '../services/api/oidc-helper.service';
import { NotificationService } from '../services/notification.service';
import { NotificationEndpoint } from '../services/notification-endpoint.service';
import { AccountService } from '../services/api/account.service';
import { AccountEndpoint } from '../services/api/account-endpoint.service';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        FormsModule,
        RouterTestingModule,
        TranslateModule.forRoot({
          loader: {
            provide: TranslateLoader,
            useClass: TranslateLanguageLoader
          }
        }),
        NgxDatatableModule,
        OAuthModule.forRoot(),
        ToastaModule.forRoot(),
        TooltipModule.forRoot(),
        PopoverModule.forRoot(),
        ModalModule.forRoot()
      ],
      declarations: [
        AppComponent,
        LoginComponent,
        NotificationsViewerComponent
      ],
      providers: [
        AuthService,
        AlertService,
        ConfigurationService,
        ThemeManager,
        AppTitleService,
        AppTranslationService,
        NotificationService,
        NotificationEndpoint,
        AccountService,
        AccountEndpoint,
        LocalStoreManager,
        OidcHelperService
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Managerial'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance as AppComponent;
    expect(app.appTitle).toEqual('Managerial');
  });

  it('should render Loaded! in a h1 tag', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Loaded!');
  });
});
