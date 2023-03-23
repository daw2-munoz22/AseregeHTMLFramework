package com.edgar.API;
import com.fasterxml.jackson.core.JsonProcessingException;

//Esta interfaz, definimos los tipos de petición de API
public interface API {
    String GET() throws JsonProcessingException; //throws es similar al try...catch. En caso de error, devuelve el JSON de error
    String POST(String jsonObject) throws Exception;
    String PUT();
    String DELETE();
}
