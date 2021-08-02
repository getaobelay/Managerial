import { BaseModel } from "../BaseModel.model";
import { Product } from "../product/Product.model";
import { Order } from "./Order.model";

export class OrderDetail extends BaseModel {

  public UnitPrice :number;
  public Quantity : number;
  public Discount :number
  public Product: Product;
  public Order: Order;
}
