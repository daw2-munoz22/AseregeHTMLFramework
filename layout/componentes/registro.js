class Usuario{
    constructor(nombre, apellido, edad, sexo, email, telefono, pass){
        
        this.nombre = nombre;
        this.apellido = apellido;
        this.edad = new Date(edad);       
        this.sexo = sexo;
        this.email = email;
        this.telefono = telefono;
        this.pass = pass;    
    }
}


// para form2
document.querySelector('#enviar2').addEventListener('click', (e)=>{
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
    if(form2.checkValidity()){
        form2.classList.remove('was-validated');       
        const json = JSON.stringify(usuario);
        console.log(json);
    }
    
});