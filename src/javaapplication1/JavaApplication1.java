package javaapplication1;

import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import com.sun.net.httpserver.HttpServer;
import java.io.IOException;
import java.io.OutputStream;
import java.net.InetSocketAddress;

/**
 *
 * @author Edgar Muñoz
 */
public class JavaApplication1 
{
    /**
     * @param args the command line arguments
     */
  
    public static void main(String[] args) throws IOException 
    {                               
        HttpServer server = HttpServer.create(new InetSocketAddress(8080), 0);                
        
        HtmlBuilder home = new HtmlBuilder(new DOMHtml("styles.css"),new DOMHtml("home.web"));
        HtmlBuilder subpage = new HtmlBuilder(new DOMHtml("styles.css"),new DOMHtml("ruta.web"));
        server.createContext("/", home);
        server.createContext("/subpage", subpage);
        
        server.setExecutor(null); // creates a default executor
        server.start();
    }   
}
