using System;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize data stores
            ICustomerDataStore customerDataStore = new InMemoryCustomerDataStore();
            IProductDataStore productDataStore = new InMemoryProductDataStore();
            IRebateDataStore rebateDataStore = new InMemoryRebateDataStore();
            ITransactionDataStore transactionDataStore = new InMemoryTransactionDataStore();

            // Initialize rebate calculator factory
            IRebateCalculatorFactory rebateCalculatorFactory = new RebateCalculatorFactory();

            // Initialize services
            var rebateService = new RebateService(rebateDataStore, productDataStore, rebateCalculatorFactory);
            var customerTransactionService = new CustomerTransactionService(customerDataStore, transactionDataStore, productDataStore);

            // Add some sample data
            AddSampleData(customerDataStore, productDataStore, rebateDataStore);

            // Main interaction loop
            string customerId = "Cust001"; // Simulate customer ID
            decimal totalPurchased = 0;
            decimal totalRebate = 0;
            List<Transaction> purchasedProducts = new List<Transaction>();

            while (true)
            {
                var customer = customerDataStore.GetCustomer(customerId);
                if (customer == null)
                {
                    Console.WriteLine("Customer not found.");
                    return;
                }

                // Clear the console before showing the main menu
                Console.Clear();

                // Display customer information
                Console.WriteLine($"\n--- Customer: {customer.Name}, Balance: {customer.Balance:C} ---");

                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. Buy a product");
                Console.WriteLine("2. List purchased products");
                Console.WriteLine("3. Add balance");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                // Clear the console after selecting an option
                Console.Clear();

                switch (option)
                {
                    case "1":
                        BuyProduct(customerId, customerTransactionService, rebateService, customerDataStore, ref totalPurchased, ref totalRebate, purchasedProducts);
                        break;

                    case "2":
                        ListPurchasedProducts(purchasedProducts, totalPurchased, totalRebate);
                        break;

                    case "3":
                        AddBalance(customerId, customerDataStore);
                        break;

                    case "4":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                // Wait for user input before clearing the screen again
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
            }
        }

        static void BuyProduct(string customerId, CustomerTransactionService transactionService, RebateService rebateService,
                               ICustomerDataStore customerDataStore, ref decimal totalPurchased, ref decimal totalRebate, List<Transaction> purchasedProducts)
        {
            Console.WriteLine("\n--- Products ---");
            Console.WriteLine("1. Laptop ($200)");
            Console.WriteLine("2. Smartphone ($100)");
            Console.Write("Select a product to buy: ");
            var productOption = Console.ReadLine();

            string productId = null;
            decimal price = 0;
            switch (productOption)
            {
                case "1":
                    productId = "Prod001";
                    price = 200;
                    break;

                case "2":
                    productId = "Prod002";
                    price = 100;
                    break;

                default:
                    Console.WriteLine("Invalid product selection.");
                    return;
            }

            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            // Calculate rebate
            var rebateRequest = new CalculateRebateRequest
            {
                RebateIdentifier = productId == "Prod001" ? "Reb001" : "Reb002",
                ProductIdentifier = productId,
                Volume = quantity
            };
            var rebateResult = rebateService.Calculate(rebateRequest);
            decimal rebateAmount = rebateResult.Success ? rebateResult.RebateAmount : 0;

            // Total cost before rebate
            decimal totalCostBeforeRebate = price * quantity;

            // Total cost after rebate
            decimal totalCostAfterRebate = totalCostBeforeRebate - rebateAmount;
            if (totalCostAfterRebate < 0)
            {
                totalCostAfterRebate = 0; // No negative cost
            }

            // Process purchase (check if the user has enough balance)
            var customer = customerDataStore.GetCustomer(customerId);
            if (customer.Balance >= totalCostAfterRebate)
            {
                customer.Balance -= totalCostAfterRebate;
                customerDataStore.UpdateCustomerBalance(customer);

                // Add to purchased products and totals
                purchasedProducts.Add(new Transaction { ProductId = productId, Quantity = quantity, TotalAmount = totalCostBeforeRebate, RebateAmount = rebateAmount });
                totalPurchased += totalCostBeforeRebate;
                totalRebate += rebateAmount;

                Console.WriteLine($"Purchase successful. Remaining balance: {customer.Balance:C}");
                Console.WriteLine($"Rebate for this purchase: {rebateAmount:C}");
                Console.WriteLine($"Total cost after rebate: {totalCostAfterRebate:C}");
            }
            else
            {
                Console.WriteLine($"Purchase failed. Insufficient balance. Total cost: {totalCostAfterRebate:C}, Current balance: {customer.Balance:C}");
            }
        }

        static void ListPurchasedProducts(List<Transaction> purchasedProducts, decimal totalPurchased, decimal totalRebate)
        {
            Console.WriteLine("\n--- Purchased Products ---");
            if (purchasedProducts.Count == 0)
            {
                Console.WriteLine("No products purchased yet.");
                return;
            }

            foreach (var transaction in purchasedProducts)
            {
                Console.WriteLine($"Product: {transaction.ProductId}, Quantity: {transaction.Quantity}, Total: {transaction.TotalAmount:C}, Rebate: {transaction.RebateAmount:C}");
            }

            Console.WriteLine($"\nTotal Purchased: {totalPurchased:C}");
            Console.WriteLine($"Total Rebate: {totalRebate:C}");
        }

        static void AddBalance(string customerId, ICustomerDataStore customerDataStore)
        {
            var customer = customerDataStore.GetCustomer(customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.Write("Enter amount to add: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amountToAdd) || amountToAdd <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            customer.Balance += amountToAdd;
            customerDataStore.UpdateCustomerBalance(customer);

            Console.WriteLine($"Balance updated. New balance: {customer.Balance:C}");
        }

        static void AddSampleData(ICustomerDataStore customerDataStore, IProductDataStore productDataStore, IRebateDataStore rebateDataStore)
        {
            // Add some sample customers
            customerDataStore.AddCustomer(new Customer { CustomerId = "Cust001", Name = "John Doe", Balance = 1000m });
            customerDataStore.AddCustomer(new Customer { CustomerId = "Cust002", Name = "Jane Smith", Balance = 500m });

            // Add some sample products
            productDataStore.AddProduct(new Product
            {
                ProductIdentifier = "Prod001",
                Name = "Laptop",
                Price = 200m,
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            });

            productDataStore.AddProduct(new Product
            {
                ProductIdentifier = "Prod002",
                Name = "Smartphone",
                Price = 100m,
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            });

            // Add some sample rebates
            rebateDataStore.AddRebate(new Rebate
            {
                RebateIdentifier = "Reb001",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 20m
            });

            rebateDataStore.AddRebate(new Rebate
            {
                RebateIdentifier = "Reb002",
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = 0.10m
            });
        }
    }
}
