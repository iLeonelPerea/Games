/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package pong;

import com.sun.j3d.utils.geometry.Box;
import com.sun.j3d.utils.image.TextureLoader;
import javax.media.j3d.Alpha;
import javax.media.j3d.Appearance;
import javax.media.j3d.BoundingSphere;
import javax.media.j3d.GeometryArray;
import javax.media.j3d.ImageComponent2D;
import javax.media.j3d.IndexedLineArray;
import javax.media.j3d.PolygonAttributes;
import javax.media.j3d.PositionInterpolator;
import javax.media.j3d.QuadArray;
import javax.media.j3d.Shape3D;
import javax.media.j3d.Texture;
import javax.media.j3d.Texture2D;
import javax.media.j3d.TextureAttributes;
import javax.media.j3d.Transform3D;
import javax.media.j3d.TransformGroup;
import javax.media.j3d.TransparencyAttributes;
import javax.vecmath.Point2f;
import javax.vecmath.Point3f;
import javax.vecmath.TexCoord2f;
import javax.vecmath.Vector3f;

/**
 *
 * @author osaenz
 */


public class Barra extends Shape3D{
    private TransformGroup tg = new TransformGroup();
    private Vector3f pos;
    private float ancho, alto, profundo;
    private Box barra;
    public static final int X=0;
    public static final int Y=2;
    public static final int Z=3;

    public Barra(float ancho, float alto, float profundo, Vector3f pos,int eje,BoundingSphere limites, 
            String nombreTextura, String descripcion){
        /*
         * Textura
         */
        Appearance appear = getTextura(nombreTextura);
        this.ancho = ancho;
        this.alto = alto;
        this.profundo = profundo;
        barra = new Box(ancho,alto,profundo,Box.GENERATE_TEXTURE_COORDS,appear);
        barra.getShape(Box.FRONT).setCapability(Shape3D.ALLOW_APPEARANCE_WRITE);
        barra.getShape(Box.BACK).setCapability(Shape3D.ALLOW_APPEARANCE_WRITE);
        barra.getShape(Box.BOTTOM).setCapability(Shape3D.ALLOW_APPEARANCE_WRITE);
        barra.getShape(Box.RIGHT).setCapability(Shape3D.ALLOW_APPEARANCE_WRITE);
        barra.getShape(Box.LEFT).setCapability(Shape3D.ALLOW_APPEARANCE_WRITE);
        barra.getShape(Box.TOP).setCapability(Shape3D.ALLOW_APPEARANCE_WRITE);
        //barra.getShape(Box.FRONT).setAppearance(appear);

        tg.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);
        Transform3D t3d = new Transform3D();
        switch (eje){
            case 1:
                t3d.rotX(Math.PI/2);
            break;
            case 2:
                t3d.rotZ(Math.PI/2);
            break;
            case 3:
                t3d.rotY(Math.PI/2);
            break;
        }
        //Posicion de la barra
        this.pos = pos;
        Transform3D posicion = new Transform3D();
        posicion.set(this.pos);
        TransformGroup tg2 = new TransformGroup();
        tg2.addChild(barra);
        tg2.setTransform(t3d);
        //Rotacion de la barra
        tg.setTransform(posicion);
        tg.addChild(tg2);
        //this.setUserData(descripcion);
        //tg.setUserData(descripcion);
        //barra.setUserData(descripcion);
        barra.getShape(Box.FRONT).setUserData(descripcion);
        barra.getShape(Box.BACK).setUserData(descripcion);
        barra.getShape(Box.LEFT).setUserData(descripcion);
        barra.getShape(Box.RIGHT).setUserData(descripcion);
        barra.getShape(Box.TOP).setUserData(descripcion);
        barra.getShape(Box.BOTTOM).setUserData(descripcion);
    }

    public TransformGroup getTG(){
        return tg;
    }

    public Appearance getTextura(String nombreTextura){
        TransparencyAttributes ta = new TransparencyAttributes();
        ta.setTransparencyMode(TransparencyAttributes.BLENDED);
        ta.setCapability(TransparencyAttributes.ALLOW_VALUE_WRITE);
        ta.setTransparency(0.3f);
        Appearance app = new Appearance();
        Texture t = new TextureLoader(nombreTextura,null).getTexture();
        app.setTexture(t);
        TextureAttributes texAttr = new TextureAttributes();
        texAttr.setTextureMode(TextureAttributes.MODULATE);
        app.setTextureAttributes(texAttr);
        app.setTransparencyAttributes(ta);
        return app;
    }

    public void setTextura(String nombreTextura){
        TransparencyAttributes ta = new TransparencyAttributes();
        ta.setTransparencyMode(TransparencyAttributes.BLENDED);
        ta.setCapability(TransparencyAttributes.ALLOW_VALUE_WRITE);
        ta.setTransparency(0.5f);
        Appearance app = new Appearance();
        Texture t = new TextureLoader(nombreTextura,null).getTexture();
        app.setTexture(t);
        TextureAttributes texAttr = new TextureAttributes();
        texAttr.setTextureMode(TextureAttributes.MODULATE);
        app.setTextureAttributes(texAttr);
        //app.setTransparencyAttributes(ta);
        
        barra.getShape(Box.FRONT).setAppearance(app);
        barra.getShape(Box.BACK).setAppearance(app);
        barra.getShape(Box.TOP).setAppearance(app);
        barra.getShape(Box.BOTTOM).setAppearance(app);
        barra.getShape(Box.RIGHT).setAppearance(app);
        barra.getShape(Box.LEFT).setAppearance(app);
    }

    public Vector3f getPosicion(){
        return pos;
    }

    public void setPosicion(Vector3f pos){
        this.pos = pos;
        Transform3D tpos = new Transform3D();
        tpos.set(pos);
        tg.setTransform(tpos);
    }
}
