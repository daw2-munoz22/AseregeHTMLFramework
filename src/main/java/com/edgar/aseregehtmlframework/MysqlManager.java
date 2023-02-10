package com.edgar.aseregehtmlframework;
//import com.sun.jdi.connect.spi.Connection;
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
     String url = "";        
     String username = "";        
     String password = "";
    
    //constructor de la conexión a la base de datos MySQL
    public MysqlManager(String hostname, int port, String username, String password, String database) throws Exception{           
           
        url = String.format("jdbc:mysql://%s:3306/%s", hostname,database);        
        this.username = username;
        this.password = password;              
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
}
