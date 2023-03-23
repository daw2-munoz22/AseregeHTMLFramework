package com.edgar.Model;
/**
 *
 * @author Edgar Muñoz
 */
public class Usuario {
    private int idUsers;
    private String nombre;
    private String apellido;
    private int edad;
    private char sexo;
    private String email;
    private String telefono;
    private String passwordseguro;

    public Usuario(){}
    public Usuario(int idUsers, String nombre, String apellido, int edad, char sexo, String email, String telefono, String passwordseguro){
        this.idUsers = idUsers;
        this.nombre = nombre;
        this.apellido = apellido;
        this.edad = edad;
        this.sexo = sexo;
        this.email= email;
        this.telefono = telefono;
        this.passwordseguro = passwordseguro;
    }
    public int getIdUsuario(){
        return idUsers;
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
        return passwordseguro;
    }
    public String getEmail() {
        return email;
    }
    public String getTelefono() {
        return telefono;
    }
    
    public void SetIdUsuario(int idUsers){
        this.idUsers = idUsers;
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
    public void SetTelefono(String telefono){
        this.telefono = telefono;
    }
    public void SetEmail(String email){
        this.email = email;
    }
    public void SetPassword(String passwordseguro){ this.passwordseguro = passwordseguro; }
}
