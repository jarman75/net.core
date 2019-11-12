namespace Application.Boundaries.GetUserDetails
{
    public interface IOutputPort
        : IOutputPortStandard<GetUserDetailsOutput>, IOutputPortNotFound
    {
    }
}
