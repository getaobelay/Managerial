import { BaseModel } from "../BaseModel.model";
import { Order } from "../order/Order.model";
import { Batch } from "../product/Batch";
import { Product } from "../product/Product.model";
import { Allocation } from "./Allocation";
import { Location } from "./Location";
import { Warehouse } from "./Warehouse.model";

export class WarehouseItem extends BaseModel{

  public ProductName: string
  public WarehouseName: string
  public BuyingPrice:number
  public SellingPrice: number
  public Location: string
  public Warehouse: Warehouse
  public Product: Product
  public Batch: Batch
}
