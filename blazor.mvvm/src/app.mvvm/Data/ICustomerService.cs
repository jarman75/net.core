using System;
using System.Collections.Generic;

namespace app.mvvm.Data
{
    public interface ICustomerService
    {
         void AddNewCustomer(NewCustomer newCustomer);
    }

    public class CustomerService : ICustomerService
    {
        public void AddNewCustomer(NewCustomer newCustomer)
        {
            CustomersData.Customers.Add(newCustomer);
        }
    }

    public static class CustomersData 
    {
        public static List<NewCustomer> Customers = new List<NewCustomer>();                     
        
    }
}