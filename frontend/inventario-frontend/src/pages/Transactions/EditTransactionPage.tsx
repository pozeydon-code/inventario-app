import { URLS } from "@/api/url";
import { CreateTransactionForm } from "@/components/CustomForms/CreateTransactionForm";
import { useApi } from "@/hooks/useApi";
import { Transaction } from "@/models/Transactions/transaction.model";
import { useEffect } from "react";
import { useParams } from "react-router-dom";

export const EditTransactionPage = () => {
  const { id } = useParams<{ id: string }>();
  const { data, sendRequest } = useApi<Transaction>();

  useEffect(() => {
    sendRequest({ method: "get", url: `${URLS.getTransactions}/${id}` });
  }, [id]);

  if (!data) return null;

  return <CreateTransactionForm initialData={data} isEdit={true} />;
};
