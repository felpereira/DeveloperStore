// Envelope padrão para respostas da API que contêm dados
export interface ApiResponse<T> {
  data: T;
  success: boolean;
  errors: string[];
}

// --- Tipos para Vendas (Sales) ---

// Item de uma venda
export interface SaleItem {
  id: string;
  productId: string;
  productName: string;
  quantity: number;
  unitPrice: number;
  discount: number;
  total: number;
}

// Resposta para a busca de uma venda por ID (GET /sales/{id})
export interface GetSaleResponse {
  id: string;
  saleNumber: string;
  date: string; // As datas vêm como strings no formato ISO
  customerName: string;
  branchName: string;
  status: string;
  totalAmount: number;
  items: SaleItem[];
}

// Requisição para criar uma nova venda (POST /sales)
export interface CreateSaleRequest {
  saleNumber: string;
  customerId: string;
  customerName: string;
  branchId: string;
  branchName: string;
  items: CreateSaleItemRequest[];
}

export interface CreateSaleItemRequest {
  productId: string;
  productName: string;
  quantity: number;
  unitPrice: number;
  discount: number;
}

// Resposta após a criação de uma venda
export interface CreateSaleResponse {
  saleId: string;
}

export interface SalesListItem {
  id: string;
  saleNumber: string;
  date: string;
  customerName: string;
  totalAmount: number;
  status: string;
}

