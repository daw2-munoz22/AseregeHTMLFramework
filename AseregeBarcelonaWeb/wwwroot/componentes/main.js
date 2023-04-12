import{registro} from "../componentes/registro.js"                
import{login} from "../componentes/login.js"         
import {panelAdministracion} from "../componentes/panelAdministracion.js"          
import {editarPerfil} from "../componentes/editarPerfil.js"

export const main = {
    template:     
    `<div id="main">
    <header>
        <div class="container">
            <div class="row">
                <div class="col">
                    <ul class="nav">
                        <li class="nav-item">
                            <a class="nav-link" aria-current="page" href="#" aria-disabled="false">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#" aria-disabled="false">Transportes</a>
                            <button id="transporteButton" type="button" class="btn btn-primary">Primary</button>
                        </li>                    
                        <li class="nav-item">
                            <a class="nav-link" href="#" aria-disabled="false">Visitas</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#" tabindex="-1" aria-disabled="false">Quienes Somos?</a>
                        </li>
                    </ul>
                </div>
                <div class="col">
                    <img src="images/avatar5.png" alt="avatar" class="avatar">
                </div>
            </div>
        </div>
    </header>
    <main>
        <div id="carouselExampleCaptions" class="carousel slide pt-4" data-bs-ride="carousel">
            <div class="carousel-indicators">
                <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="0" class="active"
                    aria-current="true" aria-label="Slide 1"></button>
                <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="1"
                    aria-label="Slide 2"></button>
                <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="2"
                    aria-label="Slide 3"></button>
            </div>
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <img src="images/campnou.jpg" class="d-block w-100" alt="Estadio FC Barcelona">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>First slide label</h5>
                        <p>Some representative placeholder content for the first slide.</p>
                    </div>
                </div>
                <div class="carousel-item">
                    <img src="images/plazaEspaña.jpg" class="d-block w-100" alt="plaza de España">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>Second slide label</h5>
                        <p>Some representative placeholder content for the second slide.</p>
                    </div>
                </div>
                <div class="carousel-item">
                    <img src="images/Montserrat.jpg" class="d-block w-100" alt="Montserrat">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>Third slide label</h5>
                        <p>Some representative placeholder content for the third slide.</p>
                    </div>
                </div>
            </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions"
                data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Previous</span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions"
                data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Next</span>
            </button>
        </div>
    </main>
    <footer>
        <p>Edgar Muñoz Manjón</p>
    </footer>
    </div>
    `,
    init: ()=>{
        document.getElementById('body').innerHTML += main.template;
        let img = document.querySelectorAll('img');
        let body = document.querySelectorAll('body');
        let header = document.querySelectorAll('header');
        //let enlace = document.querySelectorAll('a');
        const navLinks = document.querySelectorAll('a.nav-link');
        const enlaces = document.querySelectorAll('a.nav-link');
        const activeNavLink = document.querySelector('a.nav-link.active');
        const hovers = document.querySelectorAll('a.nav-link');
        const eventofocus = document.querySelectorAll('a.nav-link');
        const carousel = document.querySelector('.carousel');
        const carouselItems = document.querySelectorAll('.carousel-item');
        const avatars = document.querySelectorAll('.avatar');
        for(var i=0; i<img.length; i++){
            img[i].style.width ="25%";
        }
                        
        for(var i=0; i<body.length; i++){
            body[i].style.backgroundColor ="#85C1E9";
        } 

        for(var i =0; i<body.length; i++){
            header[i].style.background ="rgba(0, 0, 0, 0.7)"; 
            header[i].style.width ="100%";
            header[i].style.position ="fixed";
            header[i].style.zIndex ="100";
            header[i].style.border ="2px solid";

        }     
        
        /*for(var i =0; i<enlace.length; i++){
            enlace[i].style.color ="azure"; 

        }*/
        for(var i=0; i<navLinks.length; i++){
            navLinks[i].style.color ="azure"; 
        }   
        for (let i = 0; i < enlaces.length; i++) {
            enlaces[i].addEventListener('click', function() {
              this.style.backgroundColor = 'azure';
              this.style.color = 'azure';
            });
        }
        /*a.nav-link:active {
            background-color: azure;
            color: azure;
        }*/
        if (activeNavLink) {
            activeNavLink.style.color = 'tomato';
        }

        for (let i = 0; i < hovers.length; i++) {
            hovers[i].addEventListener('mouseover', function() {
              this.style.color = 'tomato';
            });
            
            // Agregar un EventListener para el evento "mouseout"
            hovers[i].addEventListener('mouseout', function() {
              this.style.color = ''; // Restablecer el color original
            });
        }
        for (let i = 0; i < eventofocus.length; i++) {
            eventofocus[i].addEventListener('focus', function() {
              this.style.color = 'azure';
            });
            
            // Agregar un EventListener para el evento "blur"
            eventofocus[i].addEventListener('blur', function() {
              this.style.color = ''; // Restablecer el color original
            });
        }
        /*.avatar {
            vertical-align: middle;
            width: 50px;
            height: 50px;
            border-radius: 50%;
            float: right;
        }*/
        for(var i=0; i<carousel.length; i++){
            carousel[i].style.height = '100px';
        }
        for (let i = 0; i < carouselItems.length; i++) {
            const img = carouselItems[i].querySelector('img');
            
            // Establecer la altura de la imagen
            img.style.height = '50%';
        }
        for (let i = 0; i < avatars.length; i++) {
            avatars[i].style.verticalAlign = 'middle';
            avatars[i].style.width = '50px';
            avatars[i].style.height = '50px';
            avatars[i].style.borderRadius = '50%';
            avatars[i].style.float = 'right';
        }
        
    },
    unload: ()=>{
        this.script.unload();
    }, 
    
    script: ()=>{            
        function unload() {        
            const child = document.getElementById('main');                 
            document.getElementById('body').removeChild(child);        
        }
        function OnTransportes() {        
            unload();
            console.log("hecho");
            panelAdministracion.init();
            panelAdministracion.script();
        }      
        document.getElementById("transporteButton").addEventListener("click", OnTransportes);       
    }
}