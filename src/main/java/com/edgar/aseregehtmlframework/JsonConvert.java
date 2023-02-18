package com.edgar.aseregehtmlframework;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

/**
 *
 * @author Edgar Muñoz
 */
public class JsonConvert{
    public static String Serialize(Object T) throws JsonProcessingException{
        return new ObjectMapper().writeValueAsString(T);
    }
    public static Object Deserialize(String json) throws JsonProcessingException{
        try {
            Object object = new ObjectMapper().readValue(json, Object.class); 
            return new ObjectMapper().writeValueAsString(object);
        } catch (JsonProcessingException e) {   
            e.printStackTrace(); // Handle the exception if the JSON parsing fails
        }        
        return null;
    }
    
    public static void SaveConfiguration(String json){
        File file = new File("aserege.conf");
        try (BufferedWriter writer = new BufferedWriter(new FileWriter(file))) {
            writer.write(json); // Write the string to the fil
        } catch (IOException e) {   
            e.printStackTrace(); // Handle the exception if the write operation fails
        }
    }
}
