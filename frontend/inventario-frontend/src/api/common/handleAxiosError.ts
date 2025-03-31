import axios from "axios";

const handleAxiosError = (error: unknown): void => {
  if (axios.isAxiosError(error)) {
    console.error("Error de Axios: ", error.response?.data || error.message);
  } else {
    console.error("Error Inesperado: ", error);
  }
};

export default handleAxiosError;
