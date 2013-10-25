package pong;


// TexPlane.java
// Andrew Davison, September 2006, ad@fivedots.coe.psu.ac.th

/* Creates a single quad array of 4 vertices with a texture.
   The texture is applied without any blending with a material.
*/

import javax.vecmath.*;
import javax.media.j3d.*;
import com.sun.j3d.utils.image.*;


public class TexPlane extends Shape3D 
{
  private static final int NUM_VERTS = 4;

  public TexPlane(Point3d p1, Point3d p2, Point3d p3, Point3d p4,String texFnm, String descripcion)
  { createGeometry(p1, p2, p3, p4);

    Texture2D tex = loadTexture(texFnm);
    Appearance app = new Appearance();
    app.setTexture(tex);      // set the texture
    setAppearance(app);
    this.setUserData(descripcion);
  } // end of TexPlane()


  private void createGeometry(Point3d p1, Point3d p2, Point3d p3, Point3d p4)
  {
    QuadArray plane = new QuadArray(NUM_VERTS, 
					GeometryArray.COORDINATES |
					GeometryArray.TEXTURE_COORDINATE_2 );

    // anti-clockwise from bottom left
    plane.setCoordinate(0, p1);
    plane.setCoordinate(1, p2);
    plane.setCoordinate(2, p3);
    plane.setCoordinate(3, p4);

    TexCoord2f q = new TexCoord2f();
    q.set(0.0f, 0.0f);    
    plane.setTextureCoordinate(0, 0, q);
    q.set(1.0f, 0.0f);   
    plane.setTextureCoordinate(0, 1, q);
    q.set(1.0f, 1.0f);    
    plane.setTextureCoordinate(0, 2, q);
    q.set(0.0f, 1.0f);   
    plane.setTextureCoordinate(0, 3, q);  

    setGeometry(plane);
  }  // end of createGeometry()


  private Texture2D loadTexture(String fn)
  // load image from file fn as a texture
  {
    TextureLoader texLoader = new TextureLoader(fn, null);
    Texture2D texture = (Texture2D) texLoader.getTexture();
    if (texture == null)
      System.out.println("Cannot load texture from " + fn);
    else {
      System.out.println("Loaded texture from " + fn);

      // remove edge texels, so no seams between texture faces
      texture.setBoundaryModeS(Texture.CLAMP_TO_EDGE);
      texture.setBoundaryModeT(Texture.CLAMP_TO_EDGE);

      // smoothing for texture enlargement/shrinking
      texture.setMinFilter(Texture2D.BASE_LEVEL_LINEAR); //shrink texels
      texture.setMagFilter(Texture2D.BASE_LEVEL_LINEAR); // enlarge
      texture.setEnable(true);
    }
    return texture;
  }  // end of loadTexture()

} // end of TexPlane class

