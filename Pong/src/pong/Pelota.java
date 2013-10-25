/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package pong;

import com.sun.j3d.utils.geometry.Primitive;
import com.sun.j3d.utils.geometry.Sphere;
import com.sun.j3d.utils.image.TextureLoader;
import javax.media.j3d.Alpha;
import javax.media.j3d.Appearance;
import javax.media.j3d.BoundingSphere;
import javax.media.j3d.ImageComponent2D;
import javax.media.j3d.Node;
import javax.media.j3d.PolygonAttributes;
import javax.media.j3d.RotationInterpolator;
import javax.media.j3d.Shape3D;
import javax.media.j3d.Texture;
import javax.media.j3d.Texture2D;
import javax.media.j3d.Transform3D;
import javax.media.j3d.TransformGroup;
import javax.vecmath.Point3d;
import javax.vecmath.Vector3d;
import javax.vecmath.Vector3f;



/**
 *
 * @author ISAAC
 */
public class Pelota {
    private TransformGroup tg;
    private Sphere sphere;
    private Vector3d posicion;

    public Pelota(Vector3d coordenadas, float radio, int rotacion,String nombreTextura){
        posicion = coordenadas;
        tg = new TransformGroup();
        tg.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);
        //Cargar la textura
        Appearance appear = getTextura(nombreTextura);
        // Crear la esfera
        sphere = new Sphere(radio,Primitive.GENERATE_TEXTURE_COORDS,appear);
        sphere.getShape().setCapability(Shape3D.ALLOW_APPEARANCE_WRITE);
        //Agregar rotacion, traslación y cambio de coordenadas

        /*Transform3D transform = new Transform3D();//Se crea un objeto Transform
        transform.setTranslation(coordenadas);//Se hace la traslación del Transform
        tg.setTransform(transform);//Se le aplica el transform al TG*/
        //tg.addChild(sphere);//Se le agrega la esfera al TG ya transformado
        
            /*//Moverlo de lugar
            Transform3D transform3D = new Transform3D();
            transform3D.setTranslation(coordenadas);
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.setTransform(transform3D);
            transformGroup.addChild(sphere);
            //transformGroup.addChild(xformGroup);


            TransformGroup xformGroup = new TransformGroup();
            xformGroup.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);

            //Create an interpolator for rotating the node.
            RotationInterpolator interpolator = new RotationInterpolator(new Alpha(-1,rotacion),xformGroup);

            //Establish the animation region for this interpolator.
            interpolator.setSchedulingBounds(new BoundingSphere(new Point3d(0.0,0.0,0.0),1.0));

            //Populate the xform group.
            xformGroup.addChild(interpolator);
            xformGroup.addChild(transformGroup);
            //xformGroup.addChild(sphere);

            


            //tg.addChild(transformGroup);
            tg.addChild(xformGroup);*/

            //Rotar el planeta
            TransformGroup xformGroup = new TransformGroup();
            xformGroup.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);
            //Create an interpolator for rotating the node.
            RotationInterpolator interpolator = new RotationInterpolator(new Alpha(-1,rotacion),xformGroup);
            //Establish the animation region for this interpolator.
            interpolator.setSchedulingBounds(new BoundingSphere(new Point3d(0.0,0.0,0.0),100.0));
            //Populate the xform group.
            xformGroup.addChild(interpolator);
            xformGroup.addChild(sphere);

            //Moverlo a su posicion de acuerdo al vector
            Transform3D transform3D = new Transform3D();
            transform3D.setTranslation(coordenadas);
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.setTransform(transform3D);
            transformGroup.addChild(xformGroup);

            tg.addChild(transformGroup);
    }

    public Appearance getTextura(String nombreTextura){
        TextureLoader loader = new TextureLoader(nombreTextura,null);
        ImageComponent2D image = loader.getImage();
        Texture2D texture = new Texture2D(Texture.BASE_LEVEL, Texture.RGBA, image.getWidth(), image.getHeight());
        texture.setImage(0, image);
        Appearance appear = new Appearance();
        appear.setTexture(texture);
        PolygonAttributes pa = new PolygonAttributes();
        pa.setCullFace(PolygonAttributes.CULL_NONE);//Para que se vean todas las caras
        pa.setBackFaceNormalFlip(true);
        appear.setPolygonAttributes(pa);
        return appear;
    }

    public void setTextura(String nombreTextura){
        TextureLoader loader = new TextureLoader(nombreTextura,null);
        ImageComponent2D image = loader.getImage();
        Texture2D texture = new Texture2D(Texture.BASE_LEVEL, Texture.RGBA, image.getWidth(), image.getHeight());
        texture.setImage(0, image);
        Appearance appear = new Appearance();
        appear.setTexture(texture);
        PolygonAttributes pa = new PolygonAttributes();
        pa.setCullFace(PolygonAttributes.CULL_NONE);//Para que se vean todas las caras
        pa.setBackFaceNormalFlip(true);
        appear.setPolygonAttributes(pa);
        sphere.setAppearance(appear);
    }


    public TransformGroup getTG(){
        return tg;
    }

    public Vector3d getPosicion(){
        return posicion;
    }

    public void setPosicion(Vector3d pos){
        posicion = pos;
        Transform3D tpos = new Transform3D();
        tpos.set(posicion);
        tg.setTransform(tpos);
    }

    public Sphere getShape(){
        return sphere;
    }


}
