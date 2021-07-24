import { ProductCategory } from "./Category";
import { Category } from "./Category.model";

export class Product {
  constructor(name?: string, BuyingPrice?: number,
    SellingPrice?: number,
    Measurement?: string, Description?: string,
    Height?: number, Weight?: number,
    IsActive?: boolean, QuantityPerUnit?: number,
    Id?: number, productCategory?: ProductCategory) {
    this.name = name;
    this.description = Description;
    this.sellingPrice = SellingPrice;
    this.buyingPrice = BuyingPrice;
    this.measurement = Measurement;
    this.weight = Weight;
    this.height = Height;
    this.quantityPerUnit = QuantityPerUnit;
    this.isActive = IsActive;
    this.id = Id;
    this.productCategory = productCategory;
  }
  public id: number;
  public name: string;
  public description: string;
  public sellingPrice: number;
  public buyingPrice: number;
  public measurement: string;
  public quantityPerUnit: number
  public weight: number;
  public height: number;
  public isActive: boolean
  public productCategory: ProductCategory
}
