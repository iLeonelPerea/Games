package pong;
/**
 * @(#)MP3.java
 *
 *
 * @author
 * @version 1.00 2009/10/2
 */
import javax.media.*;
import java.io.*;
import java.net.URL;

public class MP3 extends Thread {
	private URL url;
	private MediaLocator mediaLocator;
	private Player playMP3;

    public MP3(String mp3) {
    	try{
   			this.url = new URL (mp3);
    	}
    	catch(java.net.MalformedURLException e){
    		System.out.println(e.toString());
    		}
    }

	public void run(){

		try{
		   mediaLocator = new MediaLocator(url);
		   System.out.println("Si entro");
		   playMP3 = Manager.createPlayer(mediaLocator);
		    }catch(java.io.IOException e)
		      {System.out.println(e.getMessage());
		    }catch(javax.media.NoPlayerException e)
		      {System.out.println(e.getMessage());}

		playMP3.addControllerListener(new ControllerListener()
		  {
		  public void controllerUpdate(ControllerEvent e)
		     {
		     if (e instanceof EndOfMediaEvent)
		         {
		         playMP3.stop();
		         playMP3.close();
		         }
		     }
		  }
		 );
		 playMP3.realize();
		 playMP3.start();
	}

	public void detener(){
		playMP3.addControllerListener(new ControllerListener()
		  {
		  public void controllerUpdate(ControllerEvent e)
		     {
		     if (e instanceof EndOfMediaEvent)
		         {
		         playMP3.stop();
		         playMP3.close();
		         }
		     }
		  }
		 );
		 playMP3.stop();
	}

	public void iniciar(){
		playMP3.start();
	}
}
