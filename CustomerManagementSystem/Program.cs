using System;
using System.ComponentModel.Design;
using System.Xml.Linq;
using Validator;

class Program
{
    public static void Main()
    {
        Console.WriteLine("hello world");
        CustomerManagementSystem c = new CustomerManagementSystem();
    }
}

class Customer
{

    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfJoining { get; set; }

    public Customer(int customerId, string name, string email, DateOnly dateOfJoining)
    {
        CustomerId = customerId;
        Name = name;
        Email = email;
        DateOfJoining = dateOfJoining;  
    }

    public override string ToString()
    {
        return $"Customer ID: {CustomerId}, Name: {Name}, Email: {Email}, Joined: {DateOfJoining.ToString("yyyy-MM-dd")}";
    }

}

class Order
{

    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public double Amount { get; set; } 
    public DateOnly OrderDate { get; set; }

    public Order(int orderId, int customrId, double amount, DateOnly orderDate)
    {
        OrderId = orderId;
        CustomerId = customrId;
        Amount = amount;
        OrderDate = orderDate;
    }

    public override string ToString()
    {
        return $"Order ID: {OrderId}, Customer ID: {CustomerId}, Amount: ${Amount}, Order Date: {OrderDate.ToString("yyyy-MM-dd")}";
    }

}

class CustomerManagementSystem
{
    
    private List<Customer> customers = new List<Customer>();
    private List<Order> orders = new List<Order>();

    public CustomerManagementSystem()
    {
        Menu();
    }

    private void Menu()
    {
        Console.Clear();
        Console.WriteLine(@"1. Add Customer
2. Add Order
3. View Customers
4. View Orders
5. Calculate Total Sales
6. Exit");

        string? key = Console.ReadLine();
        int Choice = -1;

        if (key == null || !int.TryParse(key, out Choice))
        {
            Menu();
        }

        switch (Choice)
        {
            case 1:
                customers.Add(new Customer(this.customers.Count, Validator.Validator.CheckString("Name"), Validator.Validator.GetEmail(), Validator.Validator.GetDate()));
                Menu();
                break;
            case 2:
                orders.Add(new Order(this.orders.Count, Validator.Validator.CheckInt($"CustomerId (max: {this.customers.Count-1})", this.customers.Count - 1), Validator.Validator.CheckFloat("Price"), Validator.Validator.GetDate()));
                Menu();
                break;
            case 3:
                this.ListAll("Customer List:", customers);
                Menu();
                break;
            case 4:
                this.PrintOrders();
                Menu();
                break;
            case 5:
                this.CalculateTotal();
                Menu();
                break;
            case 6:
                Environment.Exit(0);
                break;
        }
    }

    private void ListAll<T>(String Message, List<T> List)
    {
        Console.Clear();
        Console.WriteLine(Message);
        var query = from item in List
                    select item;
        foreach (var item in query)
        {
            Console.WriteLine(item.ToString());
        }

        Console.ReadLine();
    }

    private void PrintOrders()
    {
        int Choice = Validator.Validator.OneOrTwo("Print All", "Print by Customer Id", "Do you want to");
        switch (Choice)
        {
            case 1:
                this.ListAll("Order List:", orders);
                Menu();
                break;
            case 2:
                this.FindOrdersByCustomer(Validator.Validator.CheckInt($"CustomerId (max {this.customers.Count-1})", this.customers.Count-1));
                Menu();
                break;
        }
    }

    private void FindOrdersByCustomer(int customerId)
    {
        var query = orders.Where(order => order.CustomerId == customerId);
        this.ListAll($"Orders with CustomerId: {customerId}", query.ToList());
    }

    private void CalculateTotal()
    {

        var ccustomerSales = orders.GroupBy(order => order.CustomerId)
            .Select(group => new
            {
                CustomerId = group.Key,
                TotalMoney = group.Sum(order => order.Amount),
                TotalSales = group.Count()
            });

        Console.Clear();
        Console.WriteLine("Total Sales Per Customer: ");

        foreach (var c in ccustomerSales)
        {
            Console.WriteLine($"Customer Id: {c.CustomerId}, Total Sales: {c.TotalSales}, Total Dollars: {c.TotalMoney}");
        }

        Console.ReadLine();
    }
}