using System;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Factories;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.Singletons
{
   public class MenuSingleton
   {
      private static readonly MenuSingleton _instance = new MenuSingleton();
      
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
            Console.ForegroundColor = ConsoleColor.White;
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
            string input = Console.ReadLine();
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
         var newUser = uf.Create<Customer>();

         newUser.UserType = "Customer";
         Console.WriteLine("\nEnter your first name: ");
         newUser.FirstName = Console.ReadLine();
         Console.WriteLine("\nEnter your last name: ");
         newUser.LastName = Console.ReadLine();
         Console.WriteLine("\nEnter your email: ");
         string email = Console.ReadLine();
         var user = _ur.VerifyUserByEmail(email);
         if(user == null) { }
         else
         {
            Console.WriteLine("An account with that email already exists.");
            Console.WriteLine("\nPress any key to return to portal menu.");
            Console.Read();
            return;
         }
         newUser.Email = email;
         Console.WriteLine("\nEnter your phone number: ");
         newUser.Phone = Console.ReadLine();
         Console.WriteLine("\nEnter your address: ");
         newUser.Address = Console.ReadLine();
         Console.WriteLine("\nSelect your preferred store location: ");
         newUser.StoreID = ChooseStore(user, null, _sr);
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
            Console.WriteLine("Press any key to return to portal menu.");
            Console.Read();
         }
         else
         {
            Console.WriteLine("\nUser account not created.");
            Console.WriteLine("Press any key to return to portal menu.");
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
         Console.WriteLine("Enter your email");
         string email = Console.ReadLine();
         user = _ur.VerifyUserByEmail(email);
         if(user == null)
         {
            Console.WriteLine("\nThat user account does not exist.");
            Console.WriteLine("Press any key to return to portal menu.");
            Console.Read();
            return;
         }
         else
         {
            Order order = of.Create();
            bool repeatUserMenu = true;
            do
               {
                  if(user.UserType == "Customer")
                  {
                     repeatUserMenu = CustomerMenu(user, order, pf, _ur, _sr, _or, _pr);
                  }
                  else if(user.UserType == "Manager")
                  {
                     repeatUserMenu = ManagerMenu(user, _ur, _sr, _or);
                  }
               } while(repeatUserMenu);
         }
      }

      private bool CustomerMenu(AUser user,
                                Order order,
                                PizzaFactory pf,
                                UserRepository _ur,
                                StoreRepository _sr,
                                OrderRepository _or,
                                PizzaRepository _pr)
      {
         DisplayHeader();

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
                  repeatCreateOrderMenu = CreateOrderMenu(user, order, pf);
               } while(repeatCreateOrderMenu);
               return true;
            case 4:
               //ViewOrders();
               return true;
            case 5:
            default:
               return false;
         }
      }

      private int ChooseStore(AUser user, Order order, StoreRepository _sr)
      {
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
         Console.WriteLine("\nPress any key to return to user menu.");
         Console.Read();
      }

      private bool CreateOrderMenu(AUser user,
                                   Order order,
                                   PizzaFactory pf)
      {
         DisplayHeader();
         Console.WriteLine($"Order ID: {order.Id}");
         Console.WriteLine($"Order Date: {order.OrderDate}");
         Console.WriteLine($"Ordering from Store #{user.StoreID}\n");
         Console.WriteLine("1) Add pizza to cart");
         Console.WriteLine("2) View current order");
         Console.WriteLine("3) Purchase order");
         Console.WriteLine("4) Cancel");
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
               PurchaseOrder();
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
         Console.WriteLine("Select a specialty pizza");
         Console.WriteLine("1) Cheese pizza");
         Console.WriteLine("2) Pepperoni pizza");
         Console.WriteLine("3) Hawaiian pizza");
         Console.WriteLine("4) MeatLovers pizza");
         Console.WriteLine("5) Veggie pizza");
         Console.WriteLine("6) Cancel");
         var type = CheckSelection(6);
         if(type == 6) return null;
         DisplayHeader();
         Console.WriteLine("Select a crust");
         Console.WriteLine("1) Handtossed");
         Console.WriteLine("2) Pan");
         Console.WriteLine("3) Thin crust");
         Console.WriteLine("4) Cancel");
         var crust = CheckSelection(4);
         if(crust == 4) return null;
         Console.WriteLine("Select a size");
         Console.WriteLine("1) Small");
         Console.WriteLine("2) Medium");
         Console.WriteLine("3) Large");
         Console.WriteLine("4) Cancel");
         var size = CheckSelection(3);
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
         }
         order.Pizzas.Add(pizza);
         return order;
      }

      private void ViewOrder(Order order)
      {
         if(order.isEmpty())
         {
            Console.WriteLine("Your cart is empty");
         }
         else
         {
            Console.WriteLine("\nYour order in cart:\n");
            foreach(var pizza in order.Pizzas)
            {
               Console.WriteLine(pizza.ToString());
            }
            Console.WriteLine($"Total cost: ${order.TotalCost}");
         }
         Console.WriteLine("\nPress any key to continue.");
         Console.Read();
      }

      private void PurchaseOrder()
      {

      }
      private bool ManagerMenu(AUser user,
                               UserRepository _ur,
                               StoreRepository _sr,
                               OrderRepository _or)
      {
         DisplayHeader();
         Console.WriteLine("1) View completed orders");
         Console.WriteLine("2) View sales");
         Console.WriteLine("3) View customers");
         Console.WriteLine("4) Signout");
         int input = CheckSelection(4);

         switch(input)
         {
            case 1:
               return true;
            case 2:
               return true;
            case 3:
               return true;
            case 4:
            default:
               return false;
         }
      }

      private void DisplayHeader()
      {
         Console.Clear();
         System.Console.WriteLine("Welcome to PizzaBox!");
         System.Console.WriteLine("--------------------\n");
      }

      private void ExitProgram()
      {
         //Console.Clear();
         Console.WriteLine("Thank you for using PizzaBox!");
      }
   }
}
            // Console.Clear();
            // Console.ForegroundColor = ConsoleColor.Red;
            // Console.WriteLine("o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-");
            // Console.ResetColor();
            // Console.ForegroundColor = ConsoleColor.Yellow;
            // Console.WriteLine("        '||''|.   ||                          '||''|.                   ");
            // Console.WriteLine("         ||   || ...  ......  ......   ....    ||   ||    ...   ... ... ");
            // Console.WriteLine("         ||...|'  ||  '  .|'  '  .|'  '' .||   ||'''|.  .|  '|.  '|..'  ");
            // Console.WriteLine("         ||       ||   .|'     .|'    .|' ||   ||    || ||   ||   .|.   ");
            // Console.WriteLine("        .||.     .||. ||....| ||....| '|..'|' .||...|'   '|..|' .|  ||. ");
            // Console.ResetColor();
            // Console.ForegroundColor = ConsoleColor.Red;
            // Console.WriteLine("o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-\n");
            // Console.ResetColor();