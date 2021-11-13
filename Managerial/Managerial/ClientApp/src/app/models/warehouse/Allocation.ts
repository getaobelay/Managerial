import { BaseModel } from "../BaseModel.model";
import { Order } from "../order/Order.model";
import { WarehouseItem } from "./WarehouseItem.model";

export class Allocation extends BaseModel {

  public IsAllocated: boolean;
  public IsAvailable:  boolean;
  public WarehouseItems: WarehouseItem;
  public Order: Order;

}
