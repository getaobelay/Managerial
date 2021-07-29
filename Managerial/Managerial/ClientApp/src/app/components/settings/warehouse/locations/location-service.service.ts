import { Injectable } from '@angular/core';
import { ConfigurationService } from 'src/app/services/app/configuration.service';
import { ApiEndpoint } from 'src/app/services/generic/api-endpoint.service';
import { APiService } from 'src/app/services/generic/api.service';

@Injectable({
  providedIn: 'root'
})
export class LocationService extends APiService {

  constructor(apiEndPoint: ApiEndpoint,configuration: ConfigurationService){
    super(apiEndPoint, configuration );
    this.endPointUrl = "locations";
  }
}
