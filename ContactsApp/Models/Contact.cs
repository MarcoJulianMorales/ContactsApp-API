using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        [StringLength(18)]
        public string CURP { get; set; }
    }
}
