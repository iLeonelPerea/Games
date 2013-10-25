/*
 * Clase Animación. Encargada de animar el splash.
 * 
 */

package pong;

import java.awt.*;
import javax.swing.*;
import java.net.URL;

/**
 *
 * @author ISAAC
 */
public class Animacion {
    private SplashScreen splash; // Pantalla que va a mostrar el splash.
    private Graphics2D g;

    public Animacion(){
        splash = SplashScreen.getSplashScreen();//Método estático que regresa el splash si existe.
        if(splash == null){
            return;
        }
        g = splash.createGraphics();
        if (g==null){
            return;
        }
        URL imgURL = splash.getImageURL();
        //Dibujar la imagen
        splash.update();
        try{
            Thread.sleep(1000);
        }
        catch(InterruptedException e){
            System.out.println(e.toString());
        }
    }

    public void clear(int i) {
        Color transColor=new Color(0f,0f,0f,0f);
        g.setBackground(transColor);
        g.clearRect(0, 0, 300, 300);
        g.drawString(""+i,150,150);
    }
}
