import { get } from "node:http";
import { Batch } from "../product/Batch";
import { Product } from "../product/Product.model";
import { Warehouse } from "../warehouse/Warehouse.model";

export class Inventory {
  public Name: string;
  public IsQuanityAvailable: boolean;
  public  TotalUnitsQuantity: number;
  public  ProductQuantity: number;
  public  BatchQuantity: number;
  public  UnitsInInventory: number;
  public  UnitsInOrder: number
  public  ReorderLevel: string;
  public Products: Product[]
  public Batches: Batch[]
  public Warehouses: Warehouse
}


