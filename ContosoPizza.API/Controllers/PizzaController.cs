using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Application.Interfaces;
using ContosoPizza.Domain.Entities;

namespace ContosoPizza.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IPizzaService _pizzaService;

    // O ASP.NET vai injetar o Service automaticamente aqui
    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pizza>>> GetAll()
    {
        var pizzas = await _pizzaService.GetAll();
        return Ok(pizzas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        var pizza = await _pizzaService.Get(id);
        if (pizza == null) return NotFound();
        return Ok(pizza);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pizza pizza)
    {
        await _pizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = await _pizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        await _pizzaService.Update(pizza);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pizza = await _pizzaService.Get(id);
        if (pizza is null)
            return NotFound();

        await _pizzaService.Remove(id);
        return NoContent();
    }
}