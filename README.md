# AseregeHtmlFramework

Se trata de una página web sobre Barcelona (para turismos, cultura..). Esta web es una SSR(Server Site Rendering), por lo cual, todas las vistas (páginas) se ejecutan en el servidor, aunque el comportamiento será de una SPA (Single Page Aplication), por lo tanto, se verá como una única página.
El servidor AseregeHTMLFramework es un servidor que ha sido creado en el lenguaje de JAVA16 en el cual, se gestiona el control de los permisos de los usuarios, la manipulación de la Base de Datos de MySQL y, el sistema de validación del BackEnd propio. Este framework está vinculado con el FrontEnd, cuyas páginas realizadas desde el propio servidor, contiene el lenguage de marcado HTML5, los complementos para que la web que sea más bonita, se ha utilizado CSS3. La lógica del proyecto, está basada en el lenguaje de JavaScript, en la cual, se implementa el sistema de validación y la conexión al BackEnd, con el lenguaje de programación mencionado anteriormente.

## Usage

Para lanzar el servidor, debes de ejecutar el siguiente comando: `java -jar AseregeHTMLFramework.jar`  

AseregeHTMLFramework require de base JAVA16 y MySQL/MARIADB.

# Configuracion AseregeHtmlFramework

El servidor, genera el JSON (diccionario clave-valor) en el cual, está compuesto por los datos que tengas. Préviamente, deberas de haber la configuración de la conexión a MySQL y del propio servidor. Un ejemplo de conexión sería el siguente:
`{
"hostname":"localhost",
"port":3306,
"username":"root",
"password":"root",
"databaseName":"barcelonaweb",
"IV":"C#:_ytresw3456j?"
}`
 Este JSON, hace referencia al fichero de configuración aserege.conf

## Compilar para Windows

Para instalar nodejs utilizaremos el siguiente comando
```
winget install -e --id OpenJS.NodeJS
```
Alternativa se puede instalar Nodejs.
Si disponemos un PC de x86 (32 Bits) o un x64 (64 bits)
x64 -> https://nodejs.org/dist/v18.14.2/node-v18.14.2-x64.msi
x86 -> https://nodejs.org/dist/v18.14.2/node-v18.14.2-x86.msi

Para más información debes ir al siguiente enlace:  https://nodejs.org/en/download/
Se debe seguir los pasos del asistente de instalación de Node.js
Una vez instalado y configurado, deberás ejecutar los siguientes comandos: 
```
winget install -e --id Git.Git
git clone --recursive https://github.com/daw2-munoz22/AseregeHtmlFramework.git
cd AseregeHtmlFramework
cd layout
npm install
winget install EclipseAdoptium.Temurin.17.JDK
```
Compilamos la solucion .pom con el editor de preferencia

## Building on Linux
```
sudo apt update
sudo apt-get install git
sudo apt-get install nodejs
sudo apt-get install mysql-server
sudo service mysql start
sudo service mysql status
sudo apt install apt-transport-https ca-certificates
wget -qO - https://adoptopenjdk.jfrog.io/adoptopenjdk/api/gpg/key/public | sudo apt-key add -
sudo add-apt-repository --yes https://adoptopenjdk.jfrog.io/adoptopenjdk/deb/
```
Puede utilizar la version 16 o la ultima (latest)

Última version de java
```
sudo apt install adoptopenjdk-latest-hotspot
```

Java 16
```
sudo apt install adoptopenjdk-16-hotspot
```

Para comprovar la versión de java instalado, debes de ejecutar la siguiente instrucción:
```
java -version
```

Para compilar el proyecto
```
git clone --recursive https://github.com/daw2-munoz22/AseregeHtmlFramework.git
cd AseregeHtmlFramework
cd layout
npm install
```
Compilamos la solucion .pom con el editor de preferencia

## Building on Mac OS X


Para instalar Adoptium Temurin OpenJDK en macOS, puedes seguir los siguientes pasos:
Abre la página de descarga de Adoptium Temurin OpenJDK en tu navegador web: https://adoptium.net/
Haz clic en el botón "Download" debajo de la versión de OpenJDK que deseas instalar.
En la siguiente página, selecciona "macOS" como tu sistema operativo.
Haz clic en el enlace de descarga para el archivo ZIP correspondiente a la arquitectura de tu sistema (por ejemplo, "x64").
Una vez que se descargue el archivo ZIP, ábrelo y extrae su contenido en la carpeta donde deseas instalar OpenJDK. Puedes hacer esto arrastrando y soltando la carpeta extraída en la ubicación deseada.

Abre la terminal en tu macOS.

Para configurar la variable de entorno JAVA_HOME, abre un archivo de inicio de shell como ~/.bashrc o ~/.zshrc en tu editor de texto preferido y agrega la siguiente línea al final del archivo:
```
export JAVA_HOME=/ruta/hacia/la/carpeta/de/OpenJDK
```
Reemplaza "/ruta/hacia/la/carpeta/de/OpenJDK" con la ruta absoluta a la carpeta donde extrajiste el contenido del archivo ZIP. Guarda y cierra el archivo.
Para que los cambios en la variable de entorno se apliquen, cierra y vuelve a abrir la terminal o ejecuta el siguiente comando en la terminal:
```
source ~/.bashrc
```
o
```
source ~/.zshrc
```
Verifica que la instalación se haya completado correctamente ejecutando el siguiente comando en la terminal:
```
java -version
```

Para instalar Node.js en macOS, puedes seguir los siguientes pasos:
Abre la página de descarga de Node.js en tu navegador web: https://nodejs.org/es/download/
Haz clic en el botón "Descargar" debajo de la versión recomendada de Node.js para tu sistema operativo.
Selecciona el paquete de instalación adecuado para tu sistema operativo macOS. El paquete más común es el que tiene el formato "macOS Installer (.pkg)".
Una vez que se descargue el archivo, haz doble clic en él para iniciar el instalador.
Sigue las instrucciones del instalador para completar la instalación de Node.js. Asegúrate de seleccionar la opción "Instalar los componentes necesarios para el entorno de desarrollo" durante la instalación.
Después de que se complete la instalación, verifica que Node.js esté instalado correctamente ejecutando el siguiente comando en la terminal:
```
node -v
```
Escriba el comando siguente para comprobar si dispone de git
```
git --version
```
En el caso de que de error puede bajarse GIT en la siguente pagina https://git-scm.com/downloads

Para instalar MySQL o MariaDB en macOS, puedes seguir los siguientes pasos:
Abre la terminal en tu macOS.
Instala Homebrew si aún no lo tienes instalado en tu sistema. Para hacerlo, ejecuta el siguiente comando en la terminal:
```
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
brew install mysql
```
Sigue las instrucciones en la terminal para completar la instalación. Esto puede incluir la creación de un usuario de base de datos y una contraseña.
Después de que se complete la instalación, inicia el servicio de base de datos ejecutando el siguiente comando en la terminal:

Para iniciar el servicio de MySQL:
```
brew services start mysql
```
Verifica que el servicio de base de datos esté en funcionamiento ejecutando el siguiente comando en la terminal:

Para verificar el estado del servicio de MySQL:
```
brew services list | grep mysql
```
Para acceder a la línea de comandos de MySQL o MariaDB, ejecuta el siguiente comando en la terminal:

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
