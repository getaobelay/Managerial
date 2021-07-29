import { Injectable } from '@angular/core';
import { ConfigurationService } from 'src/app/services/app/configuration.service';
import { ApiEndpoint } from 'src/app/services/generic/api-endpoint.service';
import { APiService } from 'src/app/services/generic/api.service';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService extends APiService {
  warehouseItemService: APiService;
  locationService: APiService;

  constructor(apiEndPoint: ApiEndpoint,private _warehouseItemService: APiService,private _locationService: APiService, configuration: ConfigurationService){
    super(apiEndPoint, configuration );
    this.endPointUrl = "warehouses";
    this.locationService = _locationService;
    this.warehouseItemService = _warehouseItemService;
    this.locationService.endPointUrl = "locations";
    this.warehouseItemService.endPointUrl ="warehouseitems"
  }
}
