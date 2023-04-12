package com.edgar.API;

import com.edgar.Configuration;
import com.edgar.JsonConvert;
import com.edgar.Managers.MySQL;
import com.edgar.Model.Usuario;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.nio.file.Files;
import java.nio.file.Paths;
import java.sql.SQLException;
import java.util.ArrayList;

import static com.edgar.JsonConvert.Deserialize;

/**
 *
 * @author Edgar Muñoz
 */
public class ConnectAPI implements  API {

    MySQL sqlManager;
    public ConnectAPI() throws Exception {
        String connectionFile = new String(Files.readAllBytes(Paths.get("aserege.conf")));
        Configuration configuration = Deserialize(connectionFile, Configuration.class);
        sqlManager = new MySQL(configuration.hostname, configuration.port, configuration.username, configuration.password, configuration.databaseName);
    }

    @Override
    public String GET() throws JsonProcessingException {
        return null;
    }

    @Override
    public String POST(String jsonObject) throws Exception {
        try{
            sqlManager.CreateRoleTable();
            sqlManager.CreateUsuarioTable();
        } catch (Exception e){
            return "Error: " + e.getMessage();
        }
      return "OK";
    }

    /*

    @Override
    public String POST(HttpServletRequest request) throws JsonProcessingException {
        // Obtener los datos del cuerpo de la solicitud y convertirlos en un objeto Usuario
        String requestBody = request.getReader().lines().collect(Collectors.joining());
        Usuario usuario = JsonConvert.Deserialize(requestBody, Usuario.class);

        // Agregar el nuevo objeto Usuario a la base de datos
        try {
            sqlManager.InsertUsuario(usuario);
        } catch (SQLException e) {
            System.out.println(e.getMessage());
            return "Error al agregar el usuario";
        }

        // Devolver una respuesta indicando que la operación se realizó correctamente
        return "Usuario agregado correctamente";
    }
    */
    @Override
    public String PUT() {

        return null;
    }

    @Override
    public String DELETE() {

        return null;
    }
}
