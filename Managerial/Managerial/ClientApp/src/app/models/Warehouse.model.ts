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
