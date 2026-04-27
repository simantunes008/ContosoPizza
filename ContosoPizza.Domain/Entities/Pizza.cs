namespace ContosoPizza.Domain.Entities;

public class Pizza
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsGlutenFree { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
}