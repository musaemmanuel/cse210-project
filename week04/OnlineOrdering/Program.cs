using System;
using System.Collections.Generic;

public class Address
{
    private string StreetAddress { get; set; }
    private string City { get; set; }
    private string State { get; set; }
    private string Country { get; set; }

    public Address(string streetAddress, string city, string state, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        State = state;
        Country = country;
    }

    public bool IsInNIGERIA() => Country.ToLower() == "Nigeria";

    public string GetFullAddress()
    {
        return $"{StreetAddress}\n{City}, {State}\n{Country}";
    }
}

public class Customer
{
    private string Name { get; set; }
    private Address Address { get; set; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool LivesInUSA() => Address.IsInNIGERIA();

    public string GetShippingLabel()
    {
        return $"{Name}\n{Address.GetFullAddress()}";
    }
}

public class Product
{
    private string Name { get; set; }
    private string ProductId { get; set; }
    private double Price { get; set; }
    private int Quantity { get; set; }

    public Product(string name, string productId, double price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public double GetTotalCost() => Price * Quantity;

    public string GetPackingLabel()
    {
        return $"{Name} (ID: {ProductId})";
    }
}

public class Order
{
    private List<Product> Products { get; set; } = new List<Product>();
    private Customer Customer { get; set; }

    public Order(Customer customer)
    {
        Customer = customer;
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double productTotal = 0;
        foreach (var product in Products)
        {
            productTotal += product.GetTotalCost();
        }
        double shippingCost = Customer.LivesInUSA() ? 5 : 35;
        return productTotal + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in Products)
        {
            label += product.GetPackingLabel() + "\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{Customer.GetShippingLabel()}";
    }
}

public class Program
{
    public static void Main()
    {
        Address address1 = new Address("123 Apple St", "ABUJA FCT", "NY", "NIGERIA");
        Address address2 = new Address("456 Maple Ave", "Lagos", "ON", "Kano");

        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "P001", 1200, 1));
        order1.AddProduct(new Product("Mouse", "P002", 20, 2));

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Phone", "P003", 800, 1));
        order2.AddProduct(new Product("Charger", "P004", 25, 1));

        List<Order> orders = new List<Order> { order1, order2 };

        foreach (var order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order.CalculateTotalCost()}");
            Console.WriteLine();
        }
    }
}
