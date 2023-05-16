namespace AseregeBarcelonaWeb.Model.Data
{
    public class User
    {
        public int ID { get; set; }
        
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        public string Nombre { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(8, ErrorMessage = "Last name length can't be more than 8.")]
        public string Apellido { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Range(0, 150, ErrorMessage = "Please enter a valid age.")]
        public int Edad { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public char Sexo { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.EmailAddress(ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string Telefono { get; set; }
     
        public string Passwordseguro { get; set; }
        public int Roles_idroles { get; set; }        
    }
}