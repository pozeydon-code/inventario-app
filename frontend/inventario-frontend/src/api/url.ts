const URL_PRODUCTS = "http://localhost:5106";
const ENDPOINT_PRODUCTS = "http://localhost:5106/api/";
const ENDPOINT_TRANSACTIONS = "http://localhost:5058/api/";

export const URLS = {
  imageUrls: URL_PRODUCTS,
  products: ENDPOINT_PRODUCTS + "products",
  getTransactions: ENDPOINT_TRANSACTIONS + "transactions",
  getTransactionsPaged: ENDPOINT_TRANSACTIONS + "transactions/paged",
};
