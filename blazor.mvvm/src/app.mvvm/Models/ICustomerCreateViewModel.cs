namespace app.mvvm.Models
{
    public interface ICustomerCreateViewModel
    {
         NewCustomerModel NewCustomer {get; set;}
         void Create();
    }
}