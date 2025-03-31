import axios from "axios";

const axiosClient = axios.create({
  baseURL: "http://localhost:5106/api",
  headers: {
    "Content-Type": "application/json",
  },
});

axiosClient.interceptors.response.use(
  (response) => {
    return response;
  },
  function (error) {
    return error.response;
  },
);
export default axiosClient;
