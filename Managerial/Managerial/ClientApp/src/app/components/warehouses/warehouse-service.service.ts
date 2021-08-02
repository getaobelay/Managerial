import { Injectable } from '@angular/core';
import { ConfigurationService } from 'src/app/services/app/configuration.service';
import { ApiEndpoint } from 'src/app/services/generic/api-endpoint.service';
import { APiService } from 'src/app/services/generic/api.service';
import { LocationService } from '../settings/warehouse/locations/location-service.service';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService extends APiService {
  constructor(apiEndPoint: ApiEndpoint,
    configuration: ConfigurationService,
    _locationService: LocationService){
    super(apiEndPoint, configuration );
    this.endPointUrl = "warehouses";
  }


}
