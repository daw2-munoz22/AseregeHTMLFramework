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
   
    private String pageName;
    
    private DOMHtml header;
    private DOMHtml css;
    private DOMHtml html;
    private DOMHtml footer;
    
    
    public HtmlBuilder (DOMHtml header, DOMHtml css, DOMHtml html, DOMHtml footer){
        this.header = header;        
        this.css = css;        
        this.html = html;
        this.footer = footer;
    }
    public void SetPageName(String pageName) {
        this.pageName = pageName;        
    }
    public void handle(HttpExchange t) throws IOException {   
        
        if (pageName.equals("update")){
            JavaApplication1.pages.UpdatePages();                
        }
        String response = header.GetDom() + "<style>" + css.GetDom() + "</style></head><body>" + html.GetDom() + "<footer>" + footer.GetDom() + "</footer></body></html>";        
        t.sendResponseHeaders(200, response.length());        
        OutputStream os = t.getResponseBody();        
        os.write(response.getBytes());        
        os.close();        
    } 
    
}
