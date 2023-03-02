 package com.edgar.Model;
/**
 *
 * @author Edgar Muñoz
 */
//Definir los roles de la Base de Datos
public class Role {
    private int idroles;
    private String nombre;
    private int type;
    
    public Role(int idroles, String nombre, int type) {        
        this.idroles = idroles;
        this.nombre = nombre;
        this.type = type;
    }
    
    public int getRoleID() {
        return idroles;
    }
    public String getNombre() {
        return nombre;
    }
    public int getType() {
        return type;
    }
    public void SetRoleID(int idroles){
        this.idroles = idroles;        
    }
    public void SetNombre(String nombre){
        this.nombre = nombre;        
    }
    public void SetType(int type){
        this.type = type;        
    }
}
