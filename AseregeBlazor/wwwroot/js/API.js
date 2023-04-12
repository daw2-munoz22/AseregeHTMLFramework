export class API {
   
    async generatePasswordHash(input) {

        const encoder = new TextEncoder();
        const data = encoder.encode(input);
        return crypto.subtle.digest('SHA-256', data).then(hash => { //elegir el protocolo de cifrado

            //implementación del cifrado
            const hexCodes = [];
            const view = new DataView(hash);
            for (let i = 0; i < view.byteLength; i += 4) {
                const value = view.getUint32(i);
                const stringValue = value.toString(16);
                const padding = '00000000'; 
                const paddedValue = (padding + stringValue).slice(-padding.length); //guardo el valor separados por espacios
                hexCodes.push(paddedValue);
            }
            return hexCodes.join(''); //concatenar los resultados
        });
    }

    async setCookie(name, value) {
        var expires = new Date();
        expires.setTime(expires.getTime() + 60000); // 1 minute
        document.cookie = name + '=' + value + ';expires=' + expires.toUTCString() + ';path=/';      
    }

    //login
    async login(nombre, password){
        localStorage.setItem("aserege_usuario",nombre);
        localStorage.setItem("aserege_pass", await this.generatePasswordHash(password));                      
        //Password:"Aloha124" 
        const Authorization = { Name: nombre, Password: password };
        this.httpPostAsync("https://localhost:5001/api/login", Authorization, (response)=>{
            console.log(response);
        })
    }
    //logout
    async logout(){
        localStorage.removeItem("aserege_usuario");
        localStorage.removeItem("aserege_pass");
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