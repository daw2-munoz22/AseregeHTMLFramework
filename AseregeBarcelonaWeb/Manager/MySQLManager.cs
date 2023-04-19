using Microsoft.VisualBasic;
using Model.Data;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AseregeBarcelonaWeb.Manager
{
    public class MySQLManager : IDisposable //IDisposable es una interfaz que contiene el método Dispose
    {
        private MySqlConnection connection;     //preprar el objeto para conectar a nuestra base de datos   
        private string database; //definir nombre de la base de datos                
        public void Dispose() => connection.Close(); //funcion para cerrar la base de datos

        public MySQLManager()
        {
            #if DEBUG
            string CONFIG_DB_NAME = "aserege.json";
            #else
            string CONFIG_DB_NAME = "aserege_release.json";
            #endif
            if (!File.Exists(CONFIG_DB_NAME)) //si no existe el fichero, muestra por consola el mensaje
            {
                Console.WriteLine("Necesitas aserege.json para realizar una conexion con la base de datos");
            }
            else
            {
                //en caso contrario, lee el fichero, parsea el contenido de aserege.json
                //a la clase AseregeConfiguration y luego lo almacena en la variable config
                //Al acabar, construye la cadena de conexión

                AseregeConfiguration config = PostJsonManager.GetJsonResult<AseregeConfiguration>(File.ReadAllText("aserege.json"));

                BuildConnection(config.hostname, config.databaseName, config.username, config.password);                
            }
        }
        //función que recibe los paramentos y construye la cadena de conexión
        public void BuildConnection(string server, string database, string username, string password)
        {
            this.database = database;
            string connectionString = $"Server={server};database={database};UID={username};password={password};";
            connection = new MySqlConnection(connectionString);
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
            string query = $"CREATE TABLE IF NOT EXISTS {database}.Roles (idroles INT NOT NULL AUTO_INCREMENT," +
                $"nombre VARCHAR(256) NULL," +
                $"type INT NULL," +
                $"PRIMARY KEY (idroles)," +
                $"UNIQUE INDEX idroles_UNIQUE (idroles ASC) VISIBLE);" +

                $"CREATE TABLE IF NOT EXISTS {database}.Usuarios (idUsers INT NOT NULL AUTO_INCREMENT, nombre VARCHAR(256) NOT NULL," +
                $"apellido VARCHAR(256) NOT NULL, edad INT NOT NULL, sexo VARCHAR(256) NOT NULL," +
                $"email VARCHAR(256) NOT NULL," +
                $"telefono VARCHAR(256) NULL," +
                $"passwordseguro LONGTEXT NOT NULL," +
                $"Roles_idroles INT NOT NULL," +
                $"PRIMARY KEY (idUsers, Roles_idroles)," +
                $"UNIQUE INDEX idUsers_UNIQUE (idUsers ASC) VISIBLE," +
                $"INDEX fk_Users_Roles_idx (Roles_idroles ASC) VISIBLE," +
                $"CONSTRAINT fk_Users_Roles FOREIGN KEY (Roles_idroles)" +
                $"REFERENCES {database}.Roles (idroles) ON DELETE NO ACTION ON UPDATE NO ACTION)";


            MySqlCommand command = new MySqlCommand(query, connection);//preparar la query
            try
            {
                connection.Open();
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
            string query = "INSERT INTO usuarios (nombre, apellido, edad,sexo,email,telefono,passwordseguro, Roles_idroles) " +
                "VALUES (@nombre, @apellido, @edad, @sexo, @email, @telefono, @passwordseguro, @Roles_idroles)";
            MySqlCommand command = new MySqlCommand(query, connection);

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
                    connection.Open();
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
            MySqlCommand command = new MySqlCommand(query, connection);
            if (role != null)
            {
                command.Parameters.AddWithValue("@nombre", role.Nombre);
                command.Parameters.AddWithValue("@type", role.Type);

                try
                {
                    connection.Open();
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
        public User GetUser(Authorize user)
        {
            //string query = "SELECT * FROM usuarios WHERE Roles_idroles = 1;";
            string query = "SELECT * FROM usuarios WHERE Nombre = @Nombre AND passwordseguro = @passwordseguro;";

            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nombre", user.Name);
            command.Parameters.AddWithValue("@passwordseguro", GeneratePasswordHash(user.Password));

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    User userData = new User()
                    {
                        ID = Convert.ToInt32(reader[0]),
                        Nombre = reader[1].ToString(),
                        Apellido = reader[2].ToString(),
                        Edad = Convert.ToInt32(reader[3]),
                        Sexo = Convert.ToChar(reader[4]),
                        Email = Convert.ToString(reader[5]),
                        Telefono = Convert.ToString(reader[6]),
                        Passwordseguro = Convert.ToString(reader[7]),
                        Roles_idroles = Convert.ToInt32(reader[8])
                    };

                    Dispose();
                    return userData;
                }

            }
            Dispose();
            return null;
        }
        public bool Login(Authorize permision)
        {
            string query = "SELECT COUNT(*) from usuarios where Nombre = @Nombre and passwordseguro = @passwordseguro";
            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nombre", permision.Name);
            command.Parameters.AddWithValue("@passwordseguro", GeneratePasswordHash(permision.Password));

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bool Valid = Convert.ToBoolean(reader[0]);                    

                    if (Valid)
                    {
                        Dispose();
                        return true;
                    }
                }
                Dispose();
            }
            Dispose();
            return false;
        }


        public List<User> SelectUsers()
        {
            string query = "SELECT * FROM usuarios;";
            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);

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
                    user.Sexo = reader[4].ToString().ToArray()[0];
                    user.Email = (string)reader[5];
                    user.Telefono = (string)reader[6];
                    user.Passwordseguro = (string)reader[7];
                    user.Roles_idroles = (int)reader[8];

                    Users.Add(user);
                }

            }
            return Users;
        }


        public async Task<List<User>> SelectUsersAsync()
        {
            string query = "SELECT * FROM usuarios;";
            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);

            List<User> Users = new List<User>();
            using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    User user = new User();


                    user.ID = (int)reader[0];
                    user.Nombre = (string)reader[1];
                    user.Apellido = (string)reader[2];
                    user.Edad = (int)reader[3];
                    user.Sexo = reader[4].ToString().ToArray()[0];
                    user.Email = (string)reader[5];
                    user.Telefono = (string)reader[6];
                    user.Passwordseguro = (string)reader[7];
                    user.Roles_idroles = (int)reader[8];

                    Users.Add(user);
                }

            }
            return Users;
        }

     
        public async Task UpdateUserAsync(string nombre, string apellido, int edad, char sexo, string email, string telefono, string passwordseguro, int Roles_idroles, int idUsers)
        {
            string query = "UPDATE usuarios SET nombre = '@nombre', apellido= '@apellido', edad=@edad, sexo='@sexo', email='@email, telefono='@telefono', passwordseguro='@passwordseguro', Roles_idroles=@idroles WHERE idUsers = @idUsers;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", nombre);
            command.Parameters.AddWithValue("@apellido", apellido);
            command.Parameters.AddWithValue("@edad", edad);
            command.Parameters.AddWithValue("@sexo", sexo);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@telefono", telefono);            
            command.Parameters.AddWithValue("@passwordseguro", GeneratePasswordHash(passwordseguro));
            command.Parameters.AddWithValue("@Roles_idroles", Roles_idroles);
            command.Parameters.AddWithValue("@idUsers", idUsers);

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error updating the user: " + ex.Message);
            }
        }


        //Función para la query de eliminar usuarios
        //CACA// public async void DeleteUserAsync(int id)
        //GOOD private void DeleteUser(int id)
        //GOOD private Task DeleteUserAsync(int id)
        public async Task DeleteUserAsync(int id)
        {            
            string query = "DELETE FROM usuarios WHERE idUsers=@id;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();                
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromException(ex); //devolver el estado del error (excepcion)                
            }
        }     
    }
}
