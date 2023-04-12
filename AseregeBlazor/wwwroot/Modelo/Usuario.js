export class Usuario {
    constructor(nombre, apellido, edad, sexo, email, telefono, pass) {

        this.nombre = nombre;
        this.apellido = apellido;
        this.edad = new Date(edad).getUTCFullYear();
        this.sexo = sexo;
        this.email = email;
        this.telefono = telefono;
        this.pass = pass;
    }
}