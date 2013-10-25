import java.awt.*;
import java.util.Formatter;
import java.awt.geom.Point2D;

/**
 * The bolabillar.
 */
public class Ball {
   private float x, y;           // Bolabillar's center (x, y)
   private Point2D pos;
   private float speedX, speedY; // Bolabillar's velocidad x y y
   private float radio;         // Bolabillar's radio
   private Color color;          // Bolabillar's color
   

   boolean collisionDetected = false;
   
  
   private float nextX, nextY;
   private float nextSpeedX, nextSpeedY;
   
   /** 
    * Constructor
   
    */
   public Ball(float x, float y, float radio, float speed, float angleInDegree, Color color) {
      this.x = x;
      this.y = y;
      // Convert velocity from polar to rectangular x and y.
      this.speedX = speed * (float)Math.cos(Math.toRadians(angleInDegree));
      this.speedY = speed * (float)Math.sin(Math.toRadians(angleInDegree));
      this.radio = radio;
      this.color = color;
   }
 
   
   public void draw(Graphics g) {
      g.setColor(color);
      g.fillOval((int)(x - radio), (int)(y - radio), (int)(2 * radio),(int)(2 * radio));
   }
   
   public void update() {
      if (collisionDetected) {
         // Collision detected, use the values computed.
         x = nextX;
         y = nextY;
         speedX = nextSpeedX;
         speedY = nextSpeedY;
         System.out.println("Colision");
      } else {
         // No collision, move one step and no change in speed.
         x += speedX;
         y += speedY;
         System.out.println("No Colision");
      }
      collisionDetected = false; // Clear the flag for the next step
   }
   public float getX(){
	   return x;
   }
   public float getY(){
	   return y;
   }
   public void setX(float x){
	   this.x=x;
   }
   public void setY(float y){
	   this.y=y;
   }
   /**
    * Detect collision with the given box in the coming time step.
    * If collision detected, update collisionDetected flag, and compute the
    *   response in nextX, nextY, nextSpeedX and nextSpeedY.
    * Otherwise, do nothing.
    * No actual update here, as there could be an earlier collision elsewhere.
    */
   public void collideWith(Box box) {
      float minX = box.getX() + radio;
      float minY = box.getY() + radio;
      float maxX = box.getX() + box.getWidth() - 1 - radio;
      float maxY = box.getY() + box.getHeight() - 1 - radio;
  
      nextX = x + speedX;
      nextY = y + speedY;
      

      if (speedX != 0) {
         if (nextX > maxX) {      
            collisionDetected = true;
            nextSpeedX = -speedX; 
            nextSpeedY = speedY;  
            nextX = maxX;
            nextY = (maxX - x) * speedY / speedX + y; 
         } else if (nextX < minX) {  
            collisionDetected = true;
            nextSpeedX = -speedX;    
            nextSpeedY = speedY;    
            nextX = minX;
            nextY = (minX - x) * speedY / speedX + y; 
         }
      }
     
      if (speedY != 0) {
         if (nextY > maxY) {      
            collisionDetected = true;
            nextSpeedX = speedX; 
            nextSpeedY = -speedY; 
            nextY = maxY;
            nextX = (maxY - y) * speedX / speedY + x; 
         } else if (nextY < minY) {  
            collisionDetected = true;
            nextSpeedX = speedX;     
            nextSpeedY = -speedY;    
            nextY = minY;
            nextX = (minY - y) * speedX / speedY + x; 
         }
      }
   }
   
   
   public float getSpeed() {
      return (float)Math.sqrt(speedX * speedX + speedY * speedY);
   }
   public float getSpeedX(){
	   return (float)speedX;
   }
   public float getSpeedY(){
    return (float)speedY;
   }
   
  
   public float getMoveAngle() {
      return (float)Math.toDegrees(Math.atan2(speedY, speedX));
   }
   public void setSpeed(float f,float angleInDegree){
	   this.speedX=f*(float)Math.cos(Math.toRadians(angleInDegree));
	   this.speedY=f*(float)Math.sin(Math.toRadians(angleInDegree));
   }
   public void setSpeedX(float s){
	   speedX=s;
   }
   public void setSpeedY(float s){
	   speedY=s;
   }
   /** Getter for radio */
   public float getradio() {
      return radio;
   }

