using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeOrganization.DAL.Models
{
    public class HomeGroupUsers
    {
        [Key]
        public int Id { get; set; }
        public string HomeUserId { get; set; }
        public int HomeGroupId { get; set; }
        
        public virtual HomeUser HomeUser { get; set; }
        public virtual HomeGroup HomeGroup { get; set; }
    }
}