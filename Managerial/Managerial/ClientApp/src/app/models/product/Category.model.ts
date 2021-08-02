import { BaseModel } from "../BaseModel.model";

export class Category extends BaseModel {
  constructor(id?: number, name?: string,
    Description?: string) {
    super();
    this.id = id;
    this.name = name;
    this.description = Description;

  }

  public name: string;
  public description: string;
}
