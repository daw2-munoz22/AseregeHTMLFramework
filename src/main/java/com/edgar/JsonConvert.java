package com.edgar;

import com.edgar.Model.Usuario;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.*;
import com.fasterxml.jackson.databind.module.SimpleModule;

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
    public static SimpleModule getModule() {
        SimpleModule module = new SimpleModule();
        module.addSerializer(char.class, new JsonSerializer<Character>() {
            @Override
            public void serialize(Character value, JsonGenerator gen, SerializerProvider serializers)
                    throws IOException {
                gen.writeRawValue("'" + value + "'");
            }
        });
        module.addDeserializer(char.class, new JsonDeserializer<Character>() {
            @Override
            public Character deserialize(JsonParser p, DeserializationContext ctxt)
                    throws IOException, JsonProcessingException {
                String str = p.getText();
                if (str.length() != 1) {
                    throw new IllegalArgumentException("Expected a single character, but got: " + str);
                }
                return str.charAt(0);
            }
        });

        return module;
    }
    
    public static <T> T Deserialize(String json, Class<T> clazz) throws IOException {    
        ObjectMapper mapper = new ObjectMapper();
        return mapper.readValue(json, clazz);
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
