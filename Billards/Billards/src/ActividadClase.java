/**
 * @(#)ActividadClase.java
 *
 * ActividadClase Applet application
 *
 * @author 
 * @version 1.00 2009/9/28
 */
 
import java.awt.*;
import java.applet.*;
import java.awt.Graphics;
import javax.swing.*;
import java.awt.Image;//Imagen
import java.awt.Color;//Colores
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.geom.Point2D;
import java.io.File;
import java.awt.geom.AffineTransform;
import javazoom.jlgui.basicplayer.BasicPlayer;
import javazoom.jlgui.basicplayer.BasicPlayerException;

public class ActividadClase extends Applet implements Runnable, KeyListener{
	Ball p;
	Point2D point;
	Ball []p1;
	int dx;
	int dy;
	float anguloRectangulo;
	float angulo2;
	float VelocidadImprimida;
	Thread hilo;
	Image imgFondo;
	Image Taco;
	//Rectangle taco;
	float xt ;
	float yt ;
	//private AudioClip sonidito;
	private Box box;
	private Mesa mesa;
	//doble buffer
    private Image dbImage;
    private Graphics dbg;
    boolean pausa;
    boolean presion;
    private BasicPlayer basicPlayer;
    String direccion ="C:\\Users\\Christian\\Music\\Tiesto-Kaleidoscope-Promo-CD-REPACK-2009-XTC\\Tiesto-Kaleidoscope-Promo-CD-REPACK-2009-XTC\\12-tiesto-_who_wants_to_be_alone_(feat._nelly_furtado)-xtc.mp3";

	
	public void init() {
		addKeyListener(this);
		p=new Ball(269, 280, 12f, 0f, 0f,Color.WHITE);
		p1=new Ball[15];
		anguloRectangulo=180;
		VelocidadImprimida=0;
		xt=0;
		angulo2=0;
		yt=0;
		mesa = new Mesa(12f*2);
		Taco= getImage(getCodeBase(),"taco.jpg");;
		//for(int i=0;i<13;i++){
			p1[0] =new Ball(705, 280, 12f, 0f, 100f,Color.BLACK);
			p1[1] = new Ball(705+24f,280-12f,12f,0f,100f,Color.BLACK);
			p1[2] = new Ball(705+24f,280+12f,12f,0f,100f,Color.BLACK);
			p1[3] = new Ball(705+48f,280,12f,100f,0f,Color.BLACK);
			p1[4] = new Ball(705+48f,280-24f,12f,0f,100f,Color.BLACK);
			p1[5] = new Ball(705+48f,280+24f,12f,0f,100f,Color.BLACK);
			p1[6] = new Ball(705+72f,280-36f, 12f,0f, 100f,Color.BLACK);
			p1[7] = new Ball(705+72f,280-12f,12f,0f,100f,Color.BLACK);
			p1[8] = new Ball(705+72f,280+12f,12f,0f,100f,Color.BLACK);
			p1[9] = new Ball(705+72f,280+36f,12f,0f,100f, Color.BLACK);
			p1[10] = new Ball(705+96f,280+36f,12f,0f,100f, Color.BLACK);
			p1[11] = new Ball(705+96f,280-12f,12f,0f,100f, Color.BLACK);
			p1[12] = new Ball(705+96f,280,12f,0f,100f, Color.BLACK);
			p1[13] = new Ball(705+96f,280+12f,12f,0f,100f,Color.BLACK);
			p1[14] = new Ball(705+96f,280+24f,12f,0f,100f, Color.BLACK);
		//}
		dx=5;
		dy=5;
		imgFondo = getImage(getCodeBase(),"pool.jpg");
		Reproductor();
		box=new Box(50,50,924,462);
		hilo = new Thread (this);
		try {
			basicPlayer.play();

			} 
		catch (BasicPlayerException e) {
			e.printStackTrace();
		}

		hilo.start();
		this.setFocusable(true);
		
	}

	public void paint(Graphics g) {
		g.drawImage(imgFondo, 0, 0, this);
		if(p!=null){
			p.draw(g);
			for(int i=0;i<15;i++){
				p1[i].draw(g);
			}
		}
		for(int i=0;i<15;i++){
			if(p.getSpeed()==0 && p1[i].getSpeed() == 0){
			    g.setColor(Color.lightGray);
			    Point2D punto;
			    punto = new Point2D.Float(xt,yt);
			    drawLineReal(g, p.getX()-p.getradio(),p.getY()-p.getradio(),punto);
			}
		}
		/*int x= taco.x;
		int y= taco.y;
		int h= taco.height;
		int w=taco.width;

		g.draw3DRect(x, y, w, h, false);*/
	//	g.drawImage(Taco,(int)p.getX(),(int)p.getY(),this);
		
	}
	
