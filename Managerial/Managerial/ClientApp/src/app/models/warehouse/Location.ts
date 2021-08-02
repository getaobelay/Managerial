import { BaseModel } from "../BaseModel.model";

export class Location extends BaseModel {

  constructor(id?: number,locationRow?: string, locationShelf?: string, locationColumn?: string){
    super()
    this.LocationColumn = locationColumn;
    this.LocationShelf = locationShelf;
    this.LocationRow = locationRow;
    this.id = id;
  }
  public LocationRow: string;
  public LocationColumn: string;
  public LocationShelf: string ;
}
