export interface Sale {
  Id: string;
  Number: string;
  CustomerId: string;
  Total: number;
  TotalDiscount: number;
  Branch: string;
  Status: number;
  CreatedAt: Date;
  UpdatedAt: Date;
}