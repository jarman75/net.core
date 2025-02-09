using Xunit;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public Task Test1()
    {
        var person = ClassBeingTested.FindPerson();
        return Verify(person);
    }
}

public static class ClassBeingTested
{
    public static Person FindPerson() =>
        new()
        {
            Id = new Guid("ebced679-45d3-4653-8791-3d969c4a986c"),
            Title = Title.Mr,
            GivenNames = "John",
            FamilyName = "Smith",
            Spouse = "Jill",
            Children = new List<string>
            {
                "Sam",
                "Mary"
            },
            Address = new Address
            {
                Street = "4 Puddle Lane",
                Country = "USA"
            }
        };
}