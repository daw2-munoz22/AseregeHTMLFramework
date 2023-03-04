export class API {
    //Método para generar un hash encriptado con javascript
    async calcularHash(data) {
        const hashBuffer = await crypto.subtle.digest('SHA-256', data);
        //Uint8Array significa Unsigned int array de 8 bits
                  
        const hashArray = Array.from(new Uint8Array(hashBuffer));
        const hashHex = hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
        return hashHex;      
    }
    async encryptPassword(password) {
        const ALGORITHM = "AES";
        const key = new Uint8Array([67, 35, 58, 95, 121, 116, 114, 101, 115, 119, 51, 52, 53, 54, 106, 63]); // Vector de cifrado
        const data = new TextEncoder().encode(password);
        const iv = crypto.getRandomValues(new Uint8Array(16)); // Generar un IV aleatorio
        const cipher = await crypto.subtle.importKey("raw", key, ALGORITHM, false, ["encrypt"]);
        const encryptedPassword = await crypto.subtle.encrypt({name: ALGORITHM, iv: iv}, cipher, data);
      
        const encryptedPasswordBytes = new Uint8Array(encryptedPassword);
        const encryptedPasswordBase64 = btoa(String.fromCharCode(...encryptedPasswordBytes)); // Convertir en base64
      
        return encryptedPasswordBase64;
      }
      
    //login
    async login(nombre, password){
        //let hashpassword = await calcularHash(new TextEncoder().encode(password));       
        let hashpassword = await encryptPassword(password);       
        localStorage.setItem("aserege_usuario",nombre);
        localStorage.setItem("aserege_hash",hashpassword);
    }
    //logout
    async logout(){
        localStorage.removeItem("aserege_usuario");
        localStorage.removeItem("aserege_hash");
    }
    //para la peticion GET
    async httpGetAsync(theUrl, callback) {
        try {
            const response = await fetch(theUrl);
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const responseData = await response.text();
            callback(responseData);
        } catch (error) {
            console.error(error);
        }
    }
    //para la petición POST
    async httpPostAsync(theUrl, data, callback) {
        try {
            const response = await fetch(theUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const responseData = await response.text();
            callback(responseData);
        } catch (error) {
            console.error(error);
        }
    }
    //para la peticion PUT
    async httpPutAsync(theUrl, data, callback) {
        try {
            const response = await fetch(theUrl, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const responseData = await response.text();
            callback(responseData);
        } catch (error) {
            console.error(error);
        }
    }
    //para la peticion DELETE
    async httpDeleteAsync(theUrl, callback) {
        try {
            const response = await fetch(theUrl, {
                method: 'DELETE'
            });
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const responseData = await response.text();
            callback(responseData);
        } catch (error) {
            console.error(error);
        }
    }

}