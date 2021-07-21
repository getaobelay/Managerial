// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from '../auth.service';
import { EndpointBase } from '../endpoint-base.service';
import { ConfigurationService } from '../configuration.service';


@Injectable()
export class ProductEndpoint extends EndpointBase {

  get productsUrl() { return this.configurations.baseUrl + '/api/products'; }


  constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {
    super(http, authService);
  }


  getProductEndpoint<T>(productId?: number): Observable<T> {
    const endpointUrl = `${this.productsUrl}/${productId}`;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getProductEndpoint(productId));
      }));
  }



  getProductsEndpoint<T>(page?: number, pageSize?: number): Observable<T> {
    const endpointUrl = page && pageSize ? `${this.productsUrl}/${page}/${pageSize}` : `${this.productsUrl}/${-1}/${1}`;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getProductsEndpoint(page, pageSize));
      }));
  }


  getNewProductEndpoint<T>(productObject: any): Observable<T> {

    return this.http.post<T>(this.productsUrl, JSON.stringify(productObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getNewProductEndpoint(productObject));
      }));
  }

  getUpdateProductEndpoint<T>(productObject: any, productId?: number): Observable<T> {
    const endpointUrl = `${this.productsUrl}/${productId}`;

    return this.http.put<T>(endpointUrl, JSON.stringify(productObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUpdateProductEndpoint(productObject, productId));
      }));
  }

  getDeleteProductEndpoint<T>(productId: number): Observable<T> {

    const endpointUrl = `${this.productsUrl}/${productId}`;

    return this.http.delete<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getDeleteProductEndpoint(productId));
      }));
  }




}
