/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/*
 * Mundo3D.java
 *
 * Created on 23/04/2010, 01:11:44 PM
 */

package pong;

import com.sun.j3d.utils.behaviors.vp.OrbitBehavior;
import com.sun.j3d.utils.geometry.Box;
import com.sun.j3d.utils.geometry.Primitive;
import com.sun.j3d.utils.geometry.Sphere;
import com.sun.j3d.utils.image.TextureLoader;
import com.sun.j3d.utils.universe.SimpleUniverse;
import com.sun.j3d.utils.universe.ViewingPlatform;
import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.GraphicsConfiguration;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.media.j3d.Appearance;
import javax.media.j3d.Background;
import javax.media.j3d.BoundingSphere;
import javax.media.j3d.BranchGroup;
import javax.media.j3d.Canvas3D;
import javax.media.j3d.Font3D;
import javax.media.j3d.FontExtrusion;
import javax.media.j3d.GeometryArray;
import javax.media.j3d.ImageComponent2D;
import javax.media.j3d.PolygonAttributes;
import javax.media.j3d.QuadArray;
import javax.media.j3d.Shape3D;
import javax.media.j3d.Text3D;
import javax.media.j3d.Texture;
import javax.media.j3d.Texture2D;
import javax.media.j3d.TextureAttributes;
import javax.media.j3d.Transform3D;
import javax.media.j3d.TransformGroup;
import javax.media.j3d.TransparencyAttributes;
import javax.media.j3d.View;
import javax.swing.Icon;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.vecmath.Point2f;
import javax.vecmath.Point3d;
import javax.vecmath.Point3f;
import javax.vecmath.Vector3d;
import javax.vecmath.Vector3f;

/**
 *
 * @author ISAAC
 */
public class Mundo3D extends javax.swing.JFrame implements Runnable{

    private SimpleUniverse su;
    private GraphicsConfiguration gc;
    private Canvas3D lienzo;
    private BoundingSphere limites;
    private BranchGroup bg,bgFin;
    private TransformGroup tg, tgB1, tgB2, tgFin;
    private Barra b1, b2;
    //private TexPlane b1,b2;
    private Pelota bola;
    private boolean regresoX,regresoY,regresoZ;
    public boolean pausado;
    public static final int menosX = 1,masX=2,menosY=3,masY=4,menosZ=5,masZ=6;
    public static final int ABAJO = 7,ARRIBA=8,IZQUIERDA=9,DERECHA=10, ENFRENTE = 11, ATRAS = 12;
    public final int HOMBREvsHOMBRE = 13;
    public final int HOMBREvsCOMPUTADORA = 14;
    private final static double DY = 15, tamanoEscena = 50;
    private int score1, score2, scoreMax = 5;
    private double velocidadX = 0.3, velocidadY = 0.2, velocidadZ = 0.0;
    private ConfigurarBalon configurarBalon;
    private ConfigurarBarras configurarBarras;
    private ConfigurarNuevoJuego configurarJuego;
    private About about;
    private String balonSeleccionado = "Adidas";
    private String jugador1;
    private String jugador2;
    private MP3 musica;
    private SoundClip rebote, gol, fin;
    private Icon iconoLider, iconoPerdedor;

    /** Creates new form Mundo3D */
    public Mundo3D() {
        super("Pong 3D - A00366045 - ITESM Campus Zacatecas");
        initComponents();
        
        gc = SimpleUniverse.getPreferredConfiguration();
	lienzo = new Canvas3D(gc);
	su = new SimpleUniverse(lienzo);

        bgFin = new BranchGroup();
        bgFin.setCapability(BranchGroup.ALLOW_DETACH);

        tgFin = new TransformGroup();
        tgFin.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);

        su.addBranchGraph(bgFin);
        
        setVisible(true);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        this.setVisible(true);
        configurarBalon = new ConfigurarBalon(this);
        configurarBarras = new ConfigurarBarras(this);
        configurarJuego = new ConfigurarNuevoJuego(this);
        about = new About();
        score1=0;
        score2=0;
        /*
         * Música
         */
        //Musica en MP3
        String camino = "file:";
        camino += System.getProperty("user.dir")+"\\musica.mp3";
        musica = new MP3(camino);
        musica.start();

        //Sonidos en wav
        rebote = new SoundClip("explosion.wav");
        gol = new SoundClip("laser.wav");
        fin = new SoundClip("pause.wav");

        //Deshabilitar menus
        jMenuItem4.setEnabled(false);
        jMenuItem5.setEnabled(false);
        
