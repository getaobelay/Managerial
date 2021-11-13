import { Injectable } from '@angular/core';
import { forkJoin } from 'rxjs';
import { Customer } from 'src/app/models/order/Customer.model';
import { WarehouseItem } from 'src/app/models/warehouse/WarehouseItem.model';
import { ConfigurationService } from 'src/app/services/app/configuration.service';
import { ApiEndpoint } from 'src/app/services/generic/api-endpoint.service';
import { APiService } from 'src/app/services/generic/api.service';
import { CustomerService } from '../customers/customers-service.service';
import { WarehouseItemService } from '../warehouses/warehoues-item/warehouse-item-service.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService extends APiService {


    constructor(apiEndPoint: ApiEndpoint,
      configuration: ConfigurationService,
      private warehouseItemService: WarehouseItemService,
      private customerService: CustomerService){
      super(apiEndPoint, configuration );
      this.endPointUrl = "orders";
  }



  loadOrderEditor(page?: number, pageSize?: number) {
    return forkJoin([
        this.warehouseItemService.getAll<WarehouseItem>(),
        this.customerService.getAll<Customer>()]);
  }

}
