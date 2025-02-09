namespace Tests;

public class Person
{
    public Guid Id { get; set; }
    public Title Title { get; set; }
    public string? GivenNames { get; set; }
    public string? FamilyName { get; set; }
    public string? Spouse { get; set; }
    public List<string>? Children { get; set; }
    public Address? Address { get; set; }
}

public class Address
{
    public string? Street { get; set; }
    public string? Country { get; set; }
}

public enum Title
{
    Mr,
    Mrs,
    Ms,
    Dr
}


