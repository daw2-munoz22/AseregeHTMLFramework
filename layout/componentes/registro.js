class Usuario{
    constructor(nombre, apellido, edad, sexo, email, telefono, passwordseguro){
        
        this.nombre = nombre;
        this.apellido = apellido;
        this.edad = new Number(edad);       
        this.sexo = sexo;
        this.email = email;
        this.telefono = telefono;
        this.passwordseguro = passwordseguro;    
    }
}
let usuario = new Usuario(nombre, apellido, edad, sexo, email, telefono, passwordseguro, Roles_idroles);
const edad = document.querySelector('#edad').classList.add('was-validated');
const nombre = document.getElementById('nombre');
const email = document.getElementById('email');
// para form2
document.querySelector('#enviar2').addEventListener('click', (e)=>{
    
    e.preventDefault();
    console.log('validandooooo');
    //Añadimos la clase was-validated para que se muestre la validación de boostrap
    document.querySelector('#form2').classList.add('was-validated');
    if(form2.checkValidity()){
        form2.classList.remove('was-validated');
       
        // const json = JSON.stringify(person);
    }
});