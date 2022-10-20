using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            UserManager userManager = new UserManager(new EfUserDal());
            Customer customer1 = new Customer();
            User user1 = new User();
            user1.Id = 1;
            user1.FirstName = "Samet";
            user1.LastName = "Tunay";
            user1.Email = "samet.tunay12@gmail.com";
            user1.Password = "123456";
            customer1.Id = 1;
            customer1.UserId = 1;
            customer1.CompanyName = "Inovasyon & Yazılım";
            //userManager.Add(user1);
            //customerManager.Add(customer1);
            foreach (var customer in customerManager.GetAll().Data)
            {
                Console.WriteLine(customer.CompanyName);
            }
            foreach (var user in userManager.GetAll().Data)
            {
                Console.WriteLine(user.FirstName);
            }
        }

        private static void DetailsDemo()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            if (result.Success == true)
            {
                foreach (var car in carManager.GetCarDetails().Data)
                {
                    Console.WriteLine(car.CarName + " / " + car.ColorName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
