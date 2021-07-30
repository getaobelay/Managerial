// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Injectable } from '@angular/core';
import { Observable, Subject, forkJoin } from 'rxjs';
import { mergeMap, tap } from 'rxjs/operators';
import { Category } from 'src/app/models/product/Category.model';
import { Product } from 'src/app/models/product/Product.model';
import { ConfigurationService } from '../app/configuration.service';
import { ApiEndpoint } from './api-endpoint.service';
import { APiService } from './api.service';

export type DataChangedOperation = 'add' | 'delete' | 'modify';
export interface DataChangedEventArg<T> { data: T[] | string[]; operation: DataChangedOperation; }

@Injectable()
export class ProductService extends APiService {
  categoryService: APiService;

  constructor(productApiEndpoint: ApiEndpoint,categoryService: APiService,configuration: ConfigurationService){
    super(productApiEndpoint, configuration );
    this.categoryService = categoryService
    this.endPointUrl = "products";
    this.categoryService.endPointUrl = "categories";
  }


    public static readonly AddedOperation: DataChangedOperation = 'add';
    public static readonly DeletedOperation: DataChangedOperation = 'delete';
    public static readonly ModifiedOperation: DataChangedOperation = 'modify';

    private categoriesChanged = new Subject<DataChangedEventArg<Category>>();

    onCategoryCountChanged(categories: Category[] | string[]) {
      return this.onCategoriesChanged(categories, ProductService.ModifiedOperation);
  }

  getRolesChangedEvent(): Observable<DataChangedEventArg<Category>> {
      return this.categoriesChanged.asObservable();
  }


  private onCategoriesChanged(data: Category[] | string[], op: DataChangedOperation) {
    this.categoriesChanged.next({ data, operation: op });
  }

  getProductAndCategories(productId?: number) {
    return forkJoin([
        this.getById<Product>(productId),
        this.categoryService.getAll<Category>()]);
}

  getProductsAndCategories(page?: number, pageSize?: number) {
    return forkJoin([
        this.getAll<Product>(page, pageSize),
        this.categoryService.getAll<Category>()]);
  }


  getCategories(page?: number, pageSize?: number) {
       return this.categoryService.getAll<Category>(page,pageSize);
  }

}
