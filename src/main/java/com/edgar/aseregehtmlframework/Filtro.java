package com.edgar.aseregehtmlframework;
import java.io.File;
import java.io.FilenameFilter;
import java.util.Map;
/**
 *
 * @author Edgar Muñoz
 */
public class Filtro implements FilenameFilter {
    String extension;
    Filtro(String extension) {
        this.extension = extension;
    }
    public Filtro() { }

     public <T, E> T getKeyFromValue(Map<T, E> map, E value) {
        for (Map.Entry<T, E> entry : map.entrySet()) {
            if (value.equals(entry.getValue())) {
                return entry.getKey();
            } 
        } 
        return null;
    }
     
    //implementación del método accept del interface
    @Override public boolean accept(File ruta, String nombre) {
        return nombre.endsWith(extension);
    }
}
