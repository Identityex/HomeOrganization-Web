using System.ComponentModel.DataAnnotations;

namespace HomeOrganization.DAL.Models
{
    public class HomePhoto
    {
        [Key]
        public int Id { get; set; }
        public string ImageData { get; set; }
        public string ImageType { get; set; }
        public string BlobHash { get; set; }
    }
}