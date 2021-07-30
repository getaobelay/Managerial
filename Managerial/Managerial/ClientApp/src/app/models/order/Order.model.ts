import { Allocation } from "../warehouse/Allocation";
import { Customer } from "./Customer.model";
import { OrderDetail } from "./OrderDetail.model";

export class Order {
  public Discount: number;
  public Comments: string;

  public Customer: Customer
  public Allocations: Allocation
  public OrderDetails: OrderDetail
}


