package com.edgar.aseregehtmlframework.api;
import com.edgar.aseregehtmlframework.Configuration;
import com.edgar.aseregehtmlframework.JsonConvert;
import static com.edgar.aseregehtmlframework.JsonConvert.Deserialize;
import com.edgar.aseregehtmlframework.Model.Usuario;
import com.edgar.aseregehtmlframework.MysqlManager;
import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import java.io.IOException;
import java.io.OutputStream;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Edgar Muñoz
 */
public class UsuarioApi implements HttpHandler, api {

    MysqlManager sqlManager; 
    public UsuarioApi() throws Exception {
        String connectionFile = new String(Files.readAllBytes(Paths.get("aserege.conf")));                                                
        Configuration configuration = Deserialize(connectionFile, Configuration.class);                               
        sqlManager = new MysqlManager(configuration.hostname, configuration.port, configuration.username, configuration.password, configuration.databaseName);            
    }
    
    @Override public void handle(HttpExchange t) throws IOException {     
        //Peticiones GET de los usuarios
        try {                                    
            ArrayList<Usuario> usuario = sqlManager.SelectUsuarios();
            String response = JsonConvert.Serialize(usuario);
                                     
            t.sendResponseHeaders(200, response.length());               
            OutputStream os = t.getResponseBody();                
            os.write(response.getBytes());                
            os.close();        
        
        
        } catch (Exception ex) {
            Logger.getLogger(UsuarioApi.class.getName()).log(Level.SEVERE, null, ex);
        }
    } 
}
