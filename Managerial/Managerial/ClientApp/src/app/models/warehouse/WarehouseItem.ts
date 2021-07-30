import { Order } from "../order/Order.model";
import { Batch } from "../product/Batch";
import { Product } from "../product/Product.model";
import { Allocation } from "./Allocation";
import { Warehouse } from "./Warehouse.model";

export class WarehouseItem {


  public Product: Product
  public Warehouse: Warehouse
  public Allocation: Allocation
  public Location: Location
  public Batch: Batch
}
