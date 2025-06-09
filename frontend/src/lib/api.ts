import axios from "axios";

// Cria uma instância do axios com configurações padrão
const api = axios.create({
  // A URL base da nossa API .NET.
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export default api;
