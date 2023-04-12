import {API} from "../js/API.js";
import { config } from "../settings/config.js";

export const panelAdministracion = {
    template: `
    <table id="templatepage" class="table table-striped table-dark">
        <thead>
            <tr>
                  <th scope="col">Id</th>
                  <th scope="col">Nombre</th>
                  <th scope="col">Apellido</th>
                  <th scope="col">Edad</th>
                  <th scope="col">Sexo</th>
                  <th scope="col">Email</th>
                  <th scope="col">Telefono</th>
                  <th scope="col">Contraseña</th>
                  <th scope="col">Rol</th>                      
                  <th scope="col">Editar</th>
                  <th scope="col">Borrar</th>
            </tr>
        </thead>
        <tbody id="cuerpoTabla">
        
        </tbody>
</table>

    `,
    init: () => {
        document.getElementById('body').innerHTML += panelAdministracion.template;
    },
    unload: async () => {
        // Espera a que la promesa del método script se resuelva antes de continuar con unload
        await panelAdministracion.script();
        const child = document.getElementById('templatepage');
        document.getElementById('body').removeChild(child);

    },
    script: () => {
        return new Promise((resolve) => {
            const UserTable = new API().httpGetAsync("https://localhost:44376/api/users", function (response) {

                const json = JSON.parse(response);
                json.forEach(usuario => {
                    let permission = "Usuario"
                    if (parseInt(usuario.Roles_idroles, 10) < 2) {
                        permission = "Administrador"
                    }
                    document.getElementById("cuerpoTabla").innerHTML +=
                        `<tr>
                        <th scope="row">${usuario.ID}</th>                        
                        <td class="px-5">${usuario.Nombre}</td>
                        <td class="px-5">${usuario.Apellido}</td>
                        <td class="px-5">${usuario.Edad}</td>
                        <td class="px-5">${usuario.Sexo}</td>
                        <td class="px-5">${usuario.Email}</td>
                        <td class="px-5">${usuario.Telefono}</td>
                        <td class="px-5"><button data-id="${usuario.ID}" type="button" class="btn btn-warning eliminar" >Recuperar Contraseña</button></td>
                        <td class="px-5">${permission}</td>                        
                        <td class="px-5"><button data-id="${usuario.ID}" type="button" class="btn btn-info editar"  data-bs-toggle="modal" data-bs-target="#exampleModal">Editar</button></td>        
                        <td class="px-5"><button data-id="${usuario.ID}" type="button" class="btn btn-danger eliminar" >Eliminar</button></td>
              </tr>`
                });
                console.log('Response received:' + UserTable);
                // Resuelve la promesa para indicar que el método ha terminado
                resolve();
            });
        });
    }
}