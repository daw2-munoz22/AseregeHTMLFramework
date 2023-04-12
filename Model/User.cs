namespace Model
{
    public class User
    {               
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public char Sexo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Passwordseguro { get; set; }
        public int Roles_idroles { get; set; }        
    }
}