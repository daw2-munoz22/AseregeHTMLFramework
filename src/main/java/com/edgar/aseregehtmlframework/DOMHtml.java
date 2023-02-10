/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package com.edgar.aseregehtmlframework;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

/**
 *
 * @author Edgar Muñoz
 */
public class DOMHtml {
    private String html = "";
    public DOMHtml(String htmlName) throws FileNotFoundException, IOException{           
        byte[] data = Files.readAllBytes(Paths.get(htmlName));
        html = new String(data);        
    }
    public String GetDom(){
        return html;        
    }
}
