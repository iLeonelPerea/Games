/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package pong;
import javax.media.j3d.*;
import java.util.Enumeration;
/**
 *
 * @author ISAAC
 */
public class TimeBehavior extends Behavior{
    private WakeupCondition tiempo;
    private int lapso;
    private Mundo3D padre;

    public TimeBehavior(Mundo3D pa, int time){
        tiempo = new WakeupOnElapsedTime(time);
        padre = pa;
    }

    public void initialize(){
        wakeupOn(tiempo);
    }

    public void processStimulus(Enumeration criteria){
        padre.moverPelota();
        wakeupOn(tiempo);
    }
}
