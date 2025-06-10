using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    // Representa a entidade Produto.
    public class Product : BaseEntity
    {
        // Construtor privado para o EF Core e mapeamento.
        private Product() { }

        public Product(string title, decimal price, string description, string category, string image, Rating rating)
        {
            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
            Rating = rating;
        }

        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string Image { get; private set; }

        // O Rating é um Value Object, representando um aspeto descritivo do Produto.
        public Rating Rating { get; private set; }

        // Poderiam ser adicionados aqui métodos para atualizar o produto,
        // gerir o stock, etc., encapsulando a lógica de negócio.
    }
}
