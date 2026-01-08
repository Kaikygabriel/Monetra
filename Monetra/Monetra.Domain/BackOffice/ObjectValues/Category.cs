using Monetra.Domain.BackOffice.Commum;

namespace Monetra.Domain.BackOffice.ObjectValues;

public class Category 
{
    protected Category()
    {
        
    }
    public Category(string name)
    {
        Name = name;
    }
    public string Name { get; set; }

    public static class Factories
    {
        public static Result<Category> Create(string name)
        {
            if (IsInvalid(name))
                return Result<Category>.Failure(new("Name.Invalid", "Name in Category invalid!"));
            return Result<Category>.Success(new (name));
        }

        public static bool IsInvalid(string name)
            => string.IsNullOrEmpty(name) || name.Length < 2;
    }
}