	private void drawLineReal(Graphics g, float x, float y, Point2D punto) {
		// TODO Auto-generated method stub
		//g.drawLine((int)(x), (int)(y),(int)(punto.getX()),(int)(punto.getY()));
		int posxtaco= (int)(p.getX()-p.getradio()-811);
		int posytaco=0;
		AffineTransform aft= new AffineTransform();
		aft.rotate((double)(angulo2));
		Graphics2D g2= (Graphics2D)g;
		g2.setTransform(aft);
		
		posytaco=(int)((Math.sin(angulo2)*10)+(p.getY()-11));
		posxtaco=(int)((Math.cos(angulo2)*10)+(p.getX()-p.getradio()-811));
		g2.drawImage(Taco,posxtaco,posytaco,this);
		AffineTransform af = new AffineTransform();
		g2.setTransform(af);
	}
	public void ChecaBachacas(){
		for(int i=0;i<13;i++){
			if(p1[i].getX()==50+p1[i].getradio() && p1[i].getY()==50+p1[i].getradio()){
				p1[i].setSpeedX(0);
				p1[i].setSpeedY(0);
				p1[i].setX(-10);
				p1[i].setY(-10);
			//	Score+=10;
			}else if(p1[i].getX()==50+p1[i].getradio() && p1[i].getY()==462-p1[i].getradio()){
				p1[i].setSpeedX(0);
				p1[i].setSpeedY(0);
				p1[i].setX(-10);
				p1[i].setY(-10);
				//Score+=10;
			}else if(p1[i].getX()==924+p1[i].getradio() && p1[i].getY()==50+p1[i].getradio()){
				p1[i].setSpeedX(0);
				p1[i].setSpeedY(0);
				p1[i].setX(-10);
				p1[i].setY(-10);
				//Score+=10;
			}else if(p1[i].getX()==924-p1[i].getradio() && p1[i].getY()==462-p1[i].getradio()){
				p1[i].setSpeedX(0);
				p1[i].setSpeedY(0);
				p1[i].setX(-10);
				p1[i].setY(-10);
				//Score+=10;
			}else if(p1[i].getX()==487+p1[i].getradio() && p1[i].getY()==462-p1[i].getradio()){
				p1[i].setSpeedX(0);
				p1[i].setSpeedY(0);
				p1[i].setX(-10);
				p1[i].setY(-10);
				//Score+=10;
			}else if(p1[i].getX()==487+p1[i].getradio() && p1[i].getY()==50+p1[i].getradio()){
				p1[i].setSpeedX(0);
				p1[i].setSpeedY(0);
				p1[i].setX(-10);
				p1[i].setY(-10);
				//Score+=10;
			}
		}
	}

