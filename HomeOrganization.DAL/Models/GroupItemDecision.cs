using System.ComponentModel.DataAnnotations;

namespace HomeOrganization.DAL.Models
{
    public class GroupItemDecision
    {
        [Key]
        public int Id { get; set; }
        public string HomeUserId { get; set; }
        public int HomeItemId { get; set; }
        public int DecisionId { get; set; }
        
        public virtual HomeUser HomeUser { get; set; }
        public virtual HomeItem HomeItem { get; set; }
        public virtual  Decision Decision { get; set; }
    }
}