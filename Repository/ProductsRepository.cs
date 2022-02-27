using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Linq;

namespace MinApi.Repository
{
    public class ProductsRepository
    {
        private static IList<Product> Products = new List<Product>
        {
            new Product {
                Id = 1,
                Name = "Lenovo Ideapad 3i",
                CreatedOn = new DateOnly(2021, 07, 11)
            },
            new Product {
                Id = 2,
                Name = "Lenovo - Yoga 7i",
                CreatedOn = new DateOnly(2021, 10, 5)
            },
            new Product {
                Id = 3,
                Name = "MacBook Pro 14-inch",
                CreatedOn = new DateOnly(2021, 06, 20)
            },
            new Product {
                Id = 4,
                Name = "Microsoft - Surface Pro 8",
                CreatedOn = new DateOnly(2021, 11, 6)
            },
        };

        public async Task<List<Product>> GetProducts()
        {
            await Task.Delay(1);
            return Products.ToList();
        }

        public async Task<Product?> GetProduct(int id)
        {
            await Task.Delay(1);
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public async Task CreateProduct(Product prod)
        {
            await Task.Delay(1);
            var maxId = Products.Max(p => p.Id);
            prod.Id = maxId + 1;
            prod.CreatedOn = new DateOnly(2022, 02, 27);
            Products.Add(prod);
        }

        public async Task DeleteProduct(int id)
        {
            await Task.Delay(1);
            Products.Remove(Products.First(p => p.Id == id));
        }
    }

    public class Product
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string? Name { get; set; }

        //Crap This doesn't work and needs a DateOnly Json Converter
        [JsonIgnore]
        public DateOnly CreatedOn { get; set; }
    }
}