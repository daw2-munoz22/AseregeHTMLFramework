package com.edgar.API;
import com.edgar.Configuration;
import com.edgar.JsonConvert;
import com.edgar.Managers.MySQL;
import com.edgar.Model.Role;
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

//Esra clase implementa la interfaz API, por lo tanto, tengo que utilizar los metodos de la interfaz
public class RolesAPI implements API {
    MySQL sqlManager;
    //constructor
    public RolesAPI() throws Exception {
        String connectionFile = new String(Files.readAllBytes(Paths.get("aserege.conf")));//cadena de conexion (parametros)
        //la configuracion del mysql, que realiza el json. La funcion obtiene el fichero de conexion,
        // y construye un objeto del tipo Configuracion
        Configuration configuration = Deserialize(connectionFile, Configuration.class);
        //query -> para la conexión
        sqlManager = new MySQL(configuration.hostname, configuration.port, configuration.username, configuration.password, configuration.databaseName);
    }

    //El @override se utiliza para alterar la funcion del padre
    @Override
    public String GET() throws JsonProcessingException {
        //Utilizar las APIs GET
        //almaceno los roles de la base de datos en una lista de roles. En este caso, se define nulo, debido a que NO está definida
        ArrayList<Role> roles = null;
        try {
            //almacenar los datos en la variable roles
            roles = sqlManager.SelectRoles();
        } catch (SQLException e) {
            System.out.println(e.getMessage()); //mensaje de error
        }
        return JsonConvert.Serialize(roles); //convertir el resultado en un JSON y lo devuelve
    }

    @Override
    public String POST() { //Utilizar las APIs POST
        return null;
    }

    @Override
    public String PUT() {//Utilizar las APIs PUT

        return null;
    }

    @Override
    public String DELETE() {//Utilizar las APIs DELETE
        return null;
    }
}
