using System.Collections.Generic;
using System.Linq;
using TwoFactorAuth.Domain.Tests.Steps;

namespace TwoFactorAuth.Domain.Tests.Helpers
{
    public static class UserHelper
    {
        public static List<User> Users = new List<User>();

        public static List<Customer> Customers = new List<Customer>();

        public static Customer GetCustomer(string customerName)
        {
            return Customers.SingleOrDefault(c => c.CustomerName == customerName);
        }

        public static Customer UpsertCustomerAndService(string customerName, string serviceName)
        {
            var customer = Customers.SingleOrDefault(x => x.CustomerName == customerName);
            if (customer != null)
            {
                if (customer.Services.All(x => x.ServiceName != serviceName))
                {
                    customer.Services.Add(new Service {
                        ServiceName = serviceName,
                        ParentCustomer = customer
                    });
                }
            }
            else
            {
                
                customer = new Customer
                {
                    CustomerName = customerName
                };

                customer.Services = new List<Service>
                {
                    new Service {ServiceName = serviceName, ParentCustomer = customer}
                };

                Customers.Add(customer);
            }

            return customer;
        }

        public static User GetUser(string username)
        {
            return Users.SingleOrDefault(x => x.Name == username);
        }

        public static void DeleteServiceForCustomer(string customerName, string serviceName)
        {
            Customers
                .SingleOrDefault(x => x.CustomerName == customerName)
                ?.Services.RemoveAll(y => y.ServiceName == serviceName);
        }

        public static User UpsertUser(string userName, string phonenumber)
        {
            var user = Users.SingleOrDefault(x => x.Name == userName);
            if (user == null)
            {
                user = new User()
                {
                    Name = userName,
                    PhoneNumber = phonenumber
                };

                Users.Add(user);
            }
            else
            {
                user.PhoneNumber = phonenumber;
            }

            return user;
        }
    }
}