	public void run(){
		while(true){
			if(presion){
				VelocidadImprimida+=100f;
				System.out.println("Presion: " + VelocidadImprimida);
			}else{
				ChecaBachacas();
				p.collideWith(box);
				p.update();
				dormir(30);
				//ChecaColision();
				// ChecaMesa();
				checkCollisionBalls();
				for(int i=0;i<15;i++){
					p1[i].collideWith(box);
					checaVelocidad();
					p.update();
					p1[i].update();
					checkCollisionBalls();
				}
				repaint();
				dormir(60);
			}
			
		}
	}
	public void checaVelocidad(){
		for(int i=0;i<15;i++){
			float vblanca = p.getSpeed();
			float ablanca = p.getMoveAngle();
			float vpelota =p1[i].getSpeed();
			float apelota =p1[i].getMoveAngle();
			if(p.getSpeed()>.001){
				vblanca=(float)(vblanca*.97);
			}else {
				vblanca=0;
			}
			if(p1[i].getSpeed()>.001){
				vpelota=(float)(vpelota*.97);
			}else{
				vpelota=0;
			}
			p.setSpeed(vblanca, ablanca);
			p1[i].setSpeed(vpelota, apelota);
			p1[i].update();
			p.update();
			
		}
	}
	 //metodo sleep del thread
	public void dormir(int ms){
		try{
			hilo.sleep(ms);
		}catch(InterruptedException e){
			System.out.println(e.getMessage());
		}
	}
	//3° metdodo que checa colision entre bolas
	private void checkCollisionBalls(){
    	for(int i=0;i<15;i++){
			double distance = 0;
			distance = (double)(Math.sqrt(Math.pow((p.getX()-p1[i].getX()),2)+Math.pow(p.getY()-p1[i].getY(), 2)));
			//distancia=
			///////////////////////skjdhfkhsdflksdljfhsd
			if(distance<p1[i].getradio()+p.getradio()){
				
				double difX = p.getX()-p1[i].getX();
				double difY = p.getY()-p1[i].getY();
				double angle = Math.atan2(difY,difX);
				double cosA = Math.cos(angle);
				double sinA = Math.sin(angle);
				p.setX((float)(p1[i].getX()+cosA*p1[i].getradio()*2));
				p.setY((float)(p1[i].getY()+sinA*p1[i].getradio()*2));
				double sx1 = cosA*p.getSpeedX()+sinA*p.getSpeedY();
				double sy1 = cosA*p.getSpeedY()-sinA*p.getSpeedX();
				double sx2 = cosA*p1[i].getSpeedX()+sinA*p1[i].getSpeedY();
				double sy2 = cosA*p1[i].getSpeedY()-sinA*p1[i].getSpeedX();
				sx1 += sx2;
				sx2 = sx1-sx2;
				sx1 = sx1-sx2;
				p.setSpeedX((float)(cosA*sx1-sinA*sy1));
				p.setSpeedY((float)(cosA*sy1+sinA*sx1));
				p1[i].setSpeedX((float)(cosA*sx2-sinA*sy2));
				p1[i].setSpeedY((float)(cosA*sy2+sinA*sx2));
			}
		}
    	for(int i=0;i<p1.length;i++){
    		for(int k=i+1;k<p1.length;k++){
    			
    			double distance = (double)(Math.sqrt(Math.pow((p1[i].getX()-p1[k].getX()),2)+Math.pow(p1[i].getY()-p1[k].getY(), 2)));
    			if(distance<p1[k].getradio()+p1[i].getradio()){
    				//p1[i].collisionDetected=true;
    			
    				double difX = p1[i].getX()-p1[k].getX();
    				double difY = p1[i].getY()-p1[k].getY();
    				double angle = Math.atan2(difY,difX);
    				double cosA = Math.cos(angle);
    				double sinA = Math.sin(angle);
    				p1[i].setX((float)(p1[k].getX()+cosA*p1[k].getradio()*2));
    				p1[i].setY((float)(p1[k].getY()+sinA*p1[k].getradio()*2));
    				double sx1 = cosA*p1[i].getSpeedX()+sinA*p1[i].getSpeedY();
    				double sy1 = cosA*p1[i].getSpeedY()-sinA*p1[i].getSpeedX();
    				double sx2 = cosA*p1[k].getSpeedX()+sinA*p1[k].getSpeedY();
    				double sy2 = cosA*p1[k].getSpeedY()-sinA*p1[k].getSpeedX();
    				sx1 += sx2;
    				sx2 = sx1-sx2;
    				sx1 = sx1-sx2;
    				p1[i].setSpeedX((float)(cosA*sx1-sinA*sy1));
    				p1[i].setSpeedY((float)(cosA*sy1+sinA*sx1));
    				p1[k].setSpeedX((float)(cosA*sx2-sinA*sy2));
    				p1[k].setSpeedY((float)(cosA*sy2+sinA*sx2));
    			}
    		}
    	}
    }
	/*public void CheckCollision(){
		for(int i=0; i<12; i++){
			double d1;
			double dx1=p1[i].x;
			double dy1=p1[i].y;
			d1=p1[i].getradio();
			for(int j=1; j<13;j++){
				double dx2=p1[j].x;
				double dy2=p1[j].y;
				double pitagoras=(double)(Math.sqrt(Math.pow(dx1-dx2,2)+Math.pow(dy1-dy2, 2)));
				if(pitagoras<=d1){
					if(p1[i].x+d1==p1[j].x){
						p1[i].collisionDetected=true;
						p1[j].collisionDetected=true;
						float s1x=p1[i].getSpeedX();
						s1x=s1x*-1;
						float s2x=p1[i].getSpeedX();
						s2x=s2x*-1;
						p1[i].nextSpeedX = s1x; 
						p1[j].nextSpeedX=s2x;
						p1[i].update();
						p1[j].update();
						//float s1x=p1[i].getSpeedX();
						//s1x=s1x*-1;
						
						//p1[i]=p1[i].x*-1;
					}
					if(p1[i].y+d1==p1[j].y){
						p1[i].collisionDetected=true;
						p1[j].collisionDetected=true;
						float s1y=p1[i].getSpeedY();
						s1y=s1y*-1;
						float s2y=p1[i].getSpeedX();
						s2y=s2y*-1;
						p1[i].nextSpeedX = s1y; 
						p1[j].nextSpeedX=s2y;
						p1[i].update();
						p1[j].update();
						//p1[i].x=p1.x*-1;
					}
					//p1[i].getSpeedX();
				}
			}
		}
		
	
	}*/
	//checa los hollos de la mesa
	public void ChecaMesa(){
		for(int k=1;k<=6;k++){
    		if(getDistance(p.getPosicion(),mesa.getHolePosition(k))<1+p.getradio()/2){
    			p.setSpeedX(0);
    			p.setSpeedY(0);
    			p.setX(0);
    			p.setY(6);
    		}
    		for(int i=0;i<p1.length;i++){
    			double distance = getDistance(p1[i].getPosicion(),mesa.getHolePosition(k));
    			if(distance<1+p1[i].getradio()/2){
    				p1[i].setSpeedX(0);
    				p1[i].setSpeedY(0);
    			}
    		}
    	}
	}
	//obtiene la distancia entre dos puntos 2d
	private double getDistance(Point2D punto1, Point2D punto2){
	    	double difX = punto1.getX()-punto2.getX();
			double difY = punto1.getY()-punto2.getY();
			return Math.sqrt(Math.pow(difX,2)+Math.pow(difY,2));
	}
	/*
	public void colision(){
		// Primera pelota
		for(int i=0;i<4;i++){
		
			if(p[i].x+p[i].radio>getWidth()){
				dx[i]=-dx[i];
				sonidito.play();			
			}
			if(p[i].getPosY()+p[i].getAlto()>getHeight()){
				dy[i]=-dy[i];
				sonidito.play();
			}
			if(p[i].getPosX()<0){
				dx[i] = -dx[i];
				sonidito.play();
			}
			if(p[i].getPosY()<0){
				dy[i]=-dy[i];
				sonidito.play();
			}
		}
	}*/
	//MEtodo de updatte del Doblebuffer
	 public void update (Graphics g){
         //inicializa el doble buffer
         if (dbImage==null){
                 dbImage= createImage(this.getSize().width, this.getSize().height);
                 dbg=dbImage.getGraphics();
         }
         //actualiza la imagen de fondo
         dbg.setColor(getBackground());
         dbg.fillRect(0,0,this.getSize().width, this.getSize().height);
         //actualiza el foreground
         dbg.setColor(getForeground());
         paint(dbg);
         //dibuja la imagen actualizada
         g.drawImage(dbImage,0,0,this);

	 }
	 //metodo del reproductor de Mp3
	 public void Reproductor() {
		 
		 basicPlayer = new BasicPlayer();
		 loadFile(direccion);

		 }

	 //metodo que obtiene la dirrecion del archivo mp3
	 public void loadFile(String direccion){
		 try {
			 basicPlayer.open(new File(direccion));
		 } catch (BasicPlayerException ex) {
			//Logger.getLogger(ventana.class.getName()).log(Level.SEVERE, null, ex);
		 }
	 }
/*
	 public void modTaco(float a){
		float xt =p.getX();
		float yt =p.getY();
		if(a>180){
			yt-=p.getradio();
		}
		if(a<90 && a>270){
			xt-=p.getradio();
		}
		
	 }
	 */
	@Override
	public void keyPressed(KeyEvent e) {
		switch(e.getKeyCode()){
		case KeyEvent.VK_LEFT:
			angulo2+=25f;
			repaint();
			break;
		case KeyEvent.VK_RIGHT:
			angulo2-=25f;
			repaint();
			break;
		case KeyEvent.VK_SPACE:
			presion=true;
			break;
		case KeyEvent.VK_P:
			pausa=true;
			pausaMusica();
						break;
		case KeyEvent.VK_R:
			pausa=false;
			reanudaMusica();
			break;
		}
		
	}
	public void reanudaMusica(){
		if(!pausa){
			try{
				basicPlayer.resume();
			}
			catch(BasicPlayerException e1){
				e1.printStackTrace();
			}
		}
	}
	public void pausaMusica(){
		if(pausa){
			try {
				basicPlayer.pause();
			} catch (BasicPlayerException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
		}
			
	}	
	
	@Override
	public void keyReleased(KeyEvent arg0) {
		// TODO Auto-generated method stub
		switch(arg0.getKeyCode()){
		case KeyEvent.VK_SPACE:
			/*if(p.getSpeed()==0){
				p.setSpeedX((float)(VelocidadImprimida*Math.cos(angulo2)));
				p.setSpeedY((float)(VelocidadImprimida*Math.sin(angulo2)));
				p.update();
				for(int i=0;i<13;i++){
					p1[i].update();
				}*/
			
				p.setSpeed(VelocidadImprimida, angulo2);
				dormir(40);
				p.update();
				checaVelocidad();
				presion=false;
				checkCollisionBalls();
				//repaint();
			//}
			break;
		}
		
		

		
	}

	@Override
	public void keyTyped(KeyEvent e) {
		// TODO Auto-generated method stub
	
		
	}

	
}