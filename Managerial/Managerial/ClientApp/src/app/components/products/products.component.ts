// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BootstrapTabDirective } from 'src/app/directives/bootstrap-tab.directive';
import { AccountService } from 'src/app/services/api/account.service';
import { fadeInOut } from '../../services/app/animations';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss'],
    animations: [fadeInOut]
})
export class ProductsComponent {

}
