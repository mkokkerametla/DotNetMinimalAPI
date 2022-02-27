using MinApi.Repository;

namespace MinApi
{
    public class ProductsApi : IMinApi
    {
        private readonly ILogger<ProductsApi> _logger;
        public ProductsApi(ILogger<ProductsApi> logger)
        {
            _logger = logger;
        }
        public void Register(WebApplication app)
        {
            app.MapGet("/api/products/{id}", Get);
            app.MapGet("/api/products", GetAll);
            app.MapPost("/api/products", Post);
            app.MapDelete("/api/products/{id}", Delete);
        }

        private async Task<IResult> Get(int id, ProductsRepository repo)
        {
            if (id < 1) return Results.BadRequest($"{nameof(id)} must be greater than zero.");
            var result = await repo.GetProduct(id);
            if (result is null) return Results.NotFound();
            return Results.Ok(result);
        }

        private async Task<IResult> GetAll(ProductsRepository repo)
        {
            var results = await repo.GetProducts();
            if (results is null) return Results.NotFound();
            return Results.Ok(results);
        }

        private async Task<IResult> Post(Product prod, ProductsRepository repo)
        {
            try
            {
                await repo.CreateProduct(prod);
                return Results.Created($"/products/{prod.Id}", prod);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred while creating Product - {prod.Name}", ex);
                return Results
                .BadRequest($"Error Occurred while creating Product - {prod.Name}: {ex}");
            }
        }

        private async Task<IResult> Delete(int id, ProductsRepository repo)
        {
            if (id < 1) return Results.BadRequest($"{nameof(id)} must be greater than zero.");
            try
            {
                await repo.DeleteProduct(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred while deleting Product Id - {id}", ex);
                return Results
                .BadRequest($"Error Occurred while deleting Product Id - {id}: {ex}");
            }
        }
    }
}