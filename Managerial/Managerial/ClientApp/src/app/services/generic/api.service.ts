import { Injectable } from '@angular/core';
import { Observable, Subject, forkJoin } from 'rxjs';
import { mergeMap, tap } from 'rxjs/operators';
import { ConfigurationService } from 'src/app/services/app/configuration.service';
import { ApiEndpoint } from './api-endpoint.service';

@Injectable()
export class APiService {
  constructor(
    private apiEndpoint: ApiEndpoint,
    private configurations: ConfigurationService) {
  }

  private _endPointUrl: string;

  public get endPointUrl(): string {
    return `${this.configurations.baseUrl}/api/${this._endPointUrl}`
  }

  public set endPointUrl(value: string) {
    this._endPointUrl = value;
  }



  getById<T>(productId?: number): Observable<T> {
    return this.apiEndpoint.getByIdEndpoint<T>(productId, this.endPointUrl);
  }

  getAll<T>(page?: number, pageSize?: number) {
    return this.apiEndpoint.getAllEndpoint<T[]>(page, pageSize, this.endPointUrl);
  }

  put(object: any) {
    if (object.id) {
      return this.apiEndpoint.getPutEndpoint(object, object.id, this.endPointUrl);
    } else {
      return this.apiEndpoint.getDeleteEndpoint<any>(object.id, this.endPointUrl).pipe(
        mergeMap((foundObject: any) => {
          object.id = foundObject.id;
          return this.apiEndpoint.getPutEndpoint(object, object.id,this.endPointUrl);
        }));
    }
  }

  post<T>(object: T) {
    return this.apiEndpoint.getPostEndpoint<T>(object, this.endPointUrl);
  }

  delete<T>(Id?: number): Observable<T> {
    return this.apiEndpoint.getDeleteEndpoint<T>(Id, this.endPointUrl);
  }
}
