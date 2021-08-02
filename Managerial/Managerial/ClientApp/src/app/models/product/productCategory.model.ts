import { BaseModel } from "../BaseModel.model";
import { Category } from "./Category.model";

export class ProductCategory extends BaseModel {
  constructor(name?: string, description?: string) {
    super()
    this.name = name;
    this.description = description;
   }

    public name: string;
    public description: string

    ;}
