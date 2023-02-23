package com.edgar.aseregehtmlframework;
import com.edgar.aseregehtmlframework.Model.Role;
import com.edgar.aseregehtmlframework.Model.Usuario;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.*;



/**
 * 
 * @author Edgar Muñoz
 */
public class MysqlManager {       
    private static Connection conexion;
     //la conexion generada es esta -> jdbc:mysql://localhost:3306/world
     private String url = "";        
     private String username = "";        
     private String password = "";
     private String databaseName = "";
     
    //constructor de la conexión a la base de datos MySQL
    public MysqlManager(String hostname, int port, String username, String password, String databaseName) throws Exception{           
           
        url = String.format("jdbc:mysql://%s:3306/%s", hostname,databaseName);        
        this.username = username;
        this.password = password;  
        this.databaseName = databaseName;
    }  
    //creación de la tabla roles
    
    protected void CreateRoleTable(){                       
        try {
            conexion = DriverManager.getConnection(url, username, password);            
            String Query = String.format("CREATE TABLE IF NOT EXISTS `%s`.`Roles` (`idroles` INT NOT NULL AUTO_INCREMENT, `nombre` VARCHAR(256) NULL, `type` INT NULL, PRIMARY KEY (`idroles`), UNIQUE INDEX `idroles_UNIQUE` (`idroles` ASC) VISIBLE);", databaseName);                                
            Statement st = conexion.createStatement();
            st.executeUpdate(Query);
            
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
        }        
    }
    //añadir los roles a la tabla
    public void InsertRole(String nombre, int type){
        
       //  Evitamos la inyecion SQL en la tabla Roles
        try {
            conexion = DriverManager.getConnection(url, username, password);            
            
              
            String query = "INSERT INTO roles (nombre,type) VALUES (?,?)";        
            PreparedStatement statement = conexion.prepareStatement(query);        
            statement.setString(1, nombre);       
            statement.setInt(2, type);            
            statement.executeUpdate();                    
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
        }        
    }     
    public Role SelectRole(int permisionlevel){                       
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
            return DBRole;//devolver el rol
            
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
        }    
        return null;//No hay roles o la base de datos falla
    }
    
    //eliminar datos
    private void Drop(String table_name, String ID){
        try {
            String Query = "DELETE FROM " + table_name + " WHERE ID = \"" + ID + "\"";
            Statement st = conexion.createStatement();
            st.executeUpdate(Query);
 
        } catch (SQLException ex) {
            System.out.println(ex.getMessage());
        }
    }
    
    //CRUD del usuario
    protected void CreateUsuarioTable(){
        try {
            conexion = DriverManager.getConnection(url, username, password);  
            
            String query = String.format("CREATE TABLE IF NOT EXISTS `%s`.`Usuarios` (idUsers INT NOT NULL AUTO_INCREMENT, nombre VARCHAR(256) NOT NULL, apellido VARCHAR(256) NOT NULL, edad INT NOT NULL, sexo VARCHAR(256) NOT NULL, email VARCHAR(256) NOT NULL, telefono VARCHAR(256) NULL,  passwordseguro LONGTEXT NOT NULL, `Roles_idroles` INT NOT NULL, PRIMARY KEY (`idUsers`, `Roles_idroles`), UNIQUE INDEX `idUsers_UNIQUE` (`idUsers` ASC) VISIBLE, INDEX `fk_Users_Roles_idx` (`Roles_idroles` ASC) VISIBLE, CONSTRAINT `fk_Users_Roles` FOREIGN KEY (`Roles_idroles`) REFERENCES `barcelonaweb`.`Roles` (`idroles`) ON DELETE NO ACTION ON UPDATE NO ACTION)", databaseName);
            
            Statement st = conexion.createStatement();
            st.executeUpdate(query);
            
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
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
        } catch (SQLException e){
            System.out.println("Error" + e.getMessage());
        }
    }
}
