/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package pong;

import java.awt.AWTEvent;
import java.awt.event.KeyEvent;
import java.util.Enumeration;
import javax.media.j3d.Behavior;
import javax.media.j3d.WakeupCondition;
import javax.media.j3d.WakeupOnAWTEvent;

/**
 *
 * @author osaenz
 */
public class BehaviorTeclado extends Behavior{
    private WakeupCondition condicion;
    private Mundo3D mundo;
    private KeyEvent evtAnterior;
    private boolean arriba1, abajo1, izquierda1, derecha1,arriba2, abajo2, izquierda2, derecha2;
    public BehaviorTeclado(Mundo3D m){
        mundo=m;
        condicion = new WakeupOnAWTEvent(AWTEvent.KEY_EVENT_MASK);
        arriba1 = false;
        abajo1 = false;
        izquierda1 = false;
        derecha1 = false;
        arriba2 = false;
        abajo2 = false;
        izquierda2 = false;
        derecha2 = false;
    }


    public void initialize() {
        wakeupOn(condicion);
    }

    public void processStimulus(Enumeration arg0) {
        KeyEvent evt=(KeyEvent)((WakeupOnAWTEvent)arg0.nextElement()).getAWTEvent()[0];
        if (evt.getID() == KeyEvent.KEY_PRESSED) {
            switch (evt.getKeyCode()){
                case KeyEvent.VK_LEFT:
                    izquierda1 = true;
                break;
                case KeyEvent.VK_RIGHT:
                    derecha1 = true;
                break;
                case KeyEvent.VK_UP:
                    arriba1 = true;
                break;
                case KeyEvent.VK_DOWN:
                    abajo1 = true;
                break;
                case KeyEvent.VK_A:
                    izquierda2=true;
                break;
                case KeyEvent.VK_D:
                    derecha2=true;
                break;
                case KeyEvent.VK_W:
                    arriba2=true;
                break;
                case KeyEvent.VK_S:
                    abajo2=true;
                break;
            }
        }
        if(evt.getID()==KeyEvent.KEY_RELEASED){
            switch (evt.getKeyCode()){
                case KeyEvent.VK_LEFT:
                    izquierda1=false;
                break;
                case KeyEvent.VK_RIGHT:
                    derecha1=false;
                break;
                case KeyEvent.VK_UP:
                    arriba1 = false;
                break;
                case KeyEvent.VK_DOWN:
                    abajo1 = false;
                break;
                case KeyEvent.VK_A:
                    izquierda2 = false;
                break;
                case KeyEvent.VK_D:
                    derecha2 = false;
                break;
                case KeyEvent.VK_W:
                    arriba2 = false;
                break;
                case KeyEvent.VK_S:
                    abajo2 = false;
                break;
                case KeyEvent.VK_ENTER:
                    mundo.pausado= !mundo.pausado;
                    break;
            }
        }

        if(arriba1){
            mundo.moverBarraP1(Mundo3D.masY);
        }
        if(abajo1){
            mundo.moverBarraP1(Mundo3D.menosY);
        }
        if(izquierda1){
            mundo.moverBarraP1(Mundo3D.masZ);
        }
        if(derecha1){
            mundo.moverBarraP1(Mundo3D.menosZ);
        }
        if(arriba2){
            mundo.moverBarraP2(Mundo3D.masY);
        }
        if(abajo2){
            mundo.moverBarraP2(Mundo3D.menosY);
        }
        if(izquierda2){
            mundo.moverBarraP2(Mundo3D.masZ);
        }
        if(derecha2){
            mundo.moverBarraP2(Mundo3D.menosZ);
        }
        evtAnterior = evt;
        wakeupOn(condicion);
    }
}
