/*Fichero de configuración que almacena las conexexiones a Base de Datos
y el cifrado
*/
package com.edgar.aseregehtmlframework;
/**
 *
 * @author Edgar Muñoz
 */

public class Configuration {
    public String hostname;
    public int port;
    public String username;
    public String password;
    public String databaseName;
    public String IV;
    
    
    public Configuration(String hostname, int port, String username, String password, String databaseName, String IV) {               
        this.hostname = hostname;
        this.port = port;
        this.username = username;
        this.password = password;
        this.databaseName = databaseName;   
        this.IV = IV;
        
    }   
}
