package com.edgar.aseregehtmlframework;
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
    private final String ALGORITHM = "AES";    
    
    public String encryptPassword(String password) throws Exception {
        Key key = new SecretKeySpec("C#:_ytresw3456j?".getBytes(), ALGORITHM);
        Cipher cipher = Cipher.getInstance(ALGORITHM);
        cipher.init(Cipher.ENCRYPT_MODE, key);

        byte[] encryptedPasswordBytes = cipher.doFinal(password.getBytes());

        return Base64.getEncoder().encodeToString(encryptedPasswordBytes);
    }
}