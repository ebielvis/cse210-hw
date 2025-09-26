namespace OnlineOrdering
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create addresses
            Address addr1 = new Address("12 Allen Avenue", "Ikeja", "Lagos State", "Nigeria");
            Address addr2 = new Address("5 Oxford Street", "Accra", "Greater Accra", "Ghana");

            // Create customers
            Customer cust1 = new Customer("Elvis Ebi-Johnson", addr1);
            Customer cust2 = new Customer("Kwame Mensah", addr2);

            // Create orders
            Order order1 = new Order(cust1);
            order1.AddProduct(new Product("Infinix Smartphone", "NG001", 120000, 1));
            order1.AddProduct(new Product("Power Bank", "NG002", 15000, 2));

            Order order2 = new Order(cust2);
            order2.AddProduct(new Product("Laptop Bag", "GH001", 30000, 1));
            order2.AddProduct(new Product("Wireless Mouse", "GH002", 8000, 1));
            order2.AddProduct(new Product("Bluetooth Speaker", "GH003", 20000, 1));

            // Display order info
            List<Order> orders = new List<Order> { order1, order2 };

            foreach (var order in orders)
            {
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine($"Total Price: ₦{order.GetTotalPrice()}\n");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
