//Interface used like a source for the tables.
export interface PageResponse<T> {
  items: T[];
  totalCount: number;
}
