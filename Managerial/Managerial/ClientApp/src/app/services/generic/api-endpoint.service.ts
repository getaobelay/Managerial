
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { EndpointBase } from '../api/endpoint-base.service';
import { ConfigurationService } from '../app/configuration.service';
import { AuthService } from '../auth/auth.service';



@Injectable()
export class ApiEndpoint extends EndpointBase {
   // private endpointUrl: string;

    //get completeUrl() { return `${this.configurations.baseUrl}/api/${this.endpointUrl}` }

    constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {
        super(http, authService);
    }

    getByIdEndpoint<T>(Id?: number, endpointUrl?: string): Observable<T> {
        const completeUrl = `${endpointUrl}/${Id}`;

        return this.http.get<T>(completeUrl, this.requestHeaders).pipe<T>(
            catchError(error =>{
                return this.handleError(error, () => this.getByIdEndpoint(Id,endpointUrl));
            }));
    }

    getAllEndpoint<T>(page?: number, pageSize?: number, endpointUrl?: string): Observable<T> {


        const completeUrl = page && pageSize ? `${endpointUrl}/${page}/${pageSize}` : `${endpointUrl}/${-1}/${1}`;

        return this.http.get<T>(completeUrl, this.requestHeaders).pipe<T>(
            catchError(error => {
                return this.handleError(error, () => this.getAllEndpoint(page, pageSize, endpointUrl));
            }));
    }

    getPostEndpoint<T>(object: any, endpointUrl?: string): Observable<T> {
        return this.http.post<T>(endpointUrl, JSON.stringify(object), this.requestHeaders).pipe<T>(
            catchError(error => {
                return this.handleError(error, () => this.getPostEndpoint(object, endpointUrl));
            }));
    }

    getPutEndpoint<T>(object: any, Id?: number, endpointUrl?: string): Observable<T> {
        const completeUrl = `${endpointUrl}/${Id}`;

        return this.http.put<T>(completeUrl, JSON.stringify(object), this.requestHeaders).pipe<T>(
            catchError(error => {
                return this.handleError(error, () => this.getPutEndpoint(object, Id, endpointUrl));
            }));
    }


    getDeleteEndpoint<T>(Id: number, endpointUrl?: string): Observable<T> {
        const completeUrl = `${endpointUrl}/${Id}`;

        return this.http.delete<T>(completeUrl, this.requestHeaders).pipe<T>(
            catchError(error => {
                return this.handleError(error, () => this.getDeleteEndpoint(Id,endpointUrl));
            }));
    }
}
