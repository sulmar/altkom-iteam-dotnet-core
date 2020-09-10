using System;

namespace ITeam.DotnetCore.Models
{
    public abstract class BaseEntity : Base
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
