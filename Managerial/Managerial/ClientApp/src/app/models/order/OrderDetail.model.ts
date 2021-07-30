import { Product } from "../product/Product.model";
import { Order } from "./Order.model";

export class OrderDetail {

  public UnitPrice :number;
  public Quantity : number;
  public Discount :number
  public Product: Product;
  public Order: Order;
}
