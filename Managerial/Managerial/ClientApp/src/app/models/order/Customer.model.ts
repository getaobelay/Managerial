import { Order } from "./Order.model";
import { OrderDetail } from "./OrderDetail.model";

export class Customer {
  public Name : string;
  public Email : string;
  public PhoneNumber : string;
  public Address : string;
  public City : string;
  public Orders: Order;
}


