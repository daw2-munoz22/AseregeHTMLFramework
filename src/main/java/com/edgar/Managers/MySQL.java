package com.edgar.Managers;
import com.edgar.Model.Role;
import com.edgar.Model.Usuario;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.*;
import java.util.ArrayList;



/**
 * 
 * @author Edgar Muñoz
 */
public class MySQL {
    private static Connection conexion;
     //la conexion generada es esta -> jdbc:mysql://localhost:3306/world
     private String url = "";        
     private String username = "";        
     private String password = "";
     private String databaseName = "";
     
    //constructor de la conexión a la base de datos MySQL
    public MySQL(String hostname, int port, String username, String password, String databaseName) throws Exception{
           
        url = String.format("jdbc:mysql://%s:3306/%s", hostname,databaseName);        
        this.username = username;
        this.password = password;  
        this.databaseName = databaseName;
    }  
    //creación de la tabla roles
    
    protected void CreateRoleTable() throws SQLException {
        try {
            conexion = DriverManager.getConnection(url, username, password);            
            String Query = String.format("CREATE TABLE IF NOT EXISTS `%s`.`Roles` (`idroles` INT NOT NULL AUTO_INCREMENT, `nombre` VARCHAR(256) NULL, `type` INT NULL, PRIMARY KEY (`idroles`), UNIQUE INDEX `idroles_UNIQUE` (`idroles` ASC) VISIBLE);", databaseName);                                
            Statement st = conexion.createStatement();
            st.executeUpdate(Query);
            conexion.close();
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
            conexion.close();
        }        
    }
    //añadir los roles a la tabla
    public void InsertRole(String nombre, int type) throws SQLException {
        
       //  Evitamos la inyecion SQL en la tabla Roles
        try {
            conexion = DriverManager.getConnection(url, username, password);            
                          
            String query = "INSERT INTO Roles (nombre,type) VALUES (?,?)";        
            PreparedStatement statement = conexion.prepareStatement(query);        
            statement.setString(1, nombre);       
            statement.setInt(2, type);            
            statement.executeUpdate();
            conexion.close();
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
            conexion.close();
        }        
    }     
    public Role SelectRole(int permisionlevel) throws SQLException {
        Role DBRole = null;
        try {
            conexion = DriverManager.getConnection(url, username, password);
            String query = "SELECT idroles, nombre, type FROM roles WHERE type = ?;";

            PreparedStatement statement = conexion.prepareStatement(query);
            statement.setInt(1, permisionlevel);

            ResultSet resultSet = statement.executeQuery(query);

            //MIENTRAS se va ejecutando, va extrayendo los datos de la base de datos
            while (resultSet.next()) {
                int idroles = resultSet.getInt("idroles");
                String name = resultSet.getString("nombre");
                int type = resultSet.getInt("type");
                //devolver el objeto tipo rol cuyos datos proceden de la DB
                DBRole = new Role(idroles, name, type);
            }
            conexion.close();
            return DBRole;//devolver el rol
        } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
            conexion.close();
        }
        return null;//No hay roles o la base de datos falla
    }
    public ArrayList<Role> SelectRoles() throws SQLException {
        ArrayList<Role> DBRole = new ArrayList<Role>();
        try {
            conexion = DriverManager.getConnection(url, username, password);            
            String query = "SELECT * FROM roles;";
            
            PreparedStatement statement = conexion.prepareStatement(query);                                            
            ResultSet resultSet = statement.executeQuery(query);
            
            //MIENTRAS se va ejecutando, va extrayendo los datos de la base de datos
            while (resultSet.next()) {                
                int idroles = resultSet.getInt("idroles");
                String name = resultSet.getString("nombre"); 
                int type = resultSet.getInt("type");
                //devolver el objeto tipo rol cuyos datos proceden de la DB
                DBRole.add(new Role(idroles, name, type));
            }
            conexion.close();
            return DBRole;//devolver el rol
            
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
            conexion.close();
        }    
        return null;//No hay roles o la base de datos falla
    }
    public ArrayList<Usuario> SelectUsuarios() throws SQLException {
        ArrayList<Usuario> userlist = new ArrayList<Usuario>();         
        try {
            conexion = DriverManager.getConnection(url, username, password);            
            String query = "SELECT * from Usuarios;";
            
            PreparedStatement statement = conexion.prepareStatement(query);                    
            ResultSet resultSet = statement.executeQuery(query);
            
            //MIENTRAS se va ejecutando, va extrayendo los datos de la base de datos
            while (resultSet.next()) {                
                int idUsuario = resultSet.getInt("idUsers");
                String nombre = resultSet.getString("nombre"); 
                String apellido = resultSet.getString("apellido");
                int edad = resultSet.getInt("edad");
                String sexo = resultSet.getString("sexo");
                String email = resultSet.getString("email");
                String telefono = resultSet.getString("telefono");
                String Roles_idroles = resultSet.getString("Roles_idroles");
                userlist.add(new Usuario(idUsuario, nombre, apellido, edad, sexo.toCharArray()[0], null));                
            }
            conexion.close();
        } catch (SQLException e) {                                          
             System.out.println("Error: " + e.getMessage());
            conexion.close();
         }           
        return userlist;
    }
    
    //eliminar datos
    private void Drop(String table_name, String ID) throws SQLException {
        try {
            String Query = "DELETE FROM " + table_name + " WHERE ID = \"" + ID + "\"";
            Statement st = conexion.createStatement();
            st.executeUpdate(Query);
            conexion.close();
 
        } catch (SQLException ex) {
            System.out.println(ex.getMessage());
            conexion.close();
        }
    }
    
    //CRUD del usuario
    protected void CreateUsuarioTable() throws SQLException {
        try {
            conexion = DriverManager.getConnection(url, username, password);  
            
            String query = String.format("CREATE TABLE IF NOT EXISTS `%s`.`Usuarios` (idUsers INT NOT NULL AUTO_INCREMENT, nombre VARCHAR(256) NOT NULL, apellido VARCHAR(256) NOT NULL, edad INT NOT NULL, sexo VARCHAR(256) NOT NULL, email VARCHAR(256) NOT NULL, telefono VARCHAR(256) NULL,  passwordseguro LONGTEXT NOT NULL, `Roles_idroles` INT NOT NULL, PRIMARY KEY (`idUsers`, `Roles_idroles`), UNIQUE INDEX `idUsers_UNIQUE` (`idUsers` ASC) VISIBLE, INDEX `fk_Users_Roles_idx` (`Roles_idroles` ASC) VISIBLE, CONSTRAINT `fk_Users_Roles` FOREIGN KEY (`Roles_idroles`) REFERENCES `%s`.`Roles` (`idroles`) ON DELETE NO ACTION ON UPDATE NO ACTION)", databaseName,databaseName);
            
            Statement st = conexion.createStatement();
            st.executeUpdate(query);
            conexion.close();
            
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
            conexion.close();
        } 
    }
    //  Evitamos la inyecion SQL en la tabla Usuarios        
    
    public void InsertarUsuario(String nombre, String apellido, int edad, String sexo, String email, String telefono, String password, int roletype) throws Exception{
        try{
            conexion = DriverManager.getConnection(url, username, this.password);            
            String query = "INSERT INTO Usuarios (nombre, apellido, edad, sexo, email, telefono, passwordseguro, Roles_idroles) VALUES (?,?,?,?,?,?,?,?)";        
            PreparedStatement statement = conexion.prepareStatement(query);        
            statement.setString(1, nombre);       
            statement.setString(2, apellido);
            statement.setInt(3, edad);
            statement.setString(4, sexo);
            statement.setString(5, email);
            statement.setString(6, telefono);
                        
            String hashedpassword = new AES().encryptPassword(password);
            //guardo el valor de la contraseña encriptada
            statement.setString(7, hashedpassword);
            statement.setInt(8, roletype);
            statement.executeUpdate();
            conexion.close();
        } catch (SQLException e){
            System.out.println("Error" + e.getMessage());
            conexion.close();
        }
    }
}
