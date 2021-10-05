using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeOrganization.DAL.Models
{
    public class HomeItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int HomePhotoId { get; set; }
        public int HomeGroupId { get; set; }
        
        public virtual HomePhoto HomePhoto { get; set; }
        public virtual HomeGroup HomeGroup { get; set; }

        public virtual ICollection<GroupItemDecision> GroupItemDecisions { get; set; }
    }
}