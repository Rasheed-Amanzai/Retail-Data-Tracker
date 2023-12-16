namespace Retail_Data_Tracker.Models
{
    public class ItemClientViewModel
    {
        public List<Item> Items { get; set; }
        public List<Client> Clients { get; set; }
        public List<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public int SelectedClientId { get; set; }
    }
}
