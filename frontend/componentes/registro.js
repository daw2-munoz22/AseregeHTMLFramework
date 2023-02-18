document.querySelector('#nick').classList.add('was-validated');
// para form2
document.querySelector('#enviar2').addEventListener('click', (e)=>{
    e.preventDefault();
    console.log('validandooooo');
    //Añadimos la clase was-validated para que se muestre la validación de boostrap
    document.querySelector('#form2').classList.add('was-validated');
    if(form2.checkValidity()){
        form2.classList.remove('was-validated')
    }
});