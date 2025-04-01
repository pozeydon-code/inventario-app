export type TransactionType = "Buy" | "Sell";

export interface Transaction {
  id: string;
  date: Date;
  type: TransactionType;
  productId: string;
  quantity: number;
  unitPrice: number;
  detail: string;
}

export interface Filters {
  productId?: string;
  type?: string;
  startDate: string;
  endDate: string;
}

export const EmptyFilters: Filters = {
  productId: "",
  type: "",
  startDate: "",
  endDate: "",
};

export const EmptyCreateTransaction: Transaction = {
  id: "",
  date: new Date(),
  type: "Buy",
  productId: "",
  quantity: 0,
  unitPrice: 0,
  detail: "",
};
