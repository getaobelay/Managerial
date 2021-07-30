import { ProductCategory } from "./productCategory.model";
import { Inventory } from "../inventory/Inventory.model";
import { Stock } from "../inventory/stock.model";
import { OrderDetail } from "../order/OrderDetail.model";
import { WarehouseItem } from "../warehouse/WarehouseItem";
import { Batch } from "./Batch";

export class Product {
  constructor(name?: string, BuyingPrice?: number,
    SellingPrice?: number,
   Description?: string,
    Height?: number, Weight?: number,
    isActive?: boolean,
    Id?: number, productCategory?: ProductCategory) {
    this.name = name;
    this.description = Description;
    this.sellingPrice = SellingPrice;
    this.buyingPrice = BuyingPrice;
    this.weight = Weight;
    this.height = Height;
    this.isActive = isActive;
    this.id = Id;
    this.productCategory = productCategory;
  }

        public id: number;
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
        public CreatedBy: string;
        public UpdatedBy: string;
        public UpdatedDate: string;
        public CreatedDate: string;
}
