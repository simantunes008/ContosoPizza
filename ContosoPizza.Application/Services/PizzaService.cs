using ContosoPizza.Application.Interfaces;
using ContosoPizza.Domain.Entities;
using ContosoPizza.Domain.Interfaces;

namespace ContosoPizza.Application.Services;

public class PizzaService : IPizzaService
{
    private readonly IPizzaRepository _repository;

    public PizzaService(IPizzaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Pizza>> GetAll() => await _repository.GetAllAsync();

    public async Task<Pizza?> Get(int id) => await _repository.GetByIdAsync(id);

    public async Task Add(Pizza pizza) => await _repository.AddAsync(pizza);

    public async Task Remove(int id) => await _repository.DeleteAsync(id);

    public async Task Update(Pizza pizza) => await _repository.UpdateAsync(pizza);
}