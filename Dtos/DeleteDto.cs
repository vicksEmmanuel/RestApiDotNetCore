using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos {
    public class DeleteDto {
        [Required]
        public int Status { get; set; }
        [Required]
        public string Message { get; set; }
    }
}