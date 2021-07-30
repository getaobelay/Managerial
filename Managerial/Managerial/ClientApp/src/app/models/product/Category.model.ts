export class Category {
  constructor(id?: number, name?: string,
    Description?: string) {
    this.id = id;
    this.name = name;
    this.description = Description;

  }

  public id: number;
  public name: string;
  public description: string;
}
