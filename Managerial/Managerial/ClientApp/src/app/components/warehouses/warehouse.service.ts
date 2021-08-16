 import { Injectable } from '@angular/core';
import { ConfigurationService } from 'src/app/services/app/configuration.service';
import { ApiEndpoint } from 'src/app/services/generic/api-endpoint.service';
import { APiService } from 'src/app/services/generic/api.service';
import { ProductService } from '../products/product.service';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService extends APiService {

  constructor(
    apiEndPoint: ApiEndpoint,
    configuration: ConfigurationService,
    private _productService: ProductService){

    super(apiEndPoint, configuration );
    this.endPointUrl = "warehouses";

  }

}
