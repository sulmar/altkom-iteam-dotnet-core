using System.Runtime.CompilerServices;

namespace ITeam.DotnetCore.Models
{
    public abstract class Item : BaseEntity
    {

    }

    public class Product : Item
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public decimal UnitPrice { get; set; }
        public string Color { get; set; }
        public bool IsRemoved { get; set; }
    }

    public class Service : Item
    {

    }
}
