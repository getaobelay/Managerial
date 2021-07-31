import { ProductCategory } from "./productCategory.model";
import { Inventory } from "../inventory/Inventory.model";
import { Stock } from "../inventory/stock.model";
import { OrderDetail } from "../order/OrderDetail.model";
import { WarehouseItem } from "../warehouse/WarehouseItem";
import { Batch } from "./Batch";
import { BaseModel } from "../BaseModel.model";

export class Product extends BaseModel {
  constructor(name?: string, BuyingPrice?: number,
    SellingPrice?: number,
   Description?: string,
    Height?: number, Weight?: number,
    isActive?: boolean,
    Id?: number, productCategory?: ProductCategory,
    createdBy?: string, UpdatedBy?: string, createDate?: Date,updatedDate?: Date
    ) {
    super()
    this.name = name;
    this.description = Description;
    this.sellingPrice = SellingPrice;
    this.buyingPrice = BuyingPrice;
    this.weight = Weight;
    this.height = Height;
    this.isActive = isActive;
    this.id = Id;
    this.CreatedBy = createdBy;
    this.UpdatedBy = UpdatedBy;
    this.CreatedDate = createDate;
    this.UpdatedDate = updatedDate;
    this.productCategory = productCategory;
  }

        public name: string;
        public description:string;
        public buyingPrice: number;
        public sellingPrice: number;
        public weight: number;
        public height: number;
        public isActive: boolean;
        public product: Product;
        public productCategory: ProductCategory;
        public children: Product[];
        public orderDetails: OrderDetail[]
        public stock: Stock
        public inventory: Inventory;
        public warehouseItems: WarehouseItem
        public batches: Batch

}
