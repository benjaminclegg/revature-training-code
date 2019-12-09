using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Factories;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.Singletons
{
   public class MenuSingleton
   {
      public static MenuSingleton Instance
      {
         get
         {
            return _instance;
         }
      }

      private MenuSingleton() {}

      public void Execute(UserFactory uf,
                          StoreFactory sf,
                          OrderFactory of,
                          PizzaFactory pf,
                          UserRepository _ur,
                          StoreRepository _sr,
                          OrderRepository _or,
                          PizzaRepository _pr)
      {
         bool repeatPortal = true;
         do
         {
            repeatPortal = PortalMenu(uf, sf, of, pf, _ur, _sr, _or, _pr);
         } while(repeatPortal);
         ExitProgram();
      }

      private int CheckSelection(int choices)
      {
         bool invalidInput = true;
         while(invalidInput)
         {
            Console.WriteLine("\nEnter a valid selection: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string input = Console.ReadLine();
            Console.ResetColor();
            try
            {
               int result = Int32.Parse(input);
               if(result > 0 && result <= choices)
               {
                  invalidInput = false;
                  return result;
               }
            }
            catch (FormatException) { }
         }
         return 0;
      }

      private bool CheckYesOrNo()
      {
         bool invalidInput = true;
         while(invalidInput)
         {
            Console.WriteLine("\nEnter a valid selection: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string input = Console.ReadLine();
            Console.ResetColor();
            if(input == "Y" || input == "y")
               {
                  invalidInput = false;
                  return true;
               }
            else if(input == "N" || input == "n")
            {
               invalidInput = false;
               return false;
            }
         }
         return false;
      }

      private bool PortalMenu(UserFactory uf,
                              StoreFactory sf,
                              OrderFactory of,
                              PizzaFactory pf,
                              UserRepository _ur,
                              StoreRepository _sr,
                              OrderRepository _or,
                              PizzaRepository _pr)
      {
         DisplayHeader();
         Console.WriteLine("1) Register new account");
         Console.WriteLine("2) Login");
         Console.WriteLine("3) Exit");
         Console.WriteLine("---------");
         int input = CheckSelection(3);

         switch(input)
         {
            case 1:
               RegisterMenu(uf, _ur, _sr);
               return true;
            case 2:
               LoginMenu(of, pf, _ur, _sr, _or, _pr);
               return true;
            default:
               return false;
         }
      }

      private void RegisterMenu(UserFactory uf,
                                UserRepository _ur,
                                StoreRepository _sr)
      {
         var newUser = uf.Create();

         newUser.UserType = "Customer";
         Console.WriteLine("\nEnter your first name: ");
         Console.ForegroundColor = ConsoleColor.Yellow;
         newUser.FirstName = Console.ReadLine();
         Console.ResetColor();
         Console.WriteLine("\nEnter your last name: ");
         Console.ForegroundColor = ConsoleColor.Yellow;
         newUser.LastName = Console.ReadLine();
         Console.ResetColor();
         Console.WriteLine("\nEnter your email: ");
         Console.ForegroundColor = ConsoleColor.Yellow;
         string email = Console.ReadLine();
         Console.ResetColor();
         var user = _ur.VerifyUserByEmail(email);
         if(user == null) { }
         else
         {
            Console.WriteLine("An account with that email already exists.");
            Console.WriteLine("\nPress any key to continue...");
            Console.Read();
            return;
         }
         newUser.Email = email;
         Console.WriteLine("\nEnter your phone number: ");
         Console.ForegroundColor = ConsoleColor.Yellow;
         newUser.Phone = Console.ReadLine();
         Console.ResetColor();
         Console.WriteLine("\nEnter your address: ");
         Console.ForegroundColor = ConsoleColor.Yellow;
         newUser.Address = Console.ReadLine();
         Console.ResetColor();
         Console.WriteLine("\nSelect your preferred store location: ");
         newUser.StoreID = ChooseStore(user, null, _sr);
         newUser.LastDate = DateTime.Now.Subtract(TimeSpan.FromDays(2));
         Console.WriteLine($"\nFirst name: {newUser.FirstName}");
         Console.WriteLine($"Last name: {newUser.LastName}");
         Console.WriteLine($"Email: {newUser.Email}");
         Console.WriteLine($"Phone number: {newUser.Phone}");
         Console.WriteLine($"Address: {newUser.Address}");
         Console.WriteLine($"Preferred store: Store #{newUser.StoreID}");
         Console.WriteLine("Is this information correct? Y or N");
         bool dataCorrect = CheckYesOrNo();
         if(dataCorrect)
         {
            _ur.Persist(newUser);
            Console.WriteLine("\nUser account created!");
            Console.WriteLine("Press any key to continue...");
            Console.Read();
         }
         else
         {
            Console.WriteLine("\nUser account not created.");
            Console.WriteLine("Press any key to continue...");
            Console.Read();
         }
      }

      private void ListStores(StoreRepository _sr)
      {
         var index = 0;
         foreach(Store store in _sr.StoreLibrary)
         {
            ++index;
            Console.WriteLine($"{index}) Store #{store.StoreID}, Address: {store.Address}");
         }
      }

      private void LoginMenu(OrderFactory of,
                             PizzaFactory pf,
                             UserRepository _ur,
                             StoreRepository _sr,
                             OrderRepository _or,
                             PizzaRepository _pr)
      {
         AUser user;

         DisplayHeader();
         Console.WriteLine("Enter your email:\n");
         Console.ForegroundColor = ConsoleColor.Yellow;
         string email = Console.ReadLine();
         Console.ResetColor();
         user = _ur.VerifyUserByEmail(email);
         if(user == null)
         {
            Console.WriteLine("\nThat user account does not exist.");
            Console.WriteLine("Press any key to continue...");
            Console.Read();
            return;
         }
         else
         {
            bool repeatUserMenu = true;
            do
               {
                  if(user.UserType == "Customer")
                  {
                     repeatUserMenu = CustomerMenu(user, of, pf, _ur, _sr, _or, _pr);
                  }
                  else if(user.UserType == "Manager")
                  {
                     repeatUserMenu = ManagerMenu(user, _or);
                  }
               } while(repeatUserMenu);
         }
      }

      private bool CustomerMenu(AUser user,
                                OrderFactory of,
                                PizzaFactory pf,
                                UserRepository _ur,
                                StoreRepository _sr,
                                OrderRepository _or,
                                PizzaRepository _pr)
      {
         DisplayHeader();
         Order order = of.Create();
         Console.WriteLine($"Welcome, {user.FirstName}!\n");
         Console.WriteLine($"1) Change preferred store (currently Store #{user.StoreID})");
         Console.WriteLine("2) View menu");
         Console.WriteLine("3) Create order");
         Console.WriteLine("4) View order history");
         Console.WriteLine("5) Signout");
         int input = CheckSelection(5);

         switch(input)
         {
            case 1:
               user.StoreID = ChooseStore(user, order, _sr);
               return true;
            case 2:
               ViewMenu();
               return true;
            case 3:
               bool repeatCreateOrderMenu = true;
               do
               {
                  repeatCreateOrderMenu = CreateOrderMenu(user, order, _or, pf);
               } while(repeatCreateOrderMenu);
               return true;
            case 4:
               ViewOrderHistory(user, _or);
               return true;
            case 5:
            default:
               return false;
         }
      }

      private int ChooseStore(AUser user, Order order, StoreRepository _sr)
      {
         DisplayHeader();
         ListStores(_sr);
         var input = CheckSelection(_sr.StoreLibrary.Count);
         if(order == null) { }
         else if(input == user.StoreID) { }
         else
         {
            order.UpdateID();
         }
         return input;
      }

      private void ViewMenu()
      {
         DisplayHeader();
         Console.WriteLine("1) Cheese pizza");
         Console.WriteLine("     -Price   Small: $5.99   Medium: $7.99   Large: $9.99");
         Console.WriteLine("2) Pepperoni pizza");
         Console.WriteLine("     -Price   Small: $5.99   Medium: $7.99   Large: $9.99");
         Console.WriteLine("3) Hawaiian pizza");
         Console.WriteLine("     -Price   Small: $5.99   Medium: $7.99   Large: $9.99");
         Console.WriteLine("4) MeatLovers pizza");
         Console.WriteLine("     -Price   Small: $5.99   Medium: $7.99   Large: $9.99");
         Console.WriteLine("5) Veggie pizza");
         Console.WriteLine("     -Price   Small: $5.99   Medium: $7.99   Large: $9.99");
         Console.WriteLine("\nPress any key to continue...");
         Console.Read();
      }

      private bool CreateOrderMenu(AUser user,
                                   Order order,
                                   OrderRepository _or,
                                   PizzaFactory pf)
      {
         DisplayHeader();
         //Console.WriteLine($"Order ID: {order.Id}");
         //Console.WriteLine($"Order Date: {order.OrderDate}");
         Console.WriteLine($"Ordering from Store #{user.StoreID}\n");
         Console.WriteLine("1) Add pizza to cart");
         Console.WriteLine("2) View current order");
         Console.WriteLine("3) Purchase order");
         Console.WriteLine("4) Cancel order");
         int input = CheckSelection(4);

         switch(input)
         {
            case 1:
               order = SelectPizza(order, pf);
               return true;
            case 2:
               ViewOrder(order);
               return true;
            case 3:
               PurchaseOrder(user, order, _or);
               return false;
            case 4:
            default:
               return false;
         }
      }

      private Order SelectPizza(Order order,
                                PizzaFactory pf)
      {
         DisplayHeader();
         Console.WriteLine("Select a specialty pizza:\n");
         Console.WriteLine("1) Cheese pizza");
         Console.WriteLine("2) Pepperoni pizza");
         Console.WriteLine("3) Hawaiian pizza");
         Console.WriteLine("4) MeatLovers pizza");
         Console.WriteLine("5) Veggie pizza");
         Console.WriteLine("6) Cancel");
         var type = CheckSelection(6);
         if(type == 6) return null;
         DisplayHeader();
         Console.WriteLine("Select a crust:\n");
         Console.WriteLine("1) Handtossed");
         Console.WriteLine("2) Pan");
         Console.WriteLine("3) Thin crust");
         Console.WriteLine("4) Cancel");
         var crust = CheckSelection(4);
         if(crust == 4) return null;
         DisplayHeader();
         Console.WriteLine("Select a size:\n");
         Console.WriteLine("1) Small");
         Console.WriteLine("2) Medium");
         Console.WriteLine("3) Large");
         Console.WriteLine("4) Cancel");
         var size = CheckSelection(4);
         if(size == 4) return null;
         Pizza pizza = pf.Create();
         
         switch(type)
         {
            case 1:
               pizza.Type = "Cheese";
               break;
            case 2:
               pizza.Type = "Pepperoni";
               break;
            case 3:
               pizza.Type = "Hawaiian";
               break;
            case 4:
               pizza.Type = "MeatLovers";
               break;
            case 5:
               pizza.Type = "Veggie";
               break;
         }

         switch(crust)
         {
            case 1:
               pizza.Crust = "Handtossed";
               break;
            case 2:
               pizza.Crust = "Pan";
               break;
            case 3:
               pizza.Crust = "Thin";
               break;
         }

         switch(size)
         {
            case 1:
               pizza.Size = "Small";
               pizza.Price = 5.99M;
               break;
            case 2:
               pizza.Size = "Medium";
               pizza.Price = 7.99M;
               break;
            case 3:
               pizza.Size = "Large";
               pizza.Price = 9.99M;
               break;
            case 4:
            default:
               break;
         }
         DisplayHeader();
         Console.WriteLine($"\nYou selected: {pizza.Size} {pizza.Type} pizza with {pizza.Crust} crust");
         Console.WriteLine("\nIs this correct? Y or N");
         bool dataCorrect = CheckYesOrNo();
         if(dataCorrect)
         {
            order.Pizzas.Add(pizza);
            Console.WriteLine("Your pizza has been added to your cart!");
            Console.WriteLine("\nPress any key to continue...");
            Console.Read();
            return order;
         }
         else
         {
            Console.WriteLine("Your pizza was not added to your cart.");
            Console.WriteLine("\nPress any key to continue.");
            Console.Read();
            return order;
         }
      }

      private void ViewOrder(Order order)
      {
         DisplayHeader();
         if(order.isEmpty())
         {
            Console.WriteLine("\nYour cart is empty");
         }
         else
         {
            Console.WriteLine("\nYour order in cart:\n");
            Console.WriteLine($"Order ID: {order.Id} Order Date: {order.OrderDate}");
            foreach(var pizza in order.Pizzas)
            {
               Console.WriteLine(pizza.ToString());
            }
            Console.WriteLine($"Total cost: ${order.TotalCost}");
         }
         Console.WriteLine("\nPress any key to continue...");
         Console.Read();
      }

      private void PurchaseOrder(AUser user, Order order, OrderRepository _or)
      {
         DisplayHeader();
         if(order.isEmpty())
         {
            Console.WriteLine("Your cart is empty");
         }
         else
         {
            user.LastDate = DateTime.Now;
            order.StoreId = user.StoreID;
            order.User = user.Email;

            //user.Orders.Add(order);
            _or.Persist(order);

            Console.WriteLine($"Your order {order.Id} has been shipped to Store #{user.StoreID}!");
         }
         Console.WriteLine("\nPress any key to continue...");
         Console.Read();
      }

      private void ViewOrderHistory(AUser user, OrderRepository _or)
      {
         DisplayHeader();

         List<Order> orderListByUser = _or.BuildListByUser(user);

         if(!orderListByUser.Any())
         {
            Console.WriteLine("\nYou have made no orders.");
         }
         else
         {
            Console.WriteLine("Your orders: \n");
            foreach(var item in orderListByUser)
            {
               Console.WriteLine($"{item.ToString()}");
            }
         }
         Console.WriteLine("\nPress any key to continue...");
         Console.Read();
      }

      private bool ManagerMenu(AUser user,
                               OrderRepository _or)
      {
         DisplayHeader();
         Console.WriteLine("1) View completed orders");
         Console.WriteLine("2) Signout");
         int input = CheckSelection(2);

         switch(input)
         {
            case 1:
               ViewOrdersByStore(user, _or);
               return true;
            case 2:
            default:
               return false;
         }
      }

      private void ViewOrdersByStore(AUser user, OrderRepository _or)
      {
         DisplayHeader();
         List<Order> ordersByStore = _or.RetrieveOrderByStoreID(user.StoreID);
         if(!ordersByStore.Any())
         {
            Console.WriteLine($"\nNo orders were placed at your location, Store #{user.StoreID}.");
         }
         else
         {
            foreach(Order orderItem in ordersByStore)
            {
               Console.WriteLine($"Order Id: {orderItem.Id} Order Date: {orderItem.OrderDate}\n   - Cost: ${orderItem.TotalCost} Store ID: #{orderItem.StoreId}");
            }
            Console.WriteLine("---------------------");
            Console.WriteLine($"Total Sales: ${_or.TotalSalesByLocation(ordersByStore)}");
         }
         Console.WriteLine("\nPress any key to continue...");
         Console.Read();
      }

      private void DisplayHeader()
      {
         Console.Clear();
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine("O-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-O");
         Console.ResetColor();
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine("        '||''|.   ||                          '||''|.                   ");
         Console.WriteLine("         ||   || ...  ......  ......   ....    ||   ||    ...   ... ... ");
         Console.WriteLine("         ||...|'  ||  '  .|'  '  .|'  '' .||   ||'''|.  .|  '|.  '|..'  ");
         Console.WriteLine("         ||       ||   .|'     .|'    .|' ||   ||    || ||   ||   .|.   ");
         Console.WriteLine("        .||.     .||. ||....| ||....| '|..'|' .||...|'   '|..|' .|  ||. ");
         Console.ResetColor();
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine("O-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-O\n");
         Console.ResetColor();
      }

      private void ExitProgram()
      {
         Console.WriteLine("Thank you for using PizzaBox!");
      }
   }
}