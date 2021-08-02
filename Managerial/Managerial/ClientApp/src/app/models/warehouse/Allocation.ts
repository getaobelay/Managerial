import { BaseModel } from "../BaseModel.model";
import { Order } from "../order/Order.model";
import { WarehouseItem } from "./WarehouseItem.model";

export class Allocation extends BaseModel {

  public IsAvailable: boolean;
  public IsCompleted:  boolean;
  public WarehouseItems: WarehouseItem;
  public Order: Order;

}
