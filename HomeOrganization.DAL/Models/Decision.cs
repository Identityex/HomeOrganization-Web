using System.ComponentModel.DataAnnotations;

namespace HomeOrganization.DAL.Models
{
    public class Decision
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}