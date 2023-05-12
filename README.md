# AseregeHtmlFramework

El proyecto a realizar es una página web sobre Barcelona. Esta web es 
una SSR(Server Site Rendering), por lo cual, todas las vistas (páginas) 
se ejecutan en el servidor, aunque el comportamiento será de una SPA 
(Single Page Aplication), por lo tanto, se verá como una única página. 
Se pretende utilizar un servidor externo para poder desarrollar la 
aplicación y otro servidor, para almacenar la base de datos de MySQL. 
Los servidores que se van a utlizar son los siguientes: Replit para la 
aplicación y Freedb, para la base de datos. En caso de que el servidor 
dónde se almacena la web no cumpla los requisitos mínimos, se 
utilizará otro servidor, como, por ejemplo, Freeasphosting. El 
proyecto se dividirá en tres apartados diferentes. En primer lugar, se 
deberá realizar el BackEnd que será realizado utilizando el lenguaje 
de Java 16. Si se tuviera que cambiar el leguaje, optaría por C# debido 
a que me resulta más sencillo de programar y de estructurar cada clase 
y cada connector (API) que se va a desarrollar. En segundo lugar, se 
deberá de implementar el FrontEnd, que almacenará las páginas que 
se van a renderizar en el navegador. Se deberá estructurar en
diferentes carpetas para poder tener una mejor organización.

# Configuracion AseregeHtmlFramework

Este JSON contiene todas las propiedades necesarias para hacer la 
conexión a la Base de datos de MySQL. Hay que tener en cuenta de 
que, si uno de las propiedades pones la configuración de manera 
errónea, automáticamente se cerrará la Base de datos, impidiendo 
conectarse a ella. A continuación, tendrás las explicaciones de cada 
propiedad, y un ejemplo del JSON.

- "hostname" indica el nombre del host donde se encuentra la base de datos, en este caso "localhost" indica que la base de datos está en el mismo servidor que la aplicación que se  conectará a ella.
- "port" es el número del puerto donde se encuentra la base de datos. En este caso, el puerto es 3306, que es el puerto predeterminado para MySQL. 
- "username" es el nombre de usuario para acceder a la base de datos. En este caso, es "root", que es el usuario predeterminado en MySQL con permisos completos.
- "password" es la contraseña para el usuario de la base de datos. En este caso, la contraseña es "root", pero en una configuración real, sería mejor utilizar una contraseña más segura y almacenarla de manera segura.
- "databaseName" indica el nombre de la base de datos a la que se quiere conectar. En este caso, es "barcelonaweb".
`{
 "hostname": "localhost",
 "port": 3306,
 "username": "root",
 "password": "root",
 "databaseName": "freedb_barcelonaweb"
}`
 Este JSON, hace referencia al fichero de configuración aserege_release.json y a aserege.json (DEBUG)

## Instalar en Windows
1: Para instalar Aserege se debe instalar MYSQL/MARIADB

2: Ejecutar el instalador de Aserege.

3: Configuramos el archivo aserege_release.json y el aserege.json para configurar la conexion con la base de datos

4: Iniciamos el servidor de AseregeBarcelonaWeb.exe

5: Opcional!. Podemos añadir a nuestro servidor un Servicio para que el server se ejecute al arrancar con Windows

## Instalar en Linux

1: Para instalar Aserege se debe instalar MYSQL/MARIADB
```
sudo apt update
sudo apt-get install git
sudo apt-get install mysql-server
sudo service mysql start
sudo service mysql status
sudo apt install apt-transport-https ca-certificates
git clone --recursive https://github.com/daw2-munoz22/AseregeHtmlFramework.git
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg 
--dearmor > microsoft.asc.gpg
sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
wget -q https://packages.microsoft.com/config/debian/10/prod.list
sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list
sudo apt-get update; sudo apt-get install -y dotnet-sdk-6.0 aspnetcore-runtime-6.0
dotnet --version
```
2: Ejecutar el instalador de Aserege.

3: Inciamos el demonio

4: Abrimos el servidor

## Licensing
Aserege Framework Licencia de usuario final (EULA)

Versión 1.0, 2023

Este acuerdo de licencia de usuario final (en adelante, "Acuerdo") es un acuerdo legal entre usted (en adelante, "Licenciatario") y Edgar Muñoz (en adelante, "Licenciante") para el software Aserege Framework (en adelante, "Software").

Al adquirir, instalar o utilizar el Software, el Licenciatario acepta los términos y condiciones de este Acuerdo. Si no acepta los términos y condiciones de este Acuerdo, no utilice ni instale el Software.

Concesión de licencia
El Licenciante otorga al Licenciatario una licencia no exclusiva, intransferible y limitada para utilizar el Software en un solo dispositivo a la vez. La licencia es válida de manera permanente y no se puede transferir ni sublicenciar.

Restricciones de uso
El Licenciatario no podrá modificar, descompilar, desensamblar ni realizar ingeniería inversa del Software. El Licenciatario no podrá revender ni transferir la licencia del Software a terceros sin la autorización previa y por escrito del Licenciante.

Propiedad intelectual
El Software es propiedad exclusiva del Licenciante y está protegido por las leyes de propiedad intelectual. El Licenciatario reconoce que el Licenciante conserva todos los derechos de propiedad intelectual sobre el Software, incluidos los derechos de autor, patentes y marcas comerciales.

Precio y pago
El Licenciatario pagará al Licenciante la cantidad de 2000€ para obtener la licencia completa del Software. Además, el Licenciatario pagará una tarifa cada tres meses para mantener la versión de demostración del Software.

Garantía limitada
El Licenciante garantiza que el Software se ajusta a las especificaciones del Licenciante. El Licenciante no garantiza que el Software sea libre de errores o que funcionará sin interrupciones. El Licenciatario acepta que el uso del Software es bajo su propio riesgo.

Limitación de responsabilidad
El Licenciante no será responsable de ningún daño especial, incidental, indirecto o consecuente que surja del uso o la imposibilidad de usar el Software, incluso si el Licenciante ha sido informado de la posibilidad de dichos daños.

Término y terminación
Este Acuerdo entrará en vigor en la fecha de adquisición, instalación o uso del Software y continuará vigente hasta su terminación. El Licenciante podrá rescindir este Acuerdo de inmediato si el Licenciatario incumple alguna de las condiciones establecidas en este Acuerdo.

Ley aplicable
Este Acuerdo se regirá e interpretará de acuerdo con las leyes del España, sin dar efecto a los principios de conflicto de leyes

## Agradecimiento
Tecnologia para buscar información: ChatGPT
