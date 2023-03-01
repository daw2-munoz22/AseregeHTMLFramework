package com.edgar.aseregehtmlframework;
import static com.edgar.aseregehtmlframework.JsonConvert.Deserialize;
import com.edgar.aseregehtmlframework.Model.Role;
import com.edgar.aseregehtmlframework.Model.Usuario;
import com.edgar.aseregehtmlframework.api.RolesApi;
import com.edgar.aseregehtmlframework.api.UsuarioApi;
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
            Configuration configuration = Deserialize(connectionFile, Configuration.class);                                                            
            
        } catch (IOException ex) {
            System.out.println(ex.getMessage());
        }                                                 
        try {
            //pasar los parametros de la base de datos
            //MysqlManager sqlManager = new MysqlManager("localhost", 3306, "Aserege2", "1234567890");
            //MysqlManager sqlManager = new MysqlManager("localhost", 3306, "root", "root", "barcelonaweb");
            
            
            String connectionFile = new String(Files.readAllBytes(Paths.get("aserege.conf")));                                                        
            Configuration configuration = Deserialize(connectionFile, Configuration.class);                                       
            MysqlManager sqlManager = new MysqlManager(configuration.hostname, configuration.port, configuration.username, configuration.password, configuration.databaseName);                        
            sqlManager.CreateRoleTable();
            sqlManager.CreateUsuarioTable();            
            //añadi los otros roles que faltaban e estuve comprobando a ver si funcionaban
            sqlManager.InsertRole("Administrador", 1);
            sqlManager.InsertarUsuario("Edgar", "Muñoz", 21, "M", "edgarmunozmanjon@gmail.com", "+34648401735", "P@ssw0rd543",1);            
             
            
                        
           
                        
            
            
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
            String folder = file.split(".web")[0];
            HtmlBuilder pageValue = pageCollection.get(folder);  
            //buscar la llave a partir del diccionario 
            String key = filter.getKeyFromValue(pageCollection, pageValue); 
            //asignacion de nombre
            pageValue.SetPageName(key);
                        
            server.createContext("/" + key, pageValue);            
        }   
                        
        server.createContext("/api/usuarios", new UsuarioApi());        
        server.createContext("/api/roles", new RolesApi());
        
         //lanzar el servidor              
        server.setExecutor(null); // creates a default executor
        server.start();
    }        
}
