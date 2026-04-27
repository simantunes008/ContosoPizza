using ContosoPizza.Domain.Entities;
using ContosoPizza.Domain.Interfaces;
using ContosoPizza.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Infrastructure.Repositories;

public class PizzaRepository : IPizzaRepository
{
    private readonly PizzaContext _context;

    public PizzaRepository(PizzaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pizza>> GetAllAsync() =>
        await _context.Pizzas.Include(p => p.Ingredients).AsNoTracking().ToListAsync();

    public async Task<Pizza?> GetByIdAsync(int id) =>
    await _context.Pizzas.Include(p => p.Ingredients).FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Pizza pizza)
    {
        await _context.Pizzas.AddAsync(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Pizza pizza)
    {
        _context.Pizzas.Update(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);
        if (pizza != null)
        {
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
        }
    }
}