export class BaseModel {
  /**
   *
   */
  constructor(id?: number, CreatedBy?: string, UpdatedBy?: string, CreatedDate?: Date,UpdatedDate?: Date) {

  }
  id: number;
  public CreatedBy: string;
  public UpdatedBy: string;
  public UpdatedDate: Date;
  public CreatedDate: Date;
}
