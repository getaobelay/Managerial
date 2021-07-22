export class Category {
  constructor(name?: string,
    Description?: string) {
    this.name = name;
    this.description = Description;

  }

  public id: number;
  public name: string;
  public description: string;
}