        this.setSize(new Dimension(1024,768));
    }

    public void pausa(int ms){
        try {
            Thread.sleep(ms);
        } catch (InterruptedException ex) {
            Logger.getLogger(Mundo3D.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public void crearMundo(int goles){
        //Habilitar cambios de balon y de barras
        jMenuItem4.setEnabled(true);
        jMenuItem5.setEnabled(true);
        pausado=true;
        scoreMax = goles;
        lblScoreMax.setText(""+scoreMax);
        su.getLocale().removeBranchGraph(bgFin);

	limites = new BoundingSphere(new Point3d(0.0f,0.0f,0.0f),100.0f);//Todo lo que este dentro de la esfera se maneja
	tg = new TransformGroup();
	bg = new BranchGroup();
        //Permitir cambios en el run-time
        bg.setCapability(BranchGroup.ALLOW_CHILDREN_READ);
        bg.setCapability(BranchGroup.ALLOW_CHILDREN_WRITE);
        bg.setCapability(BranchGroup.ALLOW_CHILDREN_EXTEND);
        bg.setCapability(BranchGroup.ALLOW_DETACH);

        //Fondo
        agregarBackground("background.jpg");
        //Caja de la escena
        addSceneBox(tamanoEscena);
        //Barras, balon y piso
        b1 = new Barra(4,2,0.5f,new Vector3f((int)(-tamanoEscena/2) + 10,5,0),Barra.Z,limites,"images/barraEstadio.jpg","barra2");
        b2 = new Barra(4,2,0.5f,new Vector3f((int)(tamanoEscena/2) - 10,5,0),Barra.Z,limites,"images/barraRed.jpg","barra1");
        tgB1 = b1.getTG();
        tgB2 = b2.getTG();
        bola = new Pelota(new Vector3d(0,0,0),1f,2000,"images/balon"+balonSeleccionado+".jpg");
        tg.addChild(bola.getTG());

        //tg.addChild(new Ejes(20).getTG());
        tg.addChild(tgB1);
        tg.addChild(tgB2);
        //agregarPiso(20,20,new Vector3d(0,-5,0),"images/ice.png");
        
        //Behaviors
        BehaviorTeclado bt = new BehaviorTeclado(this);
        bt.setSchedulingBounds(limites);
        tg.addChild(bt);

        TimeBehavior tb = new TimeBehavior(this,20);
        tb.setSchedulingBounds(limites);
        tg.addChild(tb);

        BehaviorColision bc = new BehaviorColision(this,bola.getShape().getShape());
        bc.setSchedulingBounds(limites);
        tg.addChild(bc);

        //Agregar el TG al BG y compilarlo para mejorar el render
        bg.addChild(tg);
        bg.compile();
        //Agregar el BG al Universo
        su.addBranchGraph(bg);
        //Le pides al universo que le des el viewingPlatform y le pides que estes a dos metros de ahi
        su.getViewingPlatform().setNominalViewingTransform();
        lienzo.setFocusable(true);
        lienzo.requestFocus();
        establecerPosicionInicial();
        agregarOrbitar();
        pnlJuego.add(lienzo,BorderLayout.CENTER);
        pausa(200);
    }

    public void run() {
    }

    private void establecerPosicionInicial(){
        ViewingPlatform vp = su.getViewingPlatform();
        View v = new View();
        //v.setFrontClipDistance(30.0);
        TransformGroup visionTG = vp.getViewPlatformTransform();// De la vista dame el ViewPlatform

        Transform3D t3d = new Transform3D();
        visionTG.getTransform(t3d);
        t3d.lookAt(new Point3d(0,-5,60), new Point3d(0,0,0), new Vector3d(0,1,0));//lookat (donde estas, a donde quieres ver, eje hacia arriba);
        t3d.invert();//Se requiere para que la posicion sea relativa al visor y no al objeto
        //su.getViewer().getView().setFrontClipDistance( View.PHYSICAL_EYE );
        //su.getViewer().getView().setFrontClipPolicy( View.VIRTUAL_EYE );
        //su.getViewer().getView().setBackClipPolicy( View.VIRTUAL_EYE );
        //su.getViewer().getView().setFrontClipDistance(1.0);
        su.getViewer().getView().setBackClipDistance(50.0);
        visionTG.setTransform(t3d);

    }

    private void agregarOrbitar(){
       OrbitBehavior orbit = new OrbitBehavior(lienzo, OrbitBehavior.REVERSE_ALL);
       orbit.setSchedulingBounds(limites);
       ViewingPlatform vp = su.getViewingPlatform();
       vp.setViewPlatformBehavior(orbit);
    }

    public BoundingSphere getLimites(){
        return limites;
    }

    public void moverBarraP2(int direccion){
        float velocidad=1.5f;
        if(!pausado){
            switch (direccion){
                //-z
                case menosZ:
                    if(b1.getPosicion().z>-tamanoEscena/2){
                        b1.setPosicion(new Vector3f(b1.getPosicion().x,b1.getPosicion().y,b1.getPosicion().z-velocidad-1));
                    }
                break;
                //+z
                case masZ:
                    if(b1.getPosicion().z<tamanoEscena/2){
                        b1.setPosicion(new Vector3f(b1.getPosicion().x,b1.getPosicion().y,b1.getPosicion().z+velocidad+1));
                    }
                break;
                //+x
                case masX:
                    if(b1.getPosicion().x>-tamanoEscena/2){
                        b1.setPosicion(new Vector3f(b1.getPosicion().x+velocidad+1,b1.getPosicion().y,b1.getPosicion().z));
                    }
                break;
                //-x
                case menosX:
                    if(b1.getPosicion().x<tamanoEscena/2){
                    b1.setPosicion(new Vector3f(b1.getPosicion().x-velocidad-1,b1.getPosicion().y,b1.getPosicion().z));
                    }
                break;
                //+y
                case masY:
                    if(b1.getPosicion().y < tamanoEscena/4){
                        b1.setPosicion(new Vector3f(b1.getPosicion().x,b1.getPosicion().y+velocidad+1,b1.getPosicion().z));
                    }
                break;
                //-y
                case menosY:
                    if(b1.getPosicion().y>-DY){
                        b1.setPosicion(new Vector3f(b1.getPosicion().x,b1.getPosicion().y-velocidad-1,b1.getPosicion().z));
                    }
                break;
            }
        }
    }

    public void moverBarraP1(int direccion){
        float velocidad=1.5f;
        if(!pausado){
            switch (direccion){
                //-z
                case menosZ:
                    if(b2.getPosicion().z>-tamanoEscena/2){
                        b2.setPosicion(new Vector3f(b2.getPosicion().x,b2.getPosicion().y,b2.getPosicion().z-velocidad-1));
                    }
                break;
                //+z
                case masZ:
                    if(b2.getPosicion().z<tamanoEscena/2){
                        b2.setPosicion(new Vector3f(b2.getPosicion().x,b2.getPosicion().y,b2.getPosicion().z+velocidad+1));
                    }
                break;
                //+x
                case masX:
                    if(b2.getPosicion().x>-tamanoEscena/2){
                        b2.setPosicion(new Vector3f(b2.getPosicion().x+velocidad+1,b2.getPosicion().y,b2.getPosicion().z));
                    }
                break;
                //-x
                case menosX:
                    if(b2.getPosicion().x<tamanoEscena/2){
                    b2.setPosicion(new Vector3f(b2.getPosicion().x-velocidad-1,b2.getPosicion().y,b2.getPosicion().z));
                    }
                break;
                //+y
                case masY:
                    if(b2.getPosicion().y < tamanoEscena/4){
                        b2.setPosicion(new Vector3f(b2.getPosicion().x,b2.getPosicion().y+velocidad+1,b2.getPosicion().z));
                    }
                break;
                //-y
                case menosY:
                    if(b2.getPosicion().y>-DY){
                        b2.setPosicion(new Vector3f(b2.getPosicion().x,b2.getPosicion().y-velocidad-1,b2.getPosicion().z));
                    }
                break;
            }
        }
    }

    void moverPelota() {
        if(!pausado){
            if(score1>=scoreMax){
                finJuego(1);
            }
            if(score2>=scoreMax){
                finJuego(2);
            }
            Vector3d posicion = bola.getPosicion();
            //Mover la esfera
            if(regresoX && regresoY && regresoZ){
                posicion.x-=velocidadX;
                posicion.y-=velocidadY;
                posicion.z-=velocidadZ;
            }
            else if(regresoX && regresoY && !regresoZ){
                posicion.x-=velocidadX;
                posicion.y-=velocidadY;
                posicion.z+=velocidadZ;
            }
            else if(regresoX && !regresoY && regresoZ){
                posicion.x-=velocidadX;
                posicion.y+=velocidadY;
                posicion.z-=velocidadZ;
            }
            else if(!regresoX && regresoY && regresoZ){
                posicion.x+=velocidadX;
                posicion.y-=velocidadY;
                posicion.z-=velocidadZ;
            }
            else if(regresoX && !regresoY && !regresoZ){
                posicion.x-=velocidadX;
                posicion.y+=velocidadY;
                posicion.z+=velocidadZ;
            }
            else if(!regresoX && regresoY && !regresoZ){
                posicion.x+=velocidadX;
                posicion.y-=velocidadY;
                posicion.z+=velocidadZ;
            }
            else if(!regresoX && !regresoY && regresoZ){
                posicion.x+=velocidadX;
                posicion.y+=velocidadY;
                posicion.z-=velocidadZ;
            }
            else if(!regresoX && !regresoY && !regresoZ){
                posicion.x+=velocidadX;
                posicion.y+=velocidadY;
                posicion.z+=velocidadZ;
            }

            /*
             * Por si se sale al detectar doble colision
             */
            if(posicion.x > tamanoEscena/2){
                regresoX = true;
            }
            if(posicion.x < -tamanoEscena/2){
                regresoX=false;
            }
            if(posicion.y > tamanoEscena/4){
                regresoY=true;
            }
            if(posicion.y < -DY){
                regresoY=false;
            }
            if(posicion.z > tamanoEscena/2){
                regresoZ=true;
            }
            if(posicion.z < -tamanoEscena/2){
                regresoZ=false;
            }
            bola.setPosicion(posicion);
        }
    }

    public void rebotar(int a){
        double deltaVel = 0.05;
        switch(a){
            case ARRIBA:
                regresoY = true;
                velocidadY+=deltaVel;
                break;
            case ABAJO:
                regresoY = false;
                velocidadY+=deltaVel;
                break;
            case IZQUIERDA:
                regresoX = false;
                velocidadX+=deltaVel;
                break;
            case DERECHA:
                regresoX = true;
                velocidadX+=deltaVel;
                break;
            case ENFRENTE:
                regresoZ = false;
                velocidadZ+=deltaVel;
                break;
            case ATRAS:
                regresoZ = true;
                velocidadZ+=deltaVel;
                break;
        }
        rebote.play();
    }

    public void cambiarScore(int numeroJugador){
        switch(numeroJugador){
            case 1:
                score1++;
                lblScore1.setText(""+score1);
//                b1.setDescripcion("barra1");
                break;
            case 2:
                score2++;
                lblScore2.setText(""+score2);
  //              b2.setDescripcion("barra2");
                break;
        }
        bola.setPosicion(new Vector3d(0,0,0));
        velocidadX = 0.3;
        velocidadY = 0.2;
        velocidadZ = 0.0;
        //Reproducir Gol
        gol.play();
        //Cambiar iconos
        if(score1>score2){
            lblPlayer1.setIcon(new ImageIcon(this.getClass().getResource("/pong/images/rating_star.png")));
            lblPlayer2.setIcon(new ImageIcon(this.getClass().getResource("/pong/images/rating_star_blank.png")));
        }
        else if (score2>score1){
            lblPlayer2.setIcon(new ImageIcon(this.getClass().getResource("/pong/images/rating_star.png")));
            lblPlayer1.setIcon(new ImageIcon(this.getClass().getResource("/pong/images/rating_star_blank.png")));
        }
        else{
            lblPlayer1.setIcon(new ImageIcon(this.getClass().getResource("/pong/images/rating_star_blank.png")));
            lblPlayer2.setIcon(new ImageIcon(this.getClass().getResource("/pong/images/rating_star_blank.png")));
        }
    }

    public void finJuego(int a){
        try{
            /*
             * Reiniciar parametros
             */
            velocidadX = 0.3;
            velocidadY = 0.2;
            velocidadZ = 0.0;
            score1 = 0;
            score2 = 0;
            /*
             * Remover BG del juego
             */
            su.getLocale().removeBranchGraph(bg);
            bgFin = new BranchGroup();
            tgFin = new TransformGroup();

            bgFin.setCapability(BranchGroup.ALLOW_CHILDREN_READ);
            bgFin.setCapability(BranchGroup.ALLOW_CHILDREN_WRITE);
            bgFin.setCapability(BranchGroup.ALLOW_CHILDREN_EXTEND);
            bgFin.setCapability(BranchGroup.ALLOW_DETACH);
            tgFin.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);

            tgFin.removeAllChildren();
            /*
             * Agregar Ganador
             */
            switch(a){
                case 1:
                    tgFin.addChild(agregarTexto3D("Ganador: "+configurarJuego.nombre1));
                    break;
                case 2:
                    tgFin.addChild(agregarTexto3D("Ganador: "+configurarJuego.nombre2));
                    break;
            }
            lblPlayer1.setText("Jugador 1: ");
            lblPlayer2.setText("Jugador 2: ");
            lblScore1.setText("");
            lblScore2.setText("");
            bgFin.addChild(tgFin);
            //bg.compile();
            su.addBranchGraph(bgFin);
            /*
             * Habilitar la opcion de juego nuevo
             * Deshabilitar opciones de balones y barras
             */
            jMenuItem4.setEnabled(false);
            jMenuItem5.setEnabled(false);
            jMenuItem1.setEnabled(true);
        }
        catch(java.lang.NumberFormatException ex){
            
        }
    }

    private Shape3D agregarTexto3D(String texto){
        Font3D fuente = new Font3D(new Font("Helvetica", Font.PLAIN,1), new FontExtrusion());//Font Extrusion es para darle volumen
        Text3D texto3d = new Text3D (fuente, texto, new Point3f(0,0,0));
        /*
         * Apariencia
         */
        TransparencyAttributes ta = new TransparencyAttributes();
        ta.setTransparencyMode(TransparencyAttributes.BLENDED);
        ta.setCapability(TransparencyAttributes.ALLOW_VALUE_WRITE);
        ta.setTransparency(0.4f);
        Appearance app = new Appearance();
        Texture t = new TextureLoader("images/floor1.jpg",null).getTexture();
        app.setTexture(t);
        TextureAttributes texAttr = new TextureAttributes();
        texAttr.setTextureMode(TextureAttributes.MODULATE);
        app.setTextureAttributes(texAttr);
        app.setTransparencyAttributes(ta);
        /*
         * Regresar texto
         */
        Shape3D txtShape = new Shape3D(texto3d,app);
        return txtShape;
    }

    private void agregarPiso(float ancho, float alto, Vector3d pos, String textura){
         QuadArray q = new QuadArray(4, GeometryArray.COORDINATES|GeometryArray.TEXTURE_COORDINATE_2);
         Point3f[] puntos = {
             new Point3f(-ancho/2,0,-alto/2),
             new Point3f(-ancho/2,0,alto/2),
             new Point3f(ancho/2,0,alto/2),
             new Point3f(ancho/2,0,-alto/2)
         };

         q.setCoordinates(0, puntos);
         Shape3D figura = new Shape3D();

         //Apariencia
         TextureLoader loader = new TextureLoader(textura,this);
         ImageComponent2D image = loader.getImage();

         Texture2D texture = new Texture2D(Texture.BASE_LEVEL, Texture.RGBA, image.getWidth(), image.getHeight());
         texture.setImage(0, image);
         Appearance appear = new Appearance();
         appear.setTexture(texture);
         PolygonAttributes pa = new PolygonAttributes();
         pa.setCullFace(PolygonAttributes.CULL_NONE);//Para que se vean todas las caras
         pa.setBackFaceNormalFlip(true);
         appear.setPolygonAttributes(pa);
         //Textura
         q.setTextureCoordinate(0,new Point2f(0,1));
         q.setTextureCoordinate(1,new Point2f(0,0));
         q.setTextureCoordinate(2,new Point2f(1,0));
         q.setTextureCoordinate(3,new Point2f(1,1));

         figura.addGeometry(q);
         figura.setAppearance(appear);

         //Posicion
         TransformGroup tg2 = new TransformGroup();
         tg2.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);
         Transform3D t3d2 = new Transform3D();
         t3d2.set(pos);
         tg2.setTransform(t3d2);
         tg2.addChild(figura);
         tg.addChild(tg2);
     }

    private void agregarBackground(String fnm)
  /* Create a spherical background. The texture for the
     sphere comes from fnm. */
  {
    Texture2D tex = loadTexture("images/" + fnm);
    Sphere sphere = new Sphere(1.0f,Sphere.GENERATE_NORMALS_INWARD |Sphere.GENERATE_TEXTURE_COORDS, 8);   // default = 15 (4, 8)
    Appearance backApp = sphere.getAppearance();
    backApp.setTexture( tex );
    BranchGroup backBG = new BranchGroup();
    backBG.addChild(sphere);

    Background bg2 = new Background();
    bg2.setApplicationBounds(limites);
    bg2.setGeometry(backBG);

    bg.addChild(bg2);
  }  // end of addBackground()

    private Texture2D loadTexture(String fn)
  // load image from file fn as a texture
  {
    TextureLoader texLoader = new TextureLoader(fn, null);
    Texture2D texture = (Texture2D) texLoader.getTexture();
    if (texture == null){
        System.out.println("Cannot load texture from " + fn);
    }
    else {
        System.out.println("Loaded texture from " + fn);
        texture.setEnable(true);
    }
    return texture;
  }  // end of loadTexture()


    private void addSkyBox(String fnm)
  {
    com.sun.j3d.utils.geometry.Box texCube =
         new Box(1.0f, 1.0f, 1.0f, Primitive.GENERATE_TEXTURE_COORDS,new Appearance());

    Texture2D tex = loadTexture("images" + fnm);

    setFaceTexture(texCube, com.sun.j3d.utils.geometry.Box.FRONT, tex);
    setFaceTexture(texCube, com.sun.j3d.utils.geometry.Box.LEFT, tex);
    setFaceTexture(texCube, com.sun.j3d.utils.geometry.Box.RIGHT, tex);
    setFaceTexture(texCube, com.sun.j3d.utils.geometry.Box.BACK, tex);
    setFaceTexture(texCube, com.sun.j3d.utils.geometry.Box.TOP, tex);
    setFaceTexture(texCube, com.sun.j3d.utils.geometry.Box.BOTTOM, tex);

	BranchGroup backBG = new BranchGroup();
    backBG.addChild(texCube);

    Background bk = new Background();
    bk.setApplicationBounds(limites);
    bk.setGeometry(backBG);

    bg.addChild(bk);
  }  // end of addSkyBox()


  private void setFaceTexture(com.sun.j3d.utils.geometry.Box texCube,
                                   int faceID, Texture2D tex)
  {
    Appearance app = new Appearance();

    // make texture appear on back side of face
    PolygonAttributes pa = new PolygonAttributes();
    pa.setCullFace( PolygonAttributes.CULL_FRONT);
    app.setPolygonAttributes( pa );

    if (tex != null){
      app.setTexture(tex);
    }

    texCube.getShape(faceID).setAppearance(app);
  }  // end of setFaceTexture()


  // ------------------------ skybox version 2 ---------------------


  private void addSceneBox(double wallLen)
  /* applies different textures to the faces six quads forming a
     box around the scene, of dimensions wallLen
  */
  {
    // the eight corner points
    /* base starting from front/left then anti-clockwise, at a small
       offset below floor, DY */
    Point3d p1 = new Point3d(-wallLen/2, -DY, wallLen/2);
    Point3d p2 = new Point3d(wallLen/2, -DY, wallLen/2);
    Point3d p3 = new Point3d(wallLen/2, -DY, -wallLen/2);
    Point3d p4 = new Point3d(-wallLen/2, -DY, -wallLen/2);

    /* top starting from front/left then anti-clockwise, and at height
       wallLen/4 */
    Point3d p5 = new Point3d(-wallLen/2, wallLen/4, wallLen/2);
    Point3d p6 = new Point3d(wallLen/2, wallLen/4, wallLen/2);
    Point3d p7 = new Point3d(wallLen/2, wallLen/4, -wallLen/2);
    Point3d p8 = new Point3d(-wallLen/2, wallLen/4, -wallLen/2);

    /* the six textures were created using Terragen */
    // floor
    bg.addChild( new TexPlane(p2, p3, p4, p1, "images/floor.jpg","abajo"));

    // front wall
    bg.addChild( new TexPlane(p4, p3, p7, p8, "images/skyFront.jpg","enfrente"));

    // right wall
    bg.addChild( new TexPlane(p3, p2, p6, p7, "images/skyRight.jpg","derecha"));

    // back wall
    bg.addChild( new TexPlane(p2, p1, p5, p6, "images/skyBack.jpg","atras"));

    // left wall
    bg.addChild( new TexPlane(p1, p4, p8, p5, "images/skyLeft.jpg","izquierda"));

    // ceiling
    bg.addChild( new TexPlane(p5, p8, p7, p6, "images/skyAbove.jpg","arriba"));

  } // end of addSceneBox()

  public Pelota getPelota(){
      return bola;
  }

  public Barra getBarra1(){
      return b1;
  }
  public Barra getBarra2(){
      return b2;
  }

  public void recojerModelo(){
        /*Iniciamos los componentes del juego.*/
        iniciarJuego();
    }

    /*Método que inicia el juego una vez obtenido el modelo.*/
    public void iniciarJuego(){
        /*Creamos los jugadores según el tipo de juego.*/
        if ( configurarJuego.tipo_juego == HOMBREvsHOMBRE ){
            lblPlayer1.setText("Jugador 1: "+configurarJuego.nombre1);
            lblPlayer2.setText("Jugador 2: "+configurarJuego.nombre2);
            /*Mostramos su información, asignamos los nombres de jugador al panel.*/
        } else {
            
        }
        this.jMenuItem1.setEnabled(false);
    }

    

    /** This method is called from within the constructor to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        pnlJuego = new javax.swing.JPanel();
        pnlEstadisticas = new javax.swing.JPanel();
        lblPlayer1 = new javax.swing.JLabel();
        lblPlayer2 = new javax.swing.JLabel();
        lblScore1 = new javax.swing.JLabel();
        lblScore2 = new javax.swing.JLabel();
        jLabel1 = new javax.swing.JLabel();
        jSeparator1 = new javax.swing.JSeparator();
        jLabel2 = new javax.swing.JLabel();
        lblScoreMax = new javax.swing.JLabel();
        jSeparator2 = new javax.swing.JSeparator();
        jMenuBar1 = new javax.swing.JMenuBar();
        jMenu1 = new javax.swing.JMenu();
        jMenuItem1 = new javax.swing.JMenuItem();
        jMenuItem2 = new javax.swing.JMenuItem();
        jMenuItem3 = new javax.swing.JMenuItem();
        jMenu2 = new javax.swing.JMenu();
        jMenuItem4 = new javax.swing.JMenuItem();
        jMenuItem5 = new javax.swing.JMenuItem();
        jMenu3 = new javax.swing.JMenu();
        jMenuItem6 = new javax.swing.JMenuItem();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setTitle("Pong 3D");
        setCursor(new java.awt.Cursor(java.awt.Cursor.DEFAULT_CURSOR));

        pnlJuego.setBorder(javax.swing.BorderFactory.createBevelBorder(javax.swing.border.BevelBorder.RAISED));
        pnlJuego.setLayout(new java.awt.BorderLayout());

        pnlEstadisticas.setBorder(javax.swing.BorderFactory.createTitledBorder(""));

        lblPlayer1.setFont(new java.awt.Font("Tahoma", 0, 14)); // NOI18N
        lblPlayer1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        lblPlayer1.setText("Jugador 1");

        lblPlayer2.setFont(new java.awt.Font("Tahoma", 0, 14));
        lblPlayer2.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        lblPlayer2.setText("Jugador 2");

        lblScore1.setFont(new java.awt.Font("Tahoma", 3, 12)); // NOI18N
        lblScore1.setText(" ");

        lblScore2.setFont(new java.awt.Font("Tahoma", 3, 12)); // NOI18N
        lblScore2.setText(" ");

        jLabel1.setFont(new java.awt.Font("Tahoma", 1, 14)); // NOI18N
        jLabel1.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel1.setText("Puntuación del Partido");

        jSeparator1.setOrientation(javax.swing.SwingConstants.VERTICAL);

        jLabel2.setFont(new java.awt.Font("Tahoma", 1, 14)); // NOI18N
        jLabel2.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel2.setText("Goles máximos");

        lblScoreMax.setFont(new java.awt.Font("Tahoma", 3, 12)); // NOI18N
        lblScoreMax.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        lblScoreMax.setText(" ");

        jSeparator2.setOrientation(javax.swing.SwingConstants.VERTICAL);

        javax.swing.GroupLayout pnlEstadisticasLayout = new javax.swing.GroupLayout(pnlEstadisticas);
        pnlEstadisticas.setLayout(pnlEstadisticasLayout);
        pnlEstadisticasLayout.setHorizontalGroup(
            pnlEstadisticasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pnlEstadisticasLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(pnlEstadisticasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(pnlEstadisticasLayout.createSequentialGroup()
                        .addComponent(lblPlayer2)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(lblScore2))
                    .addGroup(pnlEstadisticasLayout.createSequentialGroup()
                        .addComponent(lblPlayer1)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(lblScore1))
                    .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 239, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jSeparator1, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(pnlEstadisticasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(pnlEstadisticasLayout.createSequentialGroup()
                        .addComponent(jLabel2, javax.swing.GroupLayout.PREFERRED_SIZE, 239, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED))
                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, pnlEstadisticasLayout.createSequentialGroup()
                        .addComponent(lblScoreMax, javax.swing.GroupLayout.PREFERRED_SIZE, 239, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)))
                .addComponent(jSeparator2, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(412, Short.MAX_VALUE))
        );
        pnlEstadisticasLayout.setVerticalGroup(
            pnlEstadisticasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pnlEstadisticasLayout.createSequentialGroup()
                .addComponent(jLabel1, javax.swing.GroupLayout.DEFAULT_SIZE, 37, Short.MAX_VALUE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(pnlEstadisticasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(lblPlayer1)
                    .addComponent(lblScore1))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(pnlEstadisticasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(lblPlayer2)
                    .addComponent(lblScore2)))
            .addComponent(jSeparator1, javax.swing.GroupLayout.DEFAULT_SIZE, 83, Short.MAX_VALUE)
            .addGroup(pnlEstadisticasLayout.createSequentialGroup()
                .addComponent(jLabel2, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(lblScoreMax)
                .addContainerGap(32, Short.MAX_VALUE))
            .addComponent(jSeparator2, javax.swing.GroupLayout.DEFAULT_SIZE, 83, Short.MAX_VALUE)
        );

        jMenu1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/gdm.png"))); // NOI18N
        jMenu1.setText("Juego");

        jMenuItem1.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_N, java.awt.event.InputEvent.ALT_MASK));
        jMenuItem1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/gdm.png"))); // NOI18N
        jMenuItem1.setText("Juego Nuevo");
        jMenuItem1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                nvoJuego(evt);
            }
        });
        jMenu1.add(jMenuItem1);

        jMenuItem2.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_E, java.awt.event.InputEvent.ALT_MASK));
        jMenuItem2.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/checkin.png"))); // NOI18N
        jMenuItem2.setText("Estadísticas");
        jMenu1.add(jMenuItem2);

        jMenuItem3.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_F4, java.awt.event.InputEvent.ALT_MASK));
        jMenuItem3.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/exit.gif"))); // NOI18N
        jMenuItem3.setText("Salir");
        jMenuItem3.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mnuSalir_ActionPerformed(evt);
            }
        });
        jMenu1.add(jMenuItem3);

        jMenuBar1.add(jMenu1);

        jMenu2.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/config.png"))); // NOI18N
        jMenu2.setText("Configuración");

        jMenuItem4.setText("Balón");
        jMenuItem4.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                configurarBalon(evt);
            }
        });
        jMenu2.add(jMenuItem4);

        jMenuItem5.setText("Barras");
        jMenuItem5.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                configurarBarras(evt);
            }
        });
        jMenu2.add(jMenuItem5);

        jMenuBar1.add(jMenu2);

        jMenu3.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/atb_help.gif"))); // NOI18N
        jMenu3.setText("Acerca De...");

        jMenuItem6.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_H, java.awt.event.InputEvent.ALT_MASK));
        jMenuItem6.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/atb_search.gif"))); // NOI18N
        jMenuItem6.setText("Acerca de Pong 3D");
        jMenuItem6.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mostrarAcerca(evt);
            }
        });
        jMenu3.add(jMenuItem6);

        jMenuBar1.add(jMenu3);

        setJMenuBar(jMenuBar1);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(pnlJuego, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, 1024, Short.MAX_VALUE)
            .addComponent(pnlEstadisticas, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addComponent(pnlEstadisticas, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(pnlJuego, javax.swing.GroupLayout.PREFERRED_SIZE, 646, javax.swing.GroupLayout.PREFERRED_SIZE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void mnuSalir_ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mnuSalir_ActionPerformed
        dispose();
        System.exit(0);
    }//GEN-LAST:event_mnuSalir_ActionPerformed

    private void nvoJuego(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_nvoJuego
        configurarJuego.setVisible(true);
    }//GEN-LAST:event_nvoJuego

    private void configurarBalon(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_configurarBalon
        configurarBalon.setVisible(true);        // TODO add your handling code here:
    }//GEN-LAST:event_configurarBalon

    private void configurarBarras(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_configurarBarras
        configurarBarras.setVisible(true);        // TODO add your handling code here:
    }//GEN-LAST:event_configurarBarras

    private void mostrarAcerca(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mostrarAcerca
        about.setVisible(true);
    }//GEN-LAST:event_mostrarAcerca

    /**
    * @param args the command line arguments
    */
    

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JMenu jMenu1;
    private javax.swing.JMenu jMenu2;
    private javax.swing.JMenu jMenu3;
    private javax.swing.JMenuBar jMenuBar1;
    private javax.swing.JMenuItem jMenuItem1;
    private javax.swing.JMenuItem jMenuItem2;
    private javax.swing.JMenuItem jMenuItem3;
    private javax.swing.JMenuItem jMenuItem4;
    private javax.swing.JMenuItem jMenuItem5;
    private javax.swing.JMenuItem jMenuItem6;
    private javax.swing.JSeparator jSeparator1;
    private javax.swing.JSeparator jSeparator2;
    private javax.swing.JLabel lblPlayer1;
    private javax.swing.JLabel lblPlayer2;
    private javax.swing.JLabel lblScore1;
    private javax.swing.JLabel lblScore2;
    private javax.swing.JLabel lblScoreMax;
    private javax.swing.JPanel pnlEstadisticas;
    private javax.swing.JPanel pnlJuego;
    // End of variables declaration//GEN-END:variables



}

