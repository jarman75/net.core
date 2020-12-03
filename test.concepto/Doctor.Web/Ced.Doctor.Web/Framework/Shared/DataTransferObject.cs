namespace Framework.Shared
{
    /// <summary>
    /// DTO (Data Transfer objects) is a data container for moving data between layers. They are also termed as transfer objects. DTO is only used to pass data and does not contain any business logic. They only have simple setters and getters.    
    /// </summary>
    public class DataTransferObject
    {
        public int Id { get; set; }
    }
}
