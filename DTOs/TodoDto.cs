using System.ComponentModel.DataAnnotations;

namespace todo_dotnet_api.DTOs
{
    public class TodoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık 100 karakterden uzun olamaz.")]
        public string? Title { get; set; }

        public bool IsCompleted { get; set; }
    }
}
