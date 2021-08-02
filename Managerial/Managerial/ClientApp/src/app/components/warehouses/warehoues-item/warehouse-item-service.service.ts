import { Injectable } from '@angular/core';
import { forkJoin } from 'rxjs';
import { Product } from 'src/app/models/product/Product.model';
import { Location } from 'src/app/models/warehouse/Location';
import { Warehouse } from 'src/app/models/warehouse/Warehouse.model';
import { ConfigurationService } from 'src/app/services/app/configuration.service';
import { ApiEndpoint } from 'src/app/services/generic/api-endpoint.service';
import { APiService } from 'src/app/services/generic/api.service';
import { ProductService } from '../../products/product.service';
import { LocationService } from '../../settings/warehouse/locations/location-service.service';
import { WarehouseService } from '../warehouse-service.service';


@Injectable({
  providedIn: 'root'
})
export class WarehouseItemService extends APiService {

r
  constructor(
    apiEndPoint: ApiEndpoint,
    configuration: ConfigurationService,
    private _productService: ProductService,
    private _locationService: LocationService,
    private _warehouseService: WarehouseService){

    super(apiEndPoint, configuration );
    this.endPointUrl = "warehouseitems";

  }


  loadWarehouseItemEditor(page?: number, pageSize?: number) {
    return forkJoin([
        this._productService.getAll<Product>(),
        this._warehouseService.getAll<Warehouse>(),
        this._locationService.getAll<Location>()]);
  }

}
