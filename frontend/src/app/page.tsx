'use client'
import api from "@/lib/api";
import { Button } from "@/components/ui/button";
import { SalesListItem } from "@/types/api";
import { useQuery } from "@tanstack/react-query";
import Image from "next/image";
import { DataTable } from "@/components/data-table";
import { columns } from "@/components/columns";
import { Skeleton } from "@/components/ui/skeleton";
import { CreateSaleDialog } from "@/components/create-sale-dialog";

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
    return (
        <div className="container mx-auto py-10">
            <div className="flex justify-between items-center mb-4">
                <Skeleton className="h-8 w-48" />
                <Skeleton className="h-10 w-32" />
            </div>
            <div className="space-y-2">
                <Skeleton className="h-12 w-full" />
                <Skeleton className="h-12 w-full" />
                <Skeleton className="h-12 w-full" />
                <Skeleton className="h-12 w-full" />
            </div>
        </div>
    );
  }

  if (error) {
    return <div>Ocorreu um erro ao buscar as vendas.</div>;
  }


  
  return (
<div className="container mx-auto py-10">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-2xl font-bold">Vendas</h2>
        <CreateSaleDialog />
      </div>
      <DataTable columns={columns} data={data ?? []} />
    </div>
  );
}

export default Home;
