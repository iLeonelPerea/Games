/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package pong;

import javax.media.j3d.*;
import com.sun.j3d.utils.geometry.*;
import java.awt.Font;
import javax.vecmath.*;

/**
 *
 * @author ISAAC
 */
public class Ejes {
    private TransformGroup tg;
    private BoundingSphere limites;
    
    public Ejes(int largo){
        Shape3D figura = new Shape3D();
        figura.removeAllGeometries();
        LineArray la = new LineArray(6,LineArray.COORDINATES | LineArray.COLOR_3);

        la.setCoordinate(0,new Point3d(-(float)largo/2,0,0));
        la.setCoordinate(1,new Point3d((float)largo/2,0,0));
        la.setCoordinate(2,new Point3d(0,-(float)largo/2,0));
        la.setCoordinate(3,new Point3d(0,(float)largo/2,0));
        la.setCoordinate(4,new Point3d(0,0,-(float)largo/2));
        la.setCoordinate(5,new Point3d(0,0,(float)largo/2));
        //Colorear los ejes
        Color3f blanco = new Color3f(1,1,1);
        Color3f rojo  =  new Color3f(1,0,0);
        Color3f azul =  new Color3f(0,0,1);
        Color3f verde = new Color3f(0,1,0);
        //Agregar colores
        la.setColor(0,blanco);
        la.setColor(1,rojo);
        la.setColor(2,blanco);
        la.setColor(3,verde);
        la.setColor(4,blanco);
        la.setColor(5,azul);

        //Agregar el linearray a la figura (shape)
        figura.addGeometry(la);
        
        tg = new TransformGroup();
        tg.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);
        tg.addChild(figura);


        //Agregar Texto2D con Billboard
        agregarTexto("x",new Vector3d(10,0,0));
        agregarTexto("-x",new Vector3d(-10,0,0));
        agregarTexto("y",new Vector3d(0,10,0));
        agregarTexto("-y",new Vector3d(0,-10,0));
        agregarTexto("z",new Vector3d(0,0,10));
        agregarTexto("-z",new Vector3d(0,0,-10));
    }

    private void agregarTexto (String texto, Vector3d coordenadas){
        //Crear el texto
        Text2D texto2d = new Text2D(texto, new Color3f(0f,0f,1f),"Arial",10,Font.BOLD);
        texto2d.setRectangleScaleFactor(.1f);//Cambiar la escala con la que va a poner la fuente

        //Crear los TG para el billboard y para el texto
        TransformGroup TGT = new TransformGroup();
        TransformGroup TGR = new TransformGroup();

        //Para mover el texto a las coordenadas dadas
        Transform3D transform = new Transform3D();
        transform.setTranslation(coordenadas);
        TGT.setTransform(transform);

        //Crear el Billboard
        TGR.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);
        Billboard billboard = new Billboard(TGR);
        BoundingSphere lim = new BoundingSphere();//Todo lo que este dentro de la esfera se maneja
        billboard.setSchedulingBounds(lim);
        billboard.setAlignmentMode(Billboard.ROTATE_ABOUT_POINT);

        //Agregar cada elemento a su TG
        tg.addChild(TGT);
        tg.addChild(billboard);
        TGT.addChild(TGR);
        TGR.addChild(texto2d);
    }

    public TransformGroup getTG(){
        return tg;
    }
}

