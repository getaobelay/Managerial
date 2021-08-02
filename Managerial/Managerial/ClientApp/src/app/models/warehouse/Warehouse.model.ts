import { BaseModel } from "../BaseModel.model";

export class Warehouse extends BaseModel{
  constructor(id?: number, name?: string, type?: string, isActive?: boolean) {
    super()
    this.isActive = isActive;
    this.type = type;
    this.name = name;
   }

   public name:string;
   public type: string;
   public isActive: boolean;
}

