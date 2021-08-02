import { BaseModel } from "../BaseModel.model";
import { Order } from "../order/Order.model";
import { Batch } from "../product/Batch";
import { Product } from "../product/Product.model";
import { Allocation } from "./Allocation";
import { Location } from "./Location";
import { Warehouse } from "./Warehouse.model";

export class WarehouseItem extends BaseModel{

  public ProductId: number
  public WarehouseId:number
  public AlloactionId: number
  public LocationId: number
  public Product: Product
  public Warehouse: Warehouse
  public Allocation: Allocation
  public Location: Location
  public Batch: Batch
}
