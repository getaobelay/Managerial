import { Category } from "./Category.model";

export class Warehouse {
  constructor(id?: number, name?: string, type?: string, isActive?: boolean) {
    this.isActive = isActive;
    this.type = type;
    this.name = name;
   }

   public id: number;
   public name:string;
   public type: string;
   public isActive: boolean;

    ;}
