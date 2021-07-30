import { Order } from "../order/Order.model";
import { WarehouseItem } from "./WarehouseItem";

export class Allocation {

  public IsAvailable: boolean;
  public IsCompleted:  boolean;
  public WarehouseItems: WarehouseItem;
  public Order: Order;

}
