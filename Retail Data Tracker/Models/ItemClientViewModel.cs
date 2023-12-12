namespace Retail_Data_Tracker.Models
{
    public class ItemClientViewModel
    {
        public List<Item> Items { get; set; }
        public List<Client> Clients { get; set; }
        public int SelectedClientId { get; set; }
    }
}
