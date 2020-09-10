namespace ITeam.DotnetCore.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public bool IsRemoved { get; set; }
    }
}
