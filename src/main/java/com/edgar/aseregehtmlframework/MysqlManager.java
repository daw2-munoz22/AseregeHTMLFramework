package com.edgar.aseregehtmlframework;
import com.edgar.aseregehtmlframework.Model.Role;
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
    public void CreateRoleTable(){                       
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
    public void InsertRole(Role role){                       
        try {
            conexion = DriverManager.getConnection(url, username, password);            
            String Query = String.format("INSERT INTO ROLES(nombre, type) VALUES('%s', %d%n)", role.getNombre(), role.getType());                                
            Statement st = conexion.createStatement();
            st.executeUpdate(Query);
            
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
        }        
    }     
    public Role SelectRole(int permisionlevel){                       
        Role DBRole = null;
        try {
            conexion = DriverManager.getConnection(url, username, password);            
            String Query = String.format("select idroles, nombre, type from roles where type = %d%n;", permisionlevel);        
            Statement st = conexion.createStatement();
            ResultSet resultSet = st.executeQuery(Query);
            
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
            
            
            
            
        //Seleccionar datos, los parametros son la query + nombre columna
    public void Select(String query, String columnName) {                   
        try {
            Connection connection = DriverManager.getConnection(url, username, password);
                    
            Statement statement = connection.createStatement();
            ResultSet resultSet = statement.executeQuery(query);
            while (resultSet.next()) {                
                String name = resultSet.getString(columnName);                
                System.out.println("Name: " + name);
            }
        } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
        }        
    }
    
    //insertar datos en la base de datos
    public void Insert(String nombreTabla, int id, String nombre, String apellido, int edad, String sexo){
        try {
            String Query = "INSERT INTO " + nombreTabla + " VALUES("
                    + "\"" + id + "\", "
                    + "\"" + nombre + "\", "
                    + "\"" + apellido + "\", "
                    + "\"" + edad + "\", "
                    + "\"" + sexo + "\")";
            Statement st = conexion.createStatement();
            st.executeUpdate(Query);
            
        } catch (SQLException ex) {
            System.out.println("Error: " + ex.getMessage());
        }
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
    public void createUsuarioTable(){
        try {
            conexion = DriverManager.getConnection(url, username, password);            
            String Query = String.format("CREATE TABLE IF NOT EXISTS `%s`.`Usuarios` (`idUsuario` INT NOT NULL AUTO_INCREMENT, `nombre` VARCHAR(256) NULL, `apellido` VARCHAR(256) NULL, `edad` INT NULL, `sexo` VARCHAR(256) NULL, `password` VARCHAR(256) NULL, PRIMARY KEY (`idUsuario`), UNIQUE INDEX `idUsuario_UNIQUE` (`idUsuario` ASC) VISIBLE);", databaseName);                                
            Statement st = conexion.createStatement();
            st.executeUpdate(Query);
            
         } catch (SQLException e) {
            System.out.println("Error: " + e.getMessage());
        } 
    }
}
