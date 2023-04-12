using Model;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AseregeEdgar.Manager
{
    public class MySQLManager : IDisposable //IDisposable es una interfaz que contiene el método Dispose
    {
        private MySqlConnection connection;     //preprar el objeto para conectar a nuestra base de datos   
        private string database; //definir nombre de la base de datos
        

        public void Dispose()
        {
            this.connection.Close(); //funcion para cerrar la base de datos
        }
        
        public MySQLManager()
        {
            if (!File.Exists("aserege.json")) //si no existe el fichero, muestra por consola el mensaje
            {                
                Console.WriteLine("Necesitas aserege.json para realizar una conexion con la base de datos");                
            }
            else 
            {
                //en caso contrario, lee el fichero, parsea el contenido de aserege.json
                //a la clase AseregeConfiguration y luego lo almacena en la variable config
                //Al acabar, construye la cadena de conexión
                
                AseregeConfiguration config = PostJson.GetJsonResult<AseregeConfiguration>(File.ReadAllText("aserege.json"));
                
                BuildConnection(config.hostname, config.databaseName, config.username, config.password);
            }            
        }
        //función que recibe los paramentos y construye la cadena de conexión
        public void BuildConnection (string server, string database, string username, string password)
        {            
            this.database = database;            
            string connectionString = $"Server={server};database={database};UID={username};password={password};";
            this.connection = new MySqlConnection(connectionString);
        }       
        
        //generar el sistema de cifrado
        public string GeneratePasswordHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input); //los bytes del texto, estan representados en la codificación UTF-8
                byte[] hashBytes = sha256.ComputeHash(inputBytes); //el texto, se convierte en la clave segura que se guardara en la base de datos
                 
                StringBuilder builder = new StringBuilder(); //convierte el hash anterior (binario) a un texto
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // por cada ciclo, añade cada letra al "ArrayList"
                    //el 2 es el espacio
                }
                return builder.ToString(); //convertimos a un texto normal
            }            
        }

        public void CreateTables()
        {                            
            string query = $"CREATE TABLE IF NOT EXISTS {this.database}.Roles (idroles INT NOT NULL AUTO_INCREMENT," +
                $"nombre VARCHAR(256) NULL," +
                $"type INT NULL," +
                $"PRIMARY KEY (idroles)," +
                $"UNIQUE INDEX idroles_UNIQUE (idroles ASC) VISIBLE);" +
                
                $"CREATE TABLE IF NOT EXISTS {this.database}.Usuarios (idUsers INT NOT NULL AUTO_INCREMENT, nombre VARCHAR(256) NOT NULL," +
                $"apellido VARCHAR(256) NOT NULL, edad INT NOT NULL, sexo VARCHAR(256) NOT NULL," +
                $"email VARCHAR(256) NOT NULL," +
                $"telefono VARCHAR(256) NULL," +
                $"passwordseguro LONGTEXT NOT NULL," +
                $"Roles_idroles INT NOT NULL," +
                $"PRIMARY KEY (idUsers, Roles_idroles)," +
                $"UNIQUE INDEX idUsers_UNIQUE (idUsers ASC) VISIBLE," +
                $"INDEX fk_Users_Roles_idx (Roles_idroles ASC) VISIBLE," +
                $"CONSTRAINT fk_Users_Roles FOREIGN KEY (Roles_idroles)" +
                $"REFERENCES {this.database}.Roles (idroles) ON DELETE NO ACTION ON UPDATE NO ACTION)";

            
            MySqlCommand command = new MySqlCommand(query, this.connection);//preparar la query
            try
            {                
                this.connection.Open();
                command.ExecuteNonQuery();  //ejecutamos la consulta de manera sincronica              
            }
            catch (MySqlException ex)
            {                
                Console.WriteLine("Error creating the table: " + ex.Message);
            }
            Dispose();//cerrar conexión
        }

        public string InsertUser(User user)
        {
            CreateTables();
            //el @  representa los parametros
            string query = "INSERT INTO Usuarios (nombre, apellido, edad,sexo,email,telefono,passwordseguro, Roles_idroles) " +
                "VALUES (@nombre, @apellido, @edad, @sexo, @email, @telefono, @passwordseguro, @Roles_idroles)";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            
            if (user != null) 
            {
                command.Parameters.AddWithValue("@nombre", user.Nombre);
                command.Parameters.AddWithValue("@apellido", user.Apellido);
                command.Parameters.AddWithValue("@edad", user.Edad);
                command.Parameters.AddWithValue("@sexo", user.Sexo);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@telefono", user.Telefono);
                command.Parameters.AddWithValue("@passwordseguro", GeneratePasswordHash(user.Passwordseguro));
                command.Parameters.AddWithValue("@Roles_idroles", user.Roles_idroles);

                try
                {
                    this.connection.Open();
                    command.ExecuteNonQuery();
                    Dispose();
                    return "OK";
                }
                catch (MySqlException ex)
                {
                    Dispose();
                    return $"Error inserting the user: {ex.Message}";
                }
            } 
            else
            {
                Dispose();
                return $"Ilegal user !";                
            }                       
        }
        public string InsertRole(Role role)
        {
            CreateTables();
            
            string query = "INSERT INTO Roles (nombre, type) VALUES (@nombre, @type)";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            if (role != null)
            {
                command.Parameters.AddWithValue("@nombre", role.Nombre);
                command.Parameters.AddWithValue("@type", role.Type);

                try
                {
                    this.connection.Open();
                    command.ExecuteNonQuery();
                    Dispose();
                    return "OK";
                }
                catch (MySqlException ex)
                {
                    Dispose();
                    return $"Error inserting the user: {ex.Message}";
                }                
            }
            else 
            {
                Dispose();
                return $"Ilegal role !";
            }
            
        }

        public bool Login(Authorize permision)
        {
            string query = "SELECT COUNT(*) from usuarios where Nombre = @Nombre and passwordseguro = @passwordseguro";
            this.connection.Open();

            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@Nombre", permision.Name);            
            command.Parameters.AddWithValue("@passwordseguro", GeneratePasswordHash(permision.Password));
            
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bool Valid = Convert.ToBoolean(reader[0]);                                        
                    Dispose();
                    return Valid;                    
                }

            }
            Dispose();
            return false;
        }                    
        public List<User> SelectUsers()
        {
            string query = "SELECT * FROM usuarios;";
            this.connection.Open();

            MySqlCommand command = new MySqlCommand(query, this.connection);
            
            List<User> Users = new List<User>();
            using (MySqlDataReader reader = command.ExecuteReader())
            {                          
                while (reader.Read())
                {
                    User user = new User();


                    user.ID = (int)reader[0];
                    user.Nombre = (string)reader[1];
                    user.Apellido = (string)reader[2];
                    user.Edad = (int)reader[3];
                    user.Sexo = (char)reader[4].ToString().ToArray()[0];
                    user.Email = (string)reader[5];
                    user.Telefono = (string)reader[6];
                    user.Passwordseguro = (string)reader[7];
                    user.Roles_idroles = (int)reader[8];
                    
                    Users.Add(user);
                }
                
            }
            return Users;
        }
        
        /*ESTO NO HECHO*/
        public void UpdateUser(int id, string name, string email, string password)
        {
            string query = "UPDATE users SET name=@name, email=@email, password=@password WHERE id=@id";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error updating the user: " + ex.Message);
            }
        }

        public void DeleteUser(int id)
        {
            string query = "DELETE FROM users WHERE id=@id";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error deleting the user: " + ex.Message);
            }
        }     
    }
}
