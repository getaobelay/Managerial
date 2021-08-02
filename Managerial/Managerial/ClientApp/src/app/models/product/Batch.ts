import { BaseModel } from "../BaseModel.model"
import { Inventory } from "../inventory/Inventory.model"
import { Stock } from "../inventory/stock.model"
import { WarehouseItem } from "../warehouse/WarehouseItem.model"
import { Product } from "./Product.model"

export class Batch extends BaseModel {
       public Product: Product
        public Inventory: Inventory
        public Stock: Stock
        public WarehouseItems: WarehouseItem
}
