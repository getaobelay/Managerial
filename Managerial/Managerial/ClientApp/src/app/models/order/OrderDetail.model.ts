import { WarehouseItem } from './../warehouse/WarehouseItem.model';
import { BaseModel } from "../BaseModel.model";
import { Product } from "../product/Product.model";
import { Order } from "./Order.model";

export class OrderDetail extends BaseModel {

  public UnitPrice :number;
  public Quantity : number;
  public Discount :number
  public ProductName :string
  public ProductDesc :string
  public BuyingPrice :number
  public SellingPrice :number
  public WarehouseItem: WarehouseItem;
  public Order: Order;
}
