import java.awt.*;
/**
 * The container box.
 */
public class Box {
   private int x, y;          // Top-left corner (x, y)
   private int width, height; // Width and height
   private Color color;
   
   /** Constructor */
   public Box(int x, int y, int width, int height) {
      this.x = x;
      this.y = y;
      this.width = width;
      this.height = height;
      this.color = Color.BLACK;
   }
   
 
   public void setColor(Color color) {
      this.color = color;
   }
   
  
   public int getX() {
      return x;
   }
   
  
   public int getY() {
      return y;
   }
   
   /** Getter for width. */
   public int getWidth() {
      return width;
   }
   
   /** Getter for height. */
   public int getHeight() {
      return height;
   }
   
   public void draw(Graphics g) {
      g.setColor(color);
      g.fillRect(x, y, width, height);
   }
}