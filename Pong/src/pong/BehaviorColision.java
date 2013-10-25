/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package pong;


import java.awt.event.KeyEvent;
import java.util.Enumeration;
import javax.media.j3d.Behavior;
import javax.media.j3d.Node;
import javax.media.j3d.Shape3D;
import javax.media.j3d.WakeupCondition;
import javax.media.j3d.WakeupCriterion;
import javax.media.j3d.WakeupOnAWTEvent;
import javax.media.j3d.WakeupOnCollisionEntry;
import javax.media.j3d.SceneGraphPath;
/**
 *
 * @author osaenz
 */
public class BehaviorColision extends Behavior{
    private WakeupCondition condicion;
    private Mundo3D mundo;

    public BehaviorColision(Mundo3D m,Shape3D figura){
        mundo=m;
        condicion = new WakeupOnCollisionEntry(figura,WakeupOnCollisionEntry.USE_GEOMETRY);
        setSchedulingBounds(m.getLimites());
    }


    public void initialize() {
        wakeupOn(condicion);
        System.out.println("Initialize Colisión");
    }

    public void processStimulus(Enumeration arg0) {
        System.out.println("Colisionó");
            WakeupCriterion theCriterion = (WakeupCriterion) arg0.nextElement();
            if (theCriterion instanceof WakeupOnCollisionEntry) {
                Node theLeaf = ((WakeupOnCollisionEntry)theCriterion).getTriggeringPath().getObject();
                System.out.println(theLeaf.getUserData());
                if (theLeaf.getUserData()!=null){
                    if(theLeaf.getUserData().equals("abajo")){
                        mundo.rebotar(Mundo3D.ABAJO);
                    }
                    if(theLeaf.getUserData().equals("arriba")){
                        mundo.rebotar(Mundo3D.ARRIBA);
                    }
                    if(theLeaf.getUserData().equals("izquierda")){
                        //mundo.rebotar(Mundo3D.IZQUIERDA);
                        mundo.cambiarScore(2);
                    }
                    if(theLeaf.getUserData().equals("derecha")){
                        //mundo.rebotar(Mundo3D.DERECHA);
                        mundo.cambiarScore(1);
                    }
                    if(theLeaf.getUserData().equals("enfrente")){
                        mundo.rebotar(Mundo3D.ENFRENTE);
                    }
                    if(theLeaf.getUserData().equals("atras")){
                        mundo.rebotar(Mundo3D.ATRAS);
                    }
                    if(theLeaf.getUserData().equals("barra1")){
                        //mundo.cambiarScore(1);
                        mundo.rebotar(Mundo3D.DERECHA);
                    }
                    if(theLeaf.getUserData().equals("barra2")){
                        //mundo.cambiarScore(2);
                        mundo.rebotar(Mundo3D.IZQUIERDA);
                    }
                }
                //mundo.regresarJugador();
               //if (theLeaf.getUserData() == null){
               //   mundo.regresarJugador();
               //    System.out.println("Igual");
               // }
            }
            
        
        //condicion = new WakeupOnCollisionEntry(mundo.getLimites());
        wakeupOn(condicion);
    }
}
