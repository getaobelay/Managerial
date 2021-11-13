import { BaseModel } from "../BaseModel.model";
import { Allocation } from "../warehouse/Allocation";
import { Customer } from "./Customer.model";
import { OrderDetail } from "./OrderDetail.model";

export class Order extends BaseModel {
  public Discount: number;
  public Comments: string;
  public CustomerName: string;
  public CustomerNumber: string;
  public Customer: Customer
  public TotalPrice: number;
  public TotalItems: number;
  public OrderDetails: OrderDetail[]
  Warehouse: import("c:/Users/gadi/source/repos/getaobelay/Managerial/Managerial/Managerial/ClientApp/src/app/models/warehouse/WarehouseItem.model").WarehouseItem;
  Product: Customer;
}


