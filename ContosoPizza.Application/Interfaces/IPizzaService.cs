using ContosoPizza.Domain.Entities;

namespace ContosoPizza.Application.Interfaces;

public interface IPizzaService
{
    Task<IEnumerable<Pizza>> GetAll();
    Task<Pizza?> Get(int id);
    Task Add(Pizza pizza);
    Task Remove(int id);
    Task Update(Pizza pizza);
}