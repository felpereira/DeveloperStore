"use client";

import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { zodResolver } from "@hookform/resolvers/zod";
import { useFieldArray, useForm } from "react-hook-form";
import { z } from "zod";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import api from "@/lib/api";
import { CreateSaleRequest } from "@/types/api";
import { toast } from "sonner"
import { Trash2 } from "lucide-react";

const formSchema = z.object({
  saleNumber: z.string().min(1, "O número da venda é obrigatório."),
  customerName: z.string().min(1, "O nome do cliente é obrigatório."),
  branchName: z.string().min(1, "O nome da filial é obrigatório."),
  items: z.array(z.object({
    productName: z.string().min(1, "O nome do produto é obrigatório."),
    quantity: z.coerce.number().min(1, "A quantidade deve ser maior que 0."),
    unitPrice: z.coerce.number().min(0, "O preço não pode ser negativo."),
    discount: z.coerce.number().min(0, "O desconto não pode ser negativo.").max(100, "O desconto não pode ser maior que 100."),
  })).min(1, "A venda deve ter pelo menos um item."),
});

// Função que envia os dados para a API
async function createSale(data: CreateSaleRequest) {
  const response = await api.post("/sales", data);
  return response.data;
}

export function CreateSaleDialog() {
 
  const queryClient = useQueryClient();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      saleNumber: "",
      customerName: "",
      branchName: "",
      items: [{ productName: "", quantity: 1, unitPrice: 0, discount: 0 }],
    },
  });

  const { fields, append, remove } = useFieldArray({
    control: form.control,
    name: "items",
  });

  const mutation = useMutation({
    mutationFn: createSale,
    onSuccess: () => {
      toast.success("Sucesso!", {
        description: "Venda criada com sucesso.",
      });
      // Invalida a query de 'sales' para forçar a atualização da tabela
      queryClient.invalidateQueries({ queryKey: ["sales"] });
      form.reset();
    },
    onError: () => {
      toast.error("Erro", {
        description: "Não foi possível criar a venda.",
      });
    },
  });

  function onSubmit(values: z.infer<typeof formSchema>) {
    // Adiciona IDs fictícios, já que o backend não os utiliza, mas a entidade sim
    const payload: CreateSaleRequest = {
      ...values,
      customerId: "00000000-0000-0000-0000-000000000001", // ID Fictício
      branchId: "00000000-0000-0000-0000-000000000002", // ID Fictício
      items: values.items.map(item => ({
        ...item,
        productId: "00000000-0000-0000-0000-000000000003" // ID Fictício
      }))
    };
    mutation.mutate(payload);
  }

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button>Nova Venda</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[600px]">
        <DialogHeader>
          <DialogTitle>Criar Nova Venda</DialogTitle>
          <DialogDescription>
            Preencha as informações abaixo para registrar uma nova venda.
          </DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <div className="grid grid-cols-2 gap-4">
              <FormField
                control={form.control}
                name="saleNumber"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Número da Venda</FormLabel>
                    <FormControl>
                      <Input placeholder="Ex: V-001" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="customerName"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Nome do Cliente</FormLabel>
                    <FormControl>
                      <Input placeholder="João da Silva" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
               <FormField
                control={form.control}
                name="branchName"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Nome da Filial</FormLabel>
                    <FormControl>
                      <Input placeholder="Filial Centro" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>

            <h3 className="text-lg font-medium border-t pt-4">Itens da Venda</h3>
            
            <div className="space-y-4 max-h-60 overflow-y-auto pr-2">
              {fields.map((field, index) => (
                <div key={field.id} className="grid grid-cols-5 gap-2 items-start border p-2 rounded-md">
                   <FormField
                    control={form.control}
                    name={`items.${index}.productName`}
                    render={({ field }) => (
                      <FormItem className="col-span-2">
                        <FormLabel>Produto</FormLabel>
                        <FormControl>
                          <Input placeholder="Produto A" {...field} />
                        </FormControl>
                         <FormMessage />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name={`items.${index}.quantity`}
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Qtd.</FormLabel>
                        <FormControl>
                          <Input type="number" {...field} />
                        </FormControl>
                         <FormMessage />
                      </FormItem>
                    )}
                  />
                   <FormField
                    control={form.control}
                    name={`items.${index}.unitPrice`}
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Preço</FormLabel>
                        <FormControl>
                          <Input type="number" step="0.01" {...field} />
                        </FormControl>
                         <FormMessage />
                      </FormItem>
                    )}
                  />
                  <div className="flex items-end h-full">
                     <Button type="button" variant="destructive" size="icon" onClick={() => remove(index)}>
                        <Trash2 className="h-4 w-4" />
                     </Button>
                  </div>
                </div>
              ))}
            </div>

            <Button type="button" variant="outline" size="sm" onClick={() => append({ productName: "", quantity: 1, unitPrice: 0, discount: 0 })}>
              Adicionar Item
            </Button>

            <div className="flex justify-end pt-4">
              <Button type="submit" disabled={mutation.isPending}>
                {mutation.isPending ? "Salvando..." : "Salvar Venda"}
              </Button>
            </div>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  );
}