   public String toString() {
      StringBuilder sb = new StringBuilder();
      Formatter formatter = new Formatter(sb);
      formatter.format("(%3.0f,%3.0f) V=%4.1f \u0398=%5.1f", x, y, getSpeed(),
            getMoveAngle());
      return sb.toString();
   }
   /*public boolean checa(Ball b1, Ball b2){
	   double cx1=(double)b1.x;
	   double cx2=(double)b2.x;
	   double cy1=(double)b1.y;
	   double cy2=(double)b2.y;
	   double sx1=(double)b1.speedX;
	   double sx2=(double)b2.speedX;
	   double sy1=(double)b1.speedY;
	   double sy2=(double)b2.speedY;
	   double radio=(double)b1.radio;
	   double inte;
	   inte=movingSphereIntersectsMovingSphere(cx1,cy1,sx1,sy1,radio,cx2,cy2,sx2,sy2,radio);
	   if(inte==-1){
		  return false;
	   }
	  return true;
   }*/
   
   /*private double movingSphereIntersectsMovingSphere(double center1X,double center1Y, double speed1X, double speed1Y, double radio1,double center2X, double center2Y, double speed2X, double speed2Y,double radio2) {
   
      // Rearrange the parameters
      double centerX = center1X - center2X;
      double centerY = center1Y - center2Y;
      double speedX = speed1X - speed2X;
      double speedY = speed1Y - speed2Y;
      double radio = radio1 + radio2;
      double radioSq = radio * radio;
      double speedXSq = speedX * speedX;
      double speedYSq = speedY * speedY;
      double speedSq = speedXSq + speedYSq;
   
      double termBsq4ac = radioSq * speedSq - (centerX * speedY - centerY * speedX)
            * (centerX * speedY - centerY * speedX);
      double termMinusB = -speedX * centerX - speedY * centerY;
      double term2a = speedSq;
   
      if (termBsq4ac < 0) {
         // No intersection.
         // Moving spheres may cross at different times, or move in parallel.
         return -1;
      } else {
         // Accept the smallest positive t as the solution. 
         termBsq4ac = Math.sqrt(termBsq4ac);
         double sol1 = (termMinusB + termBsq4ac) / term2a;
         double sol2 = (termMinusB - termBsq4ac) / term2a;
         if (sol1 > 0 && sol2 > 0) {
            return Math.min(sol1, sol2);
         } else if (sol1 > 0) {
            return sol1;
         } else if (sol2 > 0) {
            return sol2;
         } else {
            return -1; // both solutions negative
         }
      }
   }
   
   // Get the rotated x after rotating by theta in inverted-y coordinates
   private double getRotatedX(double vectorX, double vectorY, double theta) {
      // In inverted-y coordinates
      return vectorX * Math.cos(theta) + vectorY * Math.sin(theta);
   }
   
   // Get the rotated y after rotating by theta in inverted-y coordinates
   private double getRotatedY(double vectorX, double vectorY, double theta) {
      // In inverted-y coordinates
      return -vectorX * Math.sin(theta) + vectorY * Math.cos(theta);
   }
   
   /*public void collideWith(Ball b1) {
	      float minX = b1.x - (2*radio);
	      float minY = b1.y - (2*radio);
	      float maxX = b1.x + (2*radio);
	      float maxY = b1.y + (2*radio);
	  
	      nextX = x + speedX;
	      nextY = y + speedY;
	      

	      if (speedX != 0) {
	         if (nextX == maxX) {      
	            collisionDetected = true;
	            nextSpeedX = -speedX; 
	            nextSpeedY = speedY;  
	            nextX = maxX;
	            nextY = (maxX - x) * speedY / speedX + y; 
	         } else if (nextX == minX) {  
	            collisionDetected = true;
	            nextSpeedX = -speedX;    
	            nextSpeedY = speedY;    
	            nextX = minX;
	            nextY = (minX - x) * speedY / speedX + y; 
	         }
	      }
	     
	      if (speedY != 0) {
	         if (nextY == maxY) {      
	            collisionDetected = true;
	            nextSpeedX = speedX; 
	            nextSpeedY = -speedY; 
	            nextY = maxY;
	            nextX = (maxY - y) * speedX / speedY + x; 
	         } else if (nextY == minY) {  
	            collisionDetected = true;
	            nextSpeedX = speedX;     
	            nextSpeedY = -speedY;    
	            nextY = minY;
	            nextX = (minY - y) * speedX / speedY + x; 
	         }
	      }
	   }*/
   public Point2D getPosicion(){
	 Point2D punto = new Point2D.Double(x,y);
	 return punto;
   }
   
}