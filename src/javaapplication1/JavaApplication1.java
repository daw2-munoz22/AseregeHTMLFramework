package javaapplication1;
import com.sun.net.httpserver.HttpServer;
import java.io.IOException;
import java.net.InetSocketAddress;
import java.util.Map;

/**
 *
 * @author Edgar Muñoz
 */
public class JavaApplication1 {    
    /*Creo una instancia de la clase PageManager*/
    public static PageManager pages = new PageManager();
    
    public static void main(String[] args) throws IOException {
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
        
        Map<String, HtmlBuilder> pageCollection = pages.GetPages();                       
        
        for (String file : pages.GetWebNames()) {                         
            HtmlBuilder pageValue = pageCollection.get(file.split(".web")[0]);  
           
            String key = filter.getKeyFromValue(pageCollection, pageValue); 
            pageValue.SetPageName(key);
            
            server.createContext("/" + key, pageValue);            
        }   
                       
        server.setExecutor(null); // creates a default executor
        server.start();
    }   
}
