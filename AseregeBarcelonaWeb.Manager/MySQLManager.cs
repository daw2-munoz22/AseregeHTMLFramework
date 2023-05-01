using AseregeBarcelonaWeb.Model.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace AseregeBarcelonaWeb.Manager
{
    public class MySQLManager : IDisposable //IDisposable es una interfaz que contiene el método Dispose
    {
        private MySqlConnection connection;     //preprar el objeto para conectar a nuestra base de datos   
        private string database; //definir nombre de la base de datos                
        public void Dispose() => connection.Close(); //funcion para cerrar la base de datos
        public async Task DisposeAsync() => await connection.CloseAsync(); //funcion para cerrar la base de datos

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



        public void CreateTables()
        {
            string query = $"CREATE TABLE IF NOT EXISTS {database}.roles (idroles INT NOT NULL AUTO_INCREMENT," +
                $"nombre VARCHAR(256) NULL," +
                $"type INT NULL," +
                $"PRIMARY KEY (idroles)," +
                $"UNIQUE INDEX idroles_UNIQUE (idroles ASC) VISIBLE);" +

                $"CREATE TABLE IF NOT EXISTS {database}.usuarios (idUsers INT NOT NULL AUTO_INCREMENT," +
                $"nombre VARCHAR(256) NOT NULL," +
                $"apellido VARCHAR(256) NOT NULL, edad INT NOT NULL, " +
                $"sexo VARCHAR(256) NOT NULL," +
                $"email VARCHAR(256) NOT NULL," +
                $"telefono VARCHAR(256) NULL," +
                $"passwordseguro LONGTEXT NOT NULL," +
                $"roles_idroles INT NOT NULL," +
                $"PRIMARY KEY (idUsers, roles_idroles)," +
                $"UNIQUE INDEX idUsers_UNIQUE (idUsers ASC) VISIBLE," +
                $"INDEX fk_Users_Roles_idx (roles_idroles ASC) VISIBLE," +
                $"CONSTRAINT fk_Users_Roles FOREIGN KEY (roles_idroles)" +
                $"REFERENCES {database}.roles (idroles) ON DELETE NO ACTION ON UPDATE NO ACTION);" +

                $"CREATE TABLE IF NOT EXISTS {database}.resources (idResources INT NOT NULL PRIMARY KEY AUTO_INCREMENT," +
                $"name VARCHAR(255) NOT NULL," +
                $"data LONGBLOB NOT NULL," +
                $"format VARCHAR(255) NOT NULL," +
                $"textinfo LONGTEXT NOT NULL," +
                $"date DATETIME NOT NULL);";


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
            string query = "INSERT INTO usuarios (nombre, apellido, edad,sexo,email,telefono,passwordseguro, roles_idroles) " +
                "VALUES (@nombre, @apellido, @edad, @sexo, @email, @telefono, @passwordseguro, @roles_idroles)";
            MySqlCommand command = new MySqlCommand(query, connection);

            if (user != null)
            {
                command.Parameters.AddWithValue("@nombre", user.Nombre);
                command.Parameters.AddWithValue("@apellido", user.Apellido);
                command.Parameters.AddWithValue("@edad", user.Edad);
                command.Parameters.AddWithValue("@sexo", user.Sexo);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@telefono", user.Telefono);
                command.Parameters.AddWithValue("@passwordseguro", Manager.CryptographyManager.GeneratePasswordHash(user.Passwordseguro));
                command.Parameters.AddWithValue("@roles_idroles", user.Roles_idroles);

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

            string query = "INSERT INTO roles (nombre, type) VALUES (@nombre, @type)";
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
            string query = "SELECT * FROM usuarios WHERE Nombre = @Nombre AND passwordseguro = @passwordseguro;";

            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nombre", user.Name);
            command.Parameters.AddWithValue("@passwordseguro", user.Password);

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
            command.Parameters.AddWithValue("@passwordseguro", permision.Password);

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
            string query = "UPDATE usuarios SET nombre = '@nombre', apellido= '@apellido', edad=@edad, sexo='@sexo', email='@email, telefono='@telefono', passwordseguro='@passwordseguro', roles_idroles=@idroles WHERE idUsers = @idUsers;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", nombre);
            command.Parameters.AddWithValue("@apellido", apellido);
            command.Parameters.AddWithValue("@edad", edad);
            command.Parameters.AddWithValue("@sexo", sexo);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@telefono", telefono);
            command.Parameters.AddWithValue("@passwordseguro", Manager.CryptographyManager.GeneratePasswordHash(passwordseguro));
            command.Parameters.AddWithValue("@roles_idroles", Roles_idroles);
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
        //CACA public async void DeleteUserAsync(int id)
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

        public async Task<string> InsertFile(Picture picture)
        {
            CreateTables();

            string query = "INSERT INTO resources (name, data, format, date, textinfo) VALUES (@name, @data, @format, @date, @textinfo)";
            MySqlCommand command = new MySqlCommand(query, connection);


            command.Parameters.AddWithValue("@name", picture.Name);
            command.Parameters.AddWithValue("@data", Convert.FromBase64String(picture.Data));
            command.Parameters.AddWithValue("@format", picture.Format);
            command.Parameters.AddWithValue("@date", picture.Date);
            command.Parameters.AddWithValue("@textinfo", picture.TextInfo);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await DisposeAsync();
                return "OK";
            }
            catch (MySqlException ex)
            {
                await DisposeAsync();
                return $"Error inserting the Resource File: {ex.Message}";
            }
        }

        //esta funcion permite obtener la imagen o video cuyo identificador sea el que el usuario le pida
        public async Task<Picture> SelectPicturesByIDAsync(int id)
        {
            string query = "SELECT format, data, name, textinfo FROM resources where idResources=@id;";
            await connection.OpenAsync();

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);


            Picture picture = new Picture();
            picture.Name = "NO RESOURCE";
            picture.Data = "data://";
            using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
            {

                while (await reader.ReadAsync()) //leer de manera asincronica
                {
                    string format = (string)reader[0];// tipo de fichero
                    byte[] file = (byte[])reader[1];//datos
                    string name = (string)reader[2];//datos
                    string textinfo = (string)reader[3];//datos

                    string file64 = $"data:{format};base64,{Convert.ToBase64String(file)}"; //fichero en texto uri base64                    
                    picture = new Picture()
                    {
                        Name = name,
                        Data = file64,
                        Format = format,
                        TextInfo = textinfo
                    };
                }
            }
            await connection.CloseAsync();
            return picture;
        }
        public async Task<long> SelectCountImages()
        {
            string query = "SELECT count(*) as Count FROM resources;";
            await connection.OpenAsync();
            MySqlCommand command = new MySqlCommand(query, connection);

            long Count = -1;
            using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync()) //leer de manera asincronica
                {
                    Count = (Int64)reader[0];// tipo de fichero

                }

            }
            await DisposeAsync();
            await Task.CompletedTask;
            return Count;

        }

    }
}
