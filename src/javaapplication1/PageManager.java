package javaapplication1;

import java.io.File;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

/**
 *
 * @author Edgar Muñoz
 */


//Esta clase, obtenmos la página principal
public class PageManager {
    /*Defino todas las páginas de la web, guardadas en la variable pages, que 
    será un diccionario de paginas (clave-valor)*/    
    private Map<String, HtmlBuilder> pages = new HashMap<String, HtmlBuilder>();
    private String[] webnames;
    /*Creo los contructores
        1º será vacío
        2º pasamos como parametro las páginas web ya recreadas
    */
    public PageManager(){}
    public PageManager(Map<String, HtmlBuilder> pages){
        this.pages = pages;
    }
    
    /*La puncion AddPage, crea la estructura de la web y hace un push al diccionario 
      */
    public void AddPage(String headfile, String stylefile, String pagefile, String footerfile) throws IOException
    {
        HtmlBuilder templatePage = new HtmlBuilder(
                new DOMHtml(headfile),
                new DOMHtml(stylefile),
                new DOMHtml(pagefile),
                new DOMHtml(footerfile)
        );
        String name = pagefile.split(".web")[0];                
        pages.put(name, templatePage);                 
    }         
    /*Funcion para cargar las páginas web*/
    /*user.dir devuelve la carpeta dónde están los ficheros del programa*/
    public void LoadPages() throws IOException{                     
        String ruta = System.getProperty("user.dir");      
        File folder = new File(ruta);                                
        webnames = folder.list(new Filtro(".web"));        
               /*Recoremos la coleccion file (páginas web)
                Llamamos a la funcion AddPage y mostramos la coleccion*/
        for (String file : webnames) {
             AddPage("head.global", "styles.css", file, "footer.global");                    
        }
    }
    
    /*Funcion para actualizar las páginas web. Para ello, 
    la definimos a null y llamamos a la función anterior*/
    public void UpdatePages() throws IOException{
        //pages = null;
        LoadPages();       
    }
    public String[] GetWebNames(){  
        return webnames;
    }
    public Map<String, HtmlBuilder> GetPages(){      
        return pages;      
    }   
}
