namespace OnlineOrdering
{
    public class Customer
    {
        private string name;
        private Address address;

        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public bool IsInNigeria()
        {
            return address.IsInNigeria();
        }

        public string GetName()
        {
            return name;
        }

        public string GetAddressString()
        {
            return address.GetFullAddress();
        }
    }
}
