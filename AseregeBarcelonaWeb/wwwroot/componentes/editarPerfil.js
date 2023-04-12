export const editarPerfil = {
    template:`
    <div id="editarperfil">    
        <h1 class="pb-3">Editar Perfil</h1> 
        <form id="form2" class="needs-validation" novalidate>
            <div class="p-sm-12 mt-3">
                <label for="nombre" class="form-label">Nombre:</label>
                <input id="nombre" type="text" class="form-control was-validated nombreE" required=""  pattern="^[^0-9][^@#]+$">
                <div class="valid-feedback">Todo estupendo</div>
                <!-- mensaje si no valida -->
                <div class="invalid-feedback">Inserte una contraseña con almenos 2 caracteres y que empiece por una letra</div>
            </div>
            <div class="p-sm-12 mt-3">
                <label for="apellido" class="form-label">Apellido:</label>
                <input id="apellido" type="text" class="form-control was-validated apellidoE" required="" pattern="^[^0-9][^@#]+$">
                <div class="valid-feedback">Todo estupendo</div>
                <!-- mensaje si no valida -->
                <div class="invalid-feedback">Introduce tu apellido</div>
            </div>
            <div class="p-sm-12 mt-3">
                <label for="edad" class="form-label">Edad:</label>
                <input id="edad" type="number" class="form-control was-validated edadE" required="" min="0">
                <div class="valid-feedback">Todo estupendo</div>
                <!-- mensaje si no valida -->
                <div class="invalid-feedback">Introduce tu edad</div>
            </div>
            <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <input type="email" class="form-control w-50" id="email" value="" pattern="[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" required >
                <!-- mensaje si valida -->
                <div class="valid-feedback">Todo estupendo</div>
                <!-- mensaje si no valida -->
                <div class="invalid-feedback">El correo es invalido</div>     
            </div>
            <div class="p-sm-12 mt-3">
                <label for="telefono" class="form-label">Teléfono:</label>
                <input id="telefono" type="tel" class="form-control was-validated telefonoE" required pattern="[0-9]{3}-[0-9]{3}-[0-9]{4}">
                <div class="valid-feedback">Todo estupendo</div>
                <!-- mensaje si no valida -->
                <div class="invalid-feedback">Introduce un número de teléfono válido (ejemplo: 123-456-7890)</div>
            </div>
            <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">Password</label>
                    <input type="password" class="form-control w-50" id="password" required maxlength="16" minlength="6">
                    <!-- mensaje si valida -->
                    <div class="valid-feedback">Todo estupendo</div>
                    <!-- mensaje si no valida -->
                    <div class="invalid-feedback">Minimo 6 y maximo 16</div>
                </div>   
            <div id="avatar" class="w-25 main"></div>
            <button id="enviar" type="submit" class="btn btn-primary">Registrarse</button>
        </form>
    </div>
    `,
    init: () => {
        document.getElementById('body').innerHTML += editarPerfil.template;        
        document.getElementById('body').style.backgroundColor = 'background-color: aquamarine;';
        /*.header{
            text-align: center;
            height: 200px;
            padding: 12px;
        }
        .avatar{
            width: 200px;
            height: 200px;
            border-radius: 50%;
        }*/
    },
    unload: async () => {
        await editarPerfil.script();
        const child = document.getElementById('editarperfil');                 
        document.getElementById('body').removeChild(child);
    },
    script: () => {
        return new Promise((resolve) => { 
            
            //detactamos el id enviar del boton
            const botonRegistro = document.querySelector("#enviar")
            //escuchamos el evento click y a�adimos el elemento
            //botonRegistro.addEventListener("click", router.a�adir)
            //seleccionamos los inputs
            const input = document.querySelector("input")
            //a�adimos el evento keydown, comprovamos si tiene la clase nick. Si es true, a�ade el avatar
            input.addEventListener("keydown", (event) => {
                if (event.target.classList.contains('nombre')) {
                    router.avatar(event)
                }
            })

            // //validacion
            document.querySelector('#nombre').classList.add('was-validated');
            // para form2
            document.querySelector('#enviar').addEventListener('click', (e) => {
                e.preventDefault();
                console.log('validandooooo');
                document.querySelector('#form2').classList.add('was-validated');
                if (form2.checkValidity()) {
                    form2.classList.remove('was-validated')
                }
            });

            const main = document.querySelector('.main')
            main.addEventListener("keydown", (event) => {
                console.log("aaa")
                if (event.target.classList.contains('nombreE')) {
                    let svgCode = multiavatar(event.target.value)
                    document.querySelector('#avatar').innerHTML = svgCode
                }
            })
            resolve();
        });                     
    }
}