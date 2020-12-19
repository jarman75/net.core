using System;
using app.mvvm.Data;

namespace app.mvvm.Models
{
    public class CustomerCreateViewModel : ICustomerCreateViewModel
    {
        private readonly ICustomerService _customerService;

        public CustomerCreateViewModel(ICustomerService customerService)
        {
            _customerService = customerService;            
        }

        public NewCustomerModel NewCustomer { get; set; } = new NewCustomerModel();        

        public void Create()
        {
            //map presentation model to the data layer entity
            var customer = new NewCustomer {
                CustomerNumber = Guid.NewGuid().ToString().Split('-')[0],
                FullName = $"{NewCustomer.FirstName} {NewCustomer.LastName}",
                Address = $"{NewCustomer.Address}, {NewCustomer.City}, {NewCustomer.State}, {NewCustomer.PostalCode}"
            };

            //create
            _customerService.AddNewCustomer(customer);
        }
    }
}