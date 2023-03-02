document.querySelector('#form2').classList.add('was-validated');
    if(form2.checkValidity()){
        form2.classList.remove('was-validated');       
        // const json = JSON.stringify(usuario);
        // console.log(json);
    }