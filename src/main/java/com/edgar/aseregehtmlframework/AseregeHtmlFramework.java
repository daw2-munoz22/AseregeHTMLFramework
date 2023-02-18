package com.edgar.aseregehtmlframework;
import com.edgar.aseregehtmlframework.Model.Role;
import com.edgar.aseregehtmlframework.Model.Usuario;
import com.sun.net.httpserver.HttpServer;
import java.io.IOException;
import java.net.InetSocketAddress;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Map;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Edgar Muñoz
 */
public class AseregeHtmlFramework {    
        
    /*Creo una instancia de la clase PageManager*/    
    public static PageManager pages = new PageManager();
    
    
    public static void main(String[] args) throws IOException, Exception {                                                   
        try {
            String connectionFile = new String(Files.readAllBytes(Paths.get("aserege.conf")));        
            Object configuration = JsonConvert.Deserialize(connectionFile);  
            
        } catch (IOException ex) {
            System.out.println(ex.getMessage());
        }
        
                
                
        //Paths.get(htmlName),
        /*AES crypt = new AES();
        String rowIV = crypt.encryptPassword("P|aaw9rd0986t");*/
        
        try {
            //pasar los parametros de la base de datos
            //MysqlManager sqlManager = new MysqlManager("localhost", 3306, "Aserege2", "1234567890");
            MysqlManager sqlManager = new MysqlManager("localhost", 3306, "root", "root", "barcelonaweb");
            sqlManager.CreateRoleTable();
            
            Role newRole = new Role(1, "Administrador",2);
            String name = newRole.getNombre();
            
            Role nuevoRol = new Role (2,"No Registrado", 1);
            String nombre = nuevoRol.getNombre();
            
            Role user_registrado = new Role (3,"Registrado", 3);
            String nombre3 = user_registrado.getNombre();
            
            //sqlManager.InsertRole(newRole);
            sqlManager.SelectRole(2);
            
            //sqlManager.Select("select name from city;","Name");
            } catch (Exception ex) {
                //este es el error
            Logger.getLogger(AseregeHtmlFramework.class.getName()).log(Level.SEVERE, null, ex);
        }
            /*Con HttpServer, obtenemos el socket de red
            (envio el puerto mediante parametro)*/
            /*Args se utiliza para inicializar el puerto concreto*/
        

        /*Uso este para publicar*/        
        //HttpServer server = HttpServer.create(new InetSocketAddress(Integer.parseInt(args[0])), 0);                        
        /*Uso este para desarrollar*/
        HttpServer server = HttpServer.create(new InetSocketAddress(8080), 0);
                        
        Filtro filter = new Filtro();
        
        /*Llamo a la funcion LoadPages*/
        pages.LoadPages();                 
        //añadir las páginas a la lista
        Map<String, HtmlBuilder> pageCollection = pages.GetPages();                       
        

        //añadir la ruta del fichero .web
        for (String file : pages.GetWebNames()) {        
            //obtener el valor cuyo clave es el archivo .web SIN extensión
            HtmlBuilder pageValue = pageCollection.get(file.split(".web")[0]);  
            //buscar la llave a partir del diccionario 
            String key = filter.getKeyFromValue(pageCollection, pageValue); 
            //asignacion de nombre
            pageValue.SetPageName(key);
                        
            server.createContext("/" + key, pageValue);            
        }   
         //lanzar el servidor              
        server.setExecutor(null); // creates a default executor
        server.start();
    }        
}
