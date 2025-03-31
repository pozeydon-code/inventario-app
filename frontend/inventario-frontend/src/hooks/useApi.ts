import { useState } from "react";
import axiosClient from "../api/clients/axiosClient";
import handleAxiosError from "../api/common/handleAxiosError";

type HttpMethod = "get" | "post" | "put" | "delete";

interface RequestOptions {
  method: HttpMethod;
  url: string;
  body?: unknown;
  params?: Record<string, any> | null;
}

export function useApi<T = any>() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<unknown>(null);
  const [data, setData] = useState<T | null>(null);

  const sendRequest = async ({
    method,
    url,
    body = null,
    params = null,
  }: RequestOptions): Promise<T | null> => {
    setLoading(true);
    setError(null);

    try {
      console.log(body);

      const response = await axiosClient({
        method,
        url,
        data: body,
        params: { ...params },
      });

      setData(response.data);
      return response.data;
    } catch (err) {
      handleAxiosError(err);
      setError(err);
      return null;
    } finally {
      setLoading(false);
    }
  };

  return { loading, error, data, sendRequest };
}
