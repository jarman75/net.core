namespace Example.Api.Domain;

public class FeatureDisabledException : Exception
{
    public FeatureDisabledException(string feature) : base($"Feature {feature} is disabled.")
    {
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string entity, string value) : base($"{entity} with '{value}' id not found.")
    {
    }
}
