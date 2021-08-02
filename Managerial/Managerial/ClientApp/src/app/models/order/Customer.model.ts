import { BaseModel } from "../BaseModel.model";
import { Order } from "./Order.model";
import { OrderDetail } from "./OrderDetail.model";

export class Customer extends BaseModel {
  public Name : string;
  public Email : string;
  public PhoneNumber : string;
  public Address : string;
  public City : string;
  public Orders: Order;
}


