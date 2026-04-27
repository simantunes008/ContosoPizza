using Microsoft.EntityFrameworkCore;
using ContosoPizza.Domain.Entities;

namespace ContosoPizza.Infrastructure.Data;

public class PizzaContext : DbContext
{
    public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
    {
    }

    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
}