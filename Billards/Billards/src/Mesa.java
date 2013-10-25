import java.awt.*;
import java.awt.geom.Point2D;


public class Mesa {
	public float xHoyo1,xHoyo2,xHoyo3,xHoyo4,xHoyo5,xHoyo6;
	public float yHoyo1,yHoyo2,yHoyo3,yHoyo4,yHoyo5,yHoyo6;
	public Mesa(float radio){
		xHoyo1=50;
		yHoyo1=50;
		xHoyo2=50;
		yHoyo2=462;
		xHoyo3=924;
		yHoyo3=50;
		xHoyo4=924;
		yHoyo4=462;
		xHoyo5=487;
		yHoyo5=50;
		xHoyo6=487;
		yHoyo6=462;
		
	}
	
	public Point2D getHolePosition(int nHoyo) {
		switch(nHoyo){
		    case 1:	return new Point2D.Float(xHoyo1,yHoyo1);
			case 2:	return new Point2D.Float(xHoyo2,yHoyo2);
			case 3:	return new Point2D.Float(xHoyo3,yHoyo3);
			case 4:	return new Point2D.Float(xHoyo4,yHoyo4);
			case 5:	return new Point2D.Float(xHoyo5,yHoyo5);
			case 6:	return new Point2D.Float(xHoyo6,yHoyo6);
		}
		return null;
	}
	

}
