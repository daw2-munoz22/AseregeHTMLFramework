package com.edgar;
import com.edgar.API.RolesAPI;
import com.edgar.API.UsuarioAPI;
import com.edgar.Model.Usuario;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.*;
import java.net.Socket;
import java.nio.file.Files;
import java.sql.SQLOutput;

/**
 *
 * @author Edgar Muñoz
 */

//esta clase utiliza la interfaz Runnable, que viene creada de java y se importa con esta libreria: import java.net.Socket;
class PageManager implements Runnable {
    private Socket cliente;

    //definir el constructor, que le pasa el socket de red
    public PageManager(Socket socket) {
        this.cliente = socket;
    }

    public void run() {
        try {
            InputStream entrada = cliente.getInputStream();
            BufferedReader lector = new BufferedReader(new InputStreamReader(entrada));
            String lineaPeticion = lector.readLine();

            String[] partes = lineaPeticion.split(" ");
            String metodo = partes[0];
            String recurso = partes[1];

            //si el método es GET

            switch(metodo) {
                case "GET":
                    if (recurso.startsWith("/api/")) {

                        String json = "{}";

                        if (recurso.equals("/api/roles")) {
                            try {
                                json = new RolesAPI().GET();
                            } catch (Exception e) {
                                throw new RuntimeException(e);
                            }
                        }
                        if (recurso.equals("/api/usuarios")) {
                            try {
                                json = new UsuarioAPI().GET();
                            } catch (Exception e) {
                                throw new RuntimeException(e);
                            }
                        }


                        byte[] contenido = json.getBytes(); //guarda los datos en un byte array (0 y 1)
                        String respuesta = "HTTP/1.1 200 OK\r\n";
                        respuesta += "Content-Type: " + obtenerTipoContenido(recurso) + "\r\n";
                        respuesta += "Content-Length: " + contenido.length + "\r\n";
                        respuesta += "\r\n";
                        cliente.getOutputStream().write(respuesta.getBytes());
                        cliente.getOutputStream().write(contenido);

                        //si no es una api
                    } else{
                        File archivo = new File("layout" + recurso);
                        if (archivo.exists()) {
                            byte[] contenido = Files.readAllBytes(archivo.toPath());
                            String respuesta = "HTTP/1.1 200 OK\r\n";
                            respuesta += "Content-Type: " + obtenerTipoContenido(recurso) + "\r\n";
                            respuesta += "Content-Length: " + contenido.length + "\r\n";
                            respuesta += "\r\n";
                            cliente.getOutputStream().write(respuesta.getBytes());
                            cliente.getOutputStream().write(contenido);
                        } else {
                            String respuesta = "HTTP/1.1 404 Not Found\r\n\r\n";
                            cliente.getOutputStream().write(respuesta.getBytes());
                        }
                    }
                    break;
                case "POST":
                    // Procesar los datos del cliente y guardarlos en ua base de datos
                    if(recurso.startsWith("/api")){
                        String json = "";
                        
                        if (recurso.startsWith("/api/roles")) {
                            try {
                                // Leer el cuerpo de la solicitud
                                StringBuilder body = new StringBuilder();
                                while (lector.ready()) {
                                    body.append((char) lector.read());
                                }

                                int startIndex = body.indexOf("Content-Length: "); // encuentra la posición del inicio del JSON
                                String resultPeticion = body.substring(startIndex); // extrae el substring desde el inicio del JSON hasta el final

                                int jsonStart = resultPeticion.indexOf("{"); // encuentra la posición del inicio del JSON
                                String jsonbody = resultPeticion.substring(jsonStart); // extrae el substring desde el inicio del JSON hasta el final

                                json = new RolesAPI().POST(jsonbody);
                                json = "{'message': 'Rol creado correctamente.'}";
                            } catch (Exception e) {
                                throw new RuntimeException(e);
                            }
                        }
                        if (recurso.equals("/api/usuarios")) {
                            try {
                                // Leer el cuerpo de la solicitud
                                StringBuilder body = new StringBuilder();
                                while (lector.ready()) {
                                    body.append((char) lector.read());
                                }

                                int startIndex = body.indexOf("Content-Length: "); // encuentra la posición del inicio del JSON
                                String resultPeticion = body.substring(startIndex); // extrae el substring desde el inicio del JSON hasta el final

                                int jsonStart = resultPeticion.indexOf("{"); // encuentra la posición del inicio del JSON
                                String jsonbody = resultPeticion.substring(jsonStart); // extrae el substring desde el inicio del JSON hasta el final

                                json = new UsuarioAPI().POST(jsonbody);
                                json = "{'message': 'Usuario creado correctamente.'}";
                            } catch (Exception e) {
                                throw new RuntimeException(e);
                            }
                        }

                        byte[] contenido = json.getBytes();
                        String respuesta = "HTTP/1.1 200 OK\r\n";
                        respuesta += "Content-Type: " + obtenerTipoContenido(recurso) + "\r\n";
                        respuesta += "Content-Length: " + contenido.length + "\r\n";
                        respuesta += "\r\n";
                        cliente.getOutputStream().write(respuesta.getBytes());
                        cliente.getOutputStream().write(contenido);

                    }
                    break;
                case "PUT":
                    // Actualizar un archivo o una base de datos con los datos del cliente
                    if (recurso.startsWith("/api/")) {
                        String json = "{}";
                        if (recurso.equals("/api/roles")) {
                            try {
                                // procesar los datos del cliente y actualizar la base de datos
                                json = new RolesAPI().PUT();
                            } catch (Exception e) {
                                throw new RuntimeException(e);
                            }
                        }
                        if (recurso.equals("/api/usuarios")) {
                            try {
                                // procesar los datos del cliente y actualizar la base de datos
                                json = new UsuarioAPI().PUT();
                            } catch (Exception e) {
                                throw new RuntimeException(e);
                            }
                        }
                        byte[] contenido = json.getBytes();
                        String respuesta = "HTTP/1.1 200 OK\r\n";
                        respuesta += "Content-Type: " + obtenerTipoContenido(recurso) + "\r\n";
                        respuesta += "Content-Length: " + contenido.length + "\r\n";
                        respuesta += "\r\n";
                        cliente.getOutputStream().write(respuesta.getBytes());
                        cliente.getOutputStream().write(contenido);
                    }
                    break;
                case "DELETE":
                    if (recurso.startsWith("/api")) {
                        String json = "{}";

                        if (recurso.startsWith("/api/roles")) {
                            try {
                                json = new RolesAPI().DELETE();
                            } catch (Exception e) {
                                throw new RuntimeException(e);
                            }
                        }
                        if (recurso.startsWith("/api/usuarios")) {
                            try {
                                json = new UsuarioAPI().DELETE();
                            } catch (Exception e) {
                                throw new RuntimeException(e);
                            }
                        }

                        byte[] contenido = json.getBytes();
                        String respuesta = "HTTP/1.1 200 OK\r\n";
                        respuesta += "Content-Type: " + obtenerTipoContenido(recurso) + "\r\n";
                        respuesta += "Content-Length: " + contenido.length + "\r\n";
                        respuesta += "\r\n";
                        cliente.getOutputStream().write(respuesta.getBytes());
                        cliente.getOutputStream().write(contenido);
                    } else {
                        File archivo = new File("layout" + recurso);
                        if (archivo.exists()) {
                            archivo.delete(); // eliminar el archivo correspondiente
                            String respuesta = "HTTP/1.1 200 OK\r\n\r\n";
                            cliente.getOutputStream().write(respuesta.getBytes());
                        } else {
                            String respuesta = "HTTP/1.1 404 Not Found\r\n\r\n";
                            cliente.getOutputStream().write(respuesta.getBytes());
                        }
                    }
                    break;
                default:
                    String respuesta = "HTTP/1.1 405 Method Not Allowed\r\n\r\n";
                    cliente.getOutputStream().write(respuesta.getBytes());
            }

            lector.close();
            entrada.close();
            cliente.close();
        } catch (IOException ex) {
            System.out.println("Error al procesar la petición: " + ex.getMessage());
        }
    }

    private static String obtenerTipoContenido(String recurso) {
        if (recurso.endsWith(".html")) {
            return "text/html";
        } else if (recurso.endsWith(".css")) {
            return "text/css";
        } else if (recurso.endsWith(".js")) {
            return "text/javascript";
        } else if (recurso.endsWith(".png")) {
            return "image/png";
        } else if (recurso.endsWith(".jpg") || recurso.endsWith(".jpeg")) {
            return "image/jpeg";
        } else if (recurso.endsWith(".gif")) {
            return "image/gif";
        } else {
            return "application/octet-stream";
        }
    }
}

