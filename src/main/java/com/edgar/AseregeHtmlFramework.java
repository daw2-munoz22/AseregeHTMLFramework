//---AseregeHtmlFramework.java---//
/*
 * Copyright (c) 2023 Edgar Muñoz
 * All rights reserved.
 * Aserege Html Framework
 */
package com.edgar;

//DEPENDENCIAS
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class AseregeHtmlFramework {
    public static void main(String[] args) {
        try {
            ServerSocket socketServidor = new ServerSocket(8080);
            //por cada usuario conectado, crea una instancia en un hilo  de la clase PageManager
            while (true) {
                Socket socketCliente = socketServidor.accept();
                PageManager pageInstance = new PageManager(socketCliente);

                // thread es el hilo que es el que se encarga de supervisar el estado de la instancia
                Thread hilo = new Thread(pageInstance);

                hilo.start(); //iniciar la maquina de estados
            }
        } catch (IOException ex) {
            System.out.println("Error al iniciar el servidor: " + ex.getMessage()); //devolver el error establecido
        }
    }
}
