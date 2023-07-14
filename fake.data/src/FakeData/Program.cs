// See https://aka.ms/new-console-template for more information
using FakeData.Fakers;

var customerFaker = new CustomerFaker();
var customer = customerFaker.Generate();
customer.Dump();
