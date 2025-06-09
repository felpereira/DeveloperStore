'use client'
import api from "@/lib/api";
import { Button } from "@/components/ui/button";
import { SalesListItem } from "@/types/api";
import { useQuery } from "@tanstack/react-query";
import Image from "next/image";
import { DataTable } from "@/components/data-table";
import { columns } from "@/components/columns";

async function getSales(): Promise<SalesListItem[]> {
  const response = await api.get<SalesListItem[]>("/sales");
  return response.data; 
}


const Home = () => {
    const { data, isLoading, error } = useQuery<SalesListItem[]>({
    queryKey: ["sales"],
    queryFn: getSales,
  });

    if (isLoading) {
    return <div>Carregando...</div>;
  }

  if (error) {
    return <div>Ocorreu um erro ao buscar as vendas.</div>;
  }


  
  return (
    <div className="container mx-auto py-10">
      <h2 className="text-2xl font-bold mb-4">Vendas</h2>
      <DataTable columns={columns} data={data ?? []} />
    </div>
  );
}

export default Home;
