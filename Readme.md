# Smartwyre Developer Test Instructions

Hansel Daza

Project Structure

Smartwyre.DeveloperTest (Solution)
│
├── Smartwyre.DeveloperTest (Project)
│   ├── Dependencies
│   │
│   ├── Data
│   │   ├── ICustomerDataStore.cs
│   │   ├── InMemoryCustomerDataStore.cs
│   │   ├── IProductDataStore.cs
│   │   ├── InMemoryProductDataStore.cs
│   │   ├── IRebateDataStore.cs
│   │   ├── InMemoryRebateDataStore.cs
│   │   ├── ITransactionDataStore.cs
│   │   └── InMemoryTransactionDataStore.cs
│   │
│   ├── Services
│   │   ├── Calculators
│   │   │   ├── IRebateCalculator.cs
│   │   │   └── RebateCalculator.cs
│   │   ├── CustomerTransactionService.cs
│   │   ├── IRebateCalculatorFactory.cs
│   │   ├── RebateCalculatorFactory.cs
│   │   └── RebateService.cs
│   │
│   ├── Types
│   │   ├── CalculateRebateRequest.cs
│   │   ├── CalculateRebateResult.cs
│   │   ├── Customer.cs
│   │   ├── IncentiveType.cs
│   │   ├── Product.cs
│   │   ├── Rebate.cs
│   │   ├── RebateCalculation.cs
│   │   ├── SupportedIncentiveType.cs
│   │   ├── Transaction.cs
│   │   └── TransactionResult.cs
│   │
├── Smartwyre.DeveloperTest.Runner (Project)
│   └── Program.cs
│
├── Smartwyre.DeveloperTest.Tests (Project)
│   ├── Data
│   │   ├── CustomerDataStoreTests.cs
│   │   ├── InMemoryCustomerDataStoreTests.cs
│   │   ├── InMemoryProductDataStoreTests.cs
│   │   ├── InMemoryRebateDataStoreTests.cs
│   │   └── InMemoryTransactionDataStoreTests.cs
│   ├── Services
│   │   ├── RebateCalculatorTests.cs
│   │   ├── CustomerTransactionServiceTests.cs
│   │   └── RebateServiceTests.cs


Key Components
Data Layer (Data/):

InMemoryDataStores: In-memory implementations for customer, product, rebate, and transaction data. These are used for testing and non-persistent scenarios.
Interfaces: Each data store implements an interface (ICustomerDataStore, IRebateDataStore, etc.), which allows for flexible, testable, and interchangeable data sources.
Services Layer (Services/):

Rebate Calculators (Calculators/): Contains the RebateCalculator and IRebateCalculator classes, which handle calculating rebates based on incentive types. The RebateCalculatorFactory provides the correct calculator based on the IncentiveType.
CustomerTransactionService.cs: Manages customer transactions such as purchases and rebate application.
RebateService.cs: The main service that calculates rebates using the rebate and product data.
Types Layer (Types/):

Business Entities: Contains domain entities such as Customer, Product, Rebate, and Transaction.
Request/Response Models: Includes models like CalculateRebateRequest and CalculateRebateResult, used for passing data between different services.
Runner Layer (Smartwyre.DeveloperTest.Runner):

Program.cs: The entry point for running the console application. This is where you can run the application and test the rebate calculations using sample data.
Test Layer (Smartwyre.DeveloperTest.Tests):

Contains unit tests for the data stores, services, and calculators to ensure the code works as expected.


How to Add a New Incentive
To add a new incentive to the system, follow these steps:

Step 1: Update the IncentiveType.cs Enum
Add your new incentive to the IncentiveType enum in IncentiveType.cs.

public enum IncentiveType
{
    FixedCashAmount,
    FixedRateRebate,
    AmountPerUom,
    NewIncentiveType // Add your new incentive type here
}

Step 2: Add the Calculation Logic to RebateCalculator.cs
Inside RebateCalculator.cs, you need to implement the calculation logic for the new incentive. Add a new method for the incentive and register it in the dictionary inside the constructor.

private decimal CalculateNewIncentiveType(Rebate rebate, Product product, CalculateRebateRequest request)
{
    // Custom logic for the new incentive type
    return rebate.Amount * request.Volume * 1.5m;  // Example logic
}

public RebateCalculator()
{
    _calculationStrategies = new Dictionary<IncentiveType, Func<Rebate, Product, CalculateRebateRequest, decimal>>
    {
        { IncentiveType.FixedCashAmount, CalculateFixedCashAmount },
        { IncentiveType.FixedRateRebate, CalculateFixedRateRebate },
        { IncentiveType.AmountPerUom, CalculateAmountPerUom },
        { IncentiveType.NewIncentiveType, CalculateNewIncentiveType } // Register new incentive
    };
}

Step 3: Register the New Incentive in RebateCalculatorFactory.cs
In the RebateCalculatorFactory.cs, register the new incentive type in the factory, so it knows which calculator to use for the new incentive.

public RebateCalculatorFactory()
{
    _calculatorRegistry = new Dictionary<IncentiveType, IRebateCalculator>
    {
        { IncentiveType.FixedCashAmount, new RebateCalculator() },
        { IncentiveType.FixedRateRebate, new RebateCalculator() },
        { IncentiveType.AmountPerUom, new RebateCalculator() },
        { IncentiveType.NewIncentiveType, new RebateCalculator() } // Register new incentive
    };
}

How to Run the Application
Open the project in Visual Studio or your preferred IDE.
Ensure the Smartwyre.DeveloperTest.Runner project is set as the Startup Project.
Run the solution (Ctrl + F5).
The console application will execute and calculate the rebates based on the hardcoded data in Program.cs.

How to Run Tests
Open the project in Visual Studio or your preferred IDE.
Ensure the Smartwyre.DeveloperTest.Tests project is included in the solution.
Open the Test Explorer (Test > Test Explorer in Visual Studio).
Click Run All to execute all the unit tests.
The results of the tests will be displayed in the Test Explorer.