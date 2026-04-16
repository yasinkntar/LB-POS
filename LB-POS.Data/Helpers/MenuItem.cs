namespace LB_POS.Data.Helpers
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Permission { get; set; }
        public List<MenuItem> Children { get; set; } = new();
    }
}
