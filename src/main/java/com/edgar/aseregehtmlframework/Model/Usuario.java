package com.edgar.aseregehtmlframework.Model;

/**
 *
 * @author Edgar Muñoz
 */
public class Usuario {
    private int idUsuario;
    private String nombre;
    private String apellido;
    private int edad;
    private char sexo;
    private String password;
    
    public Usuario(){}
    public Usuario(int idUsuario, String nombre, String apellido, int edad, char sexo, String password){
        this.idUsuario = idUsuario;
        this.nombre = nombre;
        this.apellido = apellido;
        this.edad = edad;
        this.sexo = sexo;
        this.password = password;
    }
    public int getIdUsuario(){
        return idUsuario;
    }
    public String getNombre() {
        return nombre;
    }
    public String getApellido() {
        return apellido;
    } 
    public int getEdad(){
        return edad;
    }
    public char getSexo() {
        return sexo;
    } 
    public String getPassword() {
        return password;
    }
    
    public void SetIdUsuario(int idUsuario){
        this.idUsuario = idUsuario;        
    }
    public void SetNombre(String nombre){
        this.nombre = nombre;        
    }
    
    public void SetApellido(String apellido){
        this.apellido = apellido;        
    }
    public void SetEdad(int edad){
        this.edad = edad;
    }
    public void SetSexo(char sexo){
        this.sexo = sexo;        
    }
    public void SetPassword(String password){
        this.password = password;        
    }
}
