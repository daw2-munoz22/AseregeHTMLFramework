import { API } from "../js/API.js";

export const login = {
    template: `
    <div id="templatepage" class="container-fluid">
        <form id="form2" class="w-lg-25 m-sm-8 m-lg-5 card p-5 shadow needs-validation" novalidate="">
            <h1 style="font-family: 'Quattrocento', serif;">Iniciar session</h1>
            <div class="p-sm-12 mt-3">
                <label style="font-family: 'Rokkitt', serif; font-size: xx-large;" for="nick" class="form-label">Nombre:</label>
                <input id="nick" type="text" class="form-control was-validated" required="" pattern="^[^0-9][^@#]+$">
                <!-- mensaje si valida-->
                <div class="valid-feedback">Todo estupendo</div>
                <!-- mensaje si no valida -->
                <div class="invalid-feedback">Inserte una contraseña con almenos 2 caracteres y que empiece por una letra</div>

            </div>           
            <div class="p-sm-12 mt-3">
                <label style="font-family: 'Rokkitt', serif; font-size: xx-large;" for="pass" class="form-label">Contraseña:</label=for=>
                <input id="pass" type="password" class="form-control was-validated" required="" maxlength="16" minlength="4">
                <div class="valid-feedback">Todo estupendo</div>
                <!-- mensaje si no valida -->
                <div class="invalid-feedback">Introduce tu Contraseña</div>
            </div>
            <input id="enviar2" type="submit" value="Iniciar session" class="mt-3 w-100 fs-5 btn btn-success">
        </form>
    </div>
    `,
    init: () => {
        document.getElementById('body').innerHTML += login.template;
        /*CSS*/
        document.getElementById('body').style.backgroundImage = 'url("images/cathedral.jpg")';
        let formText = document.querySelectorAll('label.form-label');
        for(var i=0; i<formText.length; i++) {
            formText[i].style.fontSize = 'medium';
        }                
    },
    unload: ()=> {
        const child = document.getElementById('templatepage');
        document.getElementById('body').removeChild(child);        
    },
    script: () => {
        document.querySelector('#form2').classList.add('was-validated');
        document.querySelector('#form2').addEventListener('click', (e)=>{
            e.preventDefault()
            if (form2.checkValidity(e)) {
                form2.classList.remove('was-validated');      
                
                const nombre = document.getElementById("nick").value;
                const password = document.getElementById("pass").value;
    
                //new API().login(nombre, password);                                
                // const json = JSON.stringify(usuario);
                // console.log(json);
            }
        })
       
    }
}