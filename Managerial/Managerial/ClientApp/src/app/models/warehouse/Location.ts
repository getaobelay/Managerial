export class Location {

  constructor(id?: number,locationRow?: string, locationShelf?: string, locationColumn?: string){
    this.LocationColumn = locationColumn;
    this.LocationShelf = locationShelf;
    this.LocationRow = locationRow;
    this.id = id;
  }
  public id: number;
  public LocationRow: string;
  public LocationColumn: string;
  public LocationShelf: string ;
}
