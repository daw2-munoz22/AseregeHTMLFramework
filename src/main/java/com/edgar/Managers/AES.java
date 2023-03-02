package com.edgar.Managers;
import java.security.Key;
import java.util.Base64;
import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;

/**
 *
 * @author Edgar Muñoz
 */

//Esta clase sirve para el sistema de encriptación de contraseña
public class AES {                           
    private final String ALGORITHM = "AES"; //definir que algoritmo es
    public String encryptPassword(String password) throws Exception {
        //definir el vector de cifrado de tipo AES's Key
        Key key = new SecretKeySpec("C#:_ytresw3456j?".getBytes(), ALGORITHM);
        Cipher cipher = Cipher.getInstance(ALGORITHM);//obtener la instancia del cifrado
        cipher.init(Cipher.ENCRYPT_MODE, key);//iniciar el cifrado con la AES (IV)

        byte[] encryptedPasswordBytes = cipher.doFinal(password.getBytes());//guardo el resultado final de la contraseña cifrada en binario

        return Base64.getEncoder().encodeToString(encryptedPasswordBytes);//convertir en base64 y lo devuelve en este nuevo formato
    }
}