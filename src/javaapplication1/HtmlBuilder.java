/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package javaapplication1;

import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import java.io.IOException;
import java.io.OutputStream;

/**
 *
 * @author Edgar Muñoz
 */
public class HtmlBuilder implements HttpHandler {    
    private DOMHtml css;
    private DOMHtml html;
    
    public HtmlBuilder (DOMHtml css, DOMHtml html){
        this.css = css;
        this.html = html;
    }
    
    public void handle(HttpExchange t) throws IOException {            
        String response = "<html><head><style>" + css.GetDom() + "</style></head><body>" + html.GetDom() + "</body></html>";        
        t.sendResponseHeaders(200, response.length());        
        OutputStream os = t.getResponseBody();        
        os.write(response.getBytes());        
        os.close();        
    } 
    
}
