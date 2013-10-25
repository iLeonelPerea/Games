/**********************************************************************/
/* Autor: Francisco I. Leyva
 * P�gina web: http://www.panchosoft.com
 * Correo electr�nico: yagami_2@hotmail.com
 *
 * Programa que permite jugar al tres en raya, gato, o tic tac toe contra otra
 * persona o contra la m�quina. Implementando el algoritmo minimax, �rboles,
 * recursi�n, etc.
 *
/**********************************************************************/
package pong;
import java.awt.Dimension;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.*;
public class Main {

    public Main() {
    }
    
    public static void main(String[] args) {
        try {
            //JFrame.setDefaultLookAndFeelDecorated(true);
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (ClassNotFoundException ex) {
            Logger.getLogger(Main.class.getName()).log(Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            Logger.getLogger(Main.class.getName()).log(Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            Logger.getLogger(Main.class.getName()).log(Level.SEVERE, null, ex);
        } catch (UnsupportedLookAndFeelException ex) {
            Logger.getLogger(Main.class.getName()).log(Level.SEVERE, null, ex);
        }
        new Animacion();
        /*Creamos una nueva instancia de nuestro gato.*/
        new Mundo3D();
    }
    
}
