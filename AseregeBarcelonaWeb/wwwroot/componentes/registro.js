import { API } from "../js/API.js";
import { Usuario } from "../modelo/Usuario.js";

export const registro = {
    template: `<div id="templatepage" class="container-fluid">
    <form id="form2" class="w-lg-25 m-5 card p-5 shadow needs-validation" novalidate="">
        <h1>Registrarse</h1>
        <div class="p-sm-12 mt-3">
            <label for="nombre" class="form-label">Nombre:</label>
            <input id="nombre" type="text" class="form-control was-validated" required="">
            <div class="valid-feedback">Todo estupendo</div>
            <!-- mensaje si no valida -->
            <div class="invalid-feedback">Introduce tu nombre</div>
        </div>
        <div class="p-sm-12 mt-3">
            <label for="apellido" class="form-label">Apellido: </label>
            <input id="apellido" type="text" class="form-control was-validated" required="">
            <div class="valid-feedback">Todo estupendo</div>
            <!-- mensaje si no valida -->
            <div class="invalid-feedback">Introduce tu apellido</div>
        </div>
        <div class="p-sm-12 mt-3">
            <label for="edad" class="form-label">Edad: </label>
            <input id="edad" type="date" class="form-control was-validated" required="">
            <div class="valid-feedback">Todo estupendo</div>
            <!-- mensaje si no valida -->
            <div class="invalid-feedback">Introduce tu Edad</div>
        </div>
        
        <div class="p-sm-12 mt-3">
            <label for="sexo" class="form-label">Sexo: </label>
            <!-- <input id="sexo" type="text" class="form-control was-validated" required=""> -->
            <select name="sexo" id="sexo">
                <option value="hombre">Hombre</option>
                <option value="mujer">Mujer</option>
                <option value="transexual">Transexual</option>
              </select>
            <div class="valid-feedback">Todo estupendo</div>
            <!-- mensaje si no valida -->
            <div class="invalid-feedback">Introduce tu Sexo</div>
        </div>
        
        <div class="p-sm-12 mt-3">
            <label for="email" class="form-label">Email: </label>
            <input id="email" type="email" class="form-control was-validated" value="" required="">
            <div class="valid-feedback">Todo estupendo</div>
            <!-- mensaje si no valida -->
            <div class="invalid-feedback">Introduce tu Email</div>
        </div>
        <div class="p-sm-12 mt-3">                
            <label for="telefono" class="form-label">Telefono: </label>
            <input id="telefono" type="tel" class="form-control was-validated" value="" required="">
            <div class="valid-feedback">Todo estupendo</div>
            <!-- mensaje si no valida -->
            <div class="invalid-feedback">Introduce tu Telefono</div>
        </div>
        <div class="p-sm-12 mt-3">
            <label for="pass" class="form-label">Contraseña:</label>
            <input id="pass" type="password" class="form-control was-validated" required="" maxlength="16" minlength="4">
            <div class="valid-feedback">Todo estupendo</div>
            <!-- mensaje si no valida -->
            <div class="invalid-feedback">Introduce tu Contraseña</div>
        </div>
        <input id="enviar2" type="submit" value="Registrarse" class="mt-3 w-100 fs-5 btn btn-success">
    </form>
    </div>`,
    init: () => {
        document.getElementById('body').innerHTML += registro.template;
        document.getElementById('body').style.backgroundImage = 'url("images/cathedral.jpg")';        
     
        let formText = document.querySelectorAll('label.form-label');
        for(var i=0; i<formText.length; i++) {
            formText[i].style.fontSize = 'medium';
        }   

        let h1 = document.querySelector('h1');
        h1.style.fontFamily = "'Rokkitt', serif";
        h1.style.fontSize = "xx-large";                
    },
    unload: ()=> {
        const child = document.getElementById('templatepage');
        document.getElementById('body').removeChild(child);        
    },
    script: () => {
        // para form2
        document.querySelector('#enviar2').addEventListener('click', (e) => {
            const nombre = document.querySelector('#nombre');
            nombre.classList.add('was-validated');

            const apellido = document.querySelector('#apellido');
            apellido.classList.add('was-validated');

            const edad = document.querySelector('#edad');
            edad.classList.add('was-validated');

            const email = document.querySelector('#email');
            email.classList.add('was-validated');

            const sexo = document.querySelector('#sexo');
            sexo.classList.add('was-validated');

            const telefono = document.querySelector('#telefono');
            telefono.classList.add('was-validated');

            const pass = document.querySelector('#pass');
            pass.classList.add('was-validated');

            e.preventDefault();

            let usuario = new Usuario(nombre.value, apellido.value, edad.value, sexo.value, email.value, telefono.value, pass.value);
            console.log(usuario);
            //Añadimos la clase was-validated para que se muestre la validación de boostrap
            document.querySelector('#form2').classList.add('was-validated');
            if (form2.checkValidity()) {
                form2.classList.remove('was-validated');
                const json = JSON.stringify(usuario);
                //llamada a la clase API, que devuelve el tipo de petición (GET, POST, PUT, DELETE)

                const peticion = new API().httpGetAsync('https://jsonplaceholder.typicode.com/posts/1', function (data) {
                    const post = JSON.parse(data);
                    console.log(`Title: ${post.title}`);
                    console.log(`Body: ${post.body}`);
                });
                console.log(peticion);

                const data = JSON.stringify(usuario);
                const metodoPost = new API().httpPostAsync('https://example.com/api/users', data, function (response) {

                    console.log('Response received:' + response);
                });
                console.log(metodoPost)
            }

        });
    }
}