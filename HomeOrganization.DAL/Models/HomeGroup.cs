using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeOrganization.DAL.Models
{
    public class HomeGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<HomeGroupUsers> GroupUsers { get; set; }
    }
}