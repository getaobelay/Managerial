// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Injectable } from '@angular/core';
import { Observable, Subject, forkJoin } from 'rxjs';
import { mergeMap, tap } from 'rxjs/operators';
import { Product } from 'src/app/models/Product.model';
import { AuthService } from '../auth.service';
import { ProductEndpoint } from './product-endpoint.service';

@Injectable()
export class ProductService {
  constructor(
    private authService: AuthService,
    private productEndpoint: ProductEndpoint) {
  }

  getProduct(productId?: number) {
    return this.productEndpoint.getProductEndpoint<Product>(productId);
  }

  getProducts(page?: number, pageSize?: number) {
    return this.productEndpoint.getProductsEndpoint<Product[]>(page, pageSize);
  }

  updateProduct(product: Product) {
    if (product.id) {
      return this.productEndpoint.getUpdateProductEndpoint(product, product.id);
    } else {
      return this.productEndpoint.getDeleteProductEndpoint<Product>(product.id).pipe(
        mergeMap(foundProduct => {
          product.id = foundProduct.id;
          return this.productEndpoint.getUpdateProductEndpoint(product, product.id);
        }));
    }
  }

  newProduct(Product: Product) {
    return this.productEndpoint.getNewProductEndpoint<Product>(Product);
  }

  deleteProduct(productId?: number): Observable<Product> {
    return this.productEndpoint.getDeleteProductEndpoint<Product>(productId);
  }
}
