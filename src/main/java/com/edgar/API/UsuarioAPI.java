package com.edgar.API;

import com.edgar.Configuration;
import com.edgar.JsonConvert;
import com.edgar.Managers.MySQL;
import com.edgar.Model.Usuario;
import com.fasterxml.jackson.core.JsonProcessingException;

import java.nio.file.Files;
import java.nio.file.Paths;
import java.sql.SQLException;
import java.util.ArrayList;

import static com.edgar.JsonConvert.Deserialize;

/**
 *
 * @author Edgar Muñoz
 */
public class UsuarioAPI implements  API {

    MySQL sqlManager;
    public UsuarioAPI() throws Exception {
        String connectionFile = new String(Files.readAllBytes(Paths.get("aserege.conf")));
        Configuration configuration = Deserialize(connectionFile, Configuration.class);
        sqlManager = new MySQL(configuration.hostname, configuration.port, configuration.username, configuration.password, configuration.databaseName);
    }

    @Override
    public String GET() throws JsonProcessingException {
        ArrayList<Usuario> usuario = null;
        try {
            usuario = sqlManager.SelectUsuarios(); //en la variable usuario, guardo los usuarios seleccionados
        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }
        return JsonConvert.Serialize(usuario);
    }

    @Override
    public String POST() throws JsonProcessingException{
       System.out.println("HE LLEGADO AL POST");
        /* try{
            URL url = new URL("https://barcelonaweb.edgarmunoz4.repl.co");
            Map<Object, Usuario> params = new LinkedHashMap<>();

            for(Map.Entry<Object, Usuario>){

            }
            //params.put();
        }catch (SQLException ex) {
            System.out.println(ex.getMessage());
        }
        return null;
        */
        return "";
    }

    @Override
    public String PUT() {
        return null;
    }

    @Override
    public String DELETE() {

        return null;
    }
}
