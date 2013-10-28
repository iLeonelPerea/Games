using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace Zelda_AgusTemple
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Nivel3 : Escena
    {
        protected Texture2D fondo, room, lin;
        protected SpriteBatch spriteBatch;
        protected AudioComponent audioComponent;
        public Link jugador;
        protected bool pArriba = false, pAbajo = false, pDer = true, pIzq = false;
        protected Rectangle pantalla;
        protected Keese k1, k2, k3, k4, k5;
        protected Switch s;
        protected Key k;
        protected bool llaveactiva;

        public Nivel3(Game game, Link l, float x, float y)
            : base(game)
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            audioComponent = (AudioComponent)Game.Services.GetService(typeof(AudioComponent));
            fondo = Game.Content.Load<Texture2D>("back");
            room = Game.Content.Load<Texture2D>("Fondos/niv3");
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            jugador = l;
            jugador.setX(x);
            jugador.setY(y);
            llaveactiva = false;
            s = new Switch(game, false, 65, 87);
            k1 = new Keese(game, 100, 100);
            k2 = new Keese(game, 200, 100);
            k3 = new Keese(game, 300, 100);
            k4 = new Keese(game, 150, 200);
            k5 = new Keese(game, 250, 200);
            Componentes.Add(s);
            Componentes.Add(jugador);
            Componentes.Add(k1);
            Componentes.Add(k2);
            Componentes.Add(k3);
            Componentes.Add(k4);
            Componentes.Add(k5);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            //backMusic.Stop(AudioStopOptions.Immediate);
            base.Hide();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            k1.setPosL(jugador.getPos());
            k2.setPosL(jugador.getPos());
            k3.setPosL(jugador.getPos());
            k4.setPosL(jugador.getPos());
            k5.setPosL(jugador.getPos());
            s.setPosL(jugador.getPos());
            Herir();
            Matar();
            LimitarLink();
            if (jugador.getVida() <= 0)
                Componentes.Remove(jugador);

            if (s.activado && !llaveactiva)
            {
                k = new Key(Game, 150, 200);
                Componentes.Add(k);
                llaveactiva = true;
            }
            if (k!= null && k.Enabled)
            Tomarllave();
            base.Update(gameTime);
        }
        public void Tomarllave()
        {
            if (k != null)
            {
                k.setPosL(jugador.getPos());
                if (k.Tomar())
                {
                    jugador.tomoLlave();
                    k.Enabled = false;
                    Componentes.Remove(k);
                }
            }
        }
        public void Herir()
        {
            if (k1.Enabled)
                jugador.Herir(k1.getArea());
            if (k2.Enabled)
                jugador.Herir(k2.getArea());
            if (k3.Enabled)
                jugador.Herir(k3.getArea());
            if (k4.Enabled)
                jugador.Herir(k4.getArea());
            if (k5.Enabled)
                jugador.Herir(k5.getArea());
        }
        public void Matar()
        {
            if (k1.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                k1.Enabled = false;
                Componentes.Remove(k1);
            }
            if (k2.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                k2.Enabled = false;
                Componentes.Remove(k2);
            }
            if (k3.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                k3.Enabled = false;
                Componentes.Remove(k3);
            }
            if (k4.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                k4.Enabled = false;
                Componentes.Remove(k4);
            }
            if (k5.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                k5.Enabled = false;
                Componentes.Remove(k5);
            }
        }
        public int EntroPuerta()
        {
            Rectangle rect;
            if (pArriba)
            {
                rect = new Rectangle(225, 50, 50, 40);
                if (rect.Contains(jugador.getArea()))
                    return 1;
            }
            if (pAbajo)
            {
                rect = new Rectangle(225, 265, 50, 50);
                if (rect.Contains(jugador.getArea()))
                    return 2;
            }
            if (pDer)
            {
                rect = new Rectangle(440, 150, 45, 45);
                if (rect.Contains(jugador.getArea()))
                    return 3;
            }
            if (pIzq)
            {
                rect = new Rectangle(25, 150, 45, 45);
                if (rect.Contains(jugador.getArea()))
                    return 4;
            }
            return 0;
        }
        public void LimitarLink()
        {
            for (int i = 0; i < Paredes().Length; i++)
            {
                if (jugador.getArea().Intersects(Paredes()[i]))
                {
                    if (i >= 2)
                        if (i >= 4)
                            if (i >= 6)
                                jugador.setY(jugador.getPos().Y - 2);
                            else
                                jugador.setX(jugador.getPos().X - 2);
                        else
                            jugador.setX(jugador.getPos().X + 2);
                    else
                        jugador.setY(jugador.getPos().Y + 2);
                }
            }
        }
        public Rectangle[] Paredes()
        {
            Rectangle[] rect = new Rectangle[8];
            //Arriba izq
            if (!pArriba)
                rect[0] = new Rectangle(25, 50, 250, 35);
            else
                rect[0] = new Rectangle(25, 50, 200, 35);
            //Arriba der
            rect[1] = new Rectangle(280, 50, 200, 35);
            //Izq arriba
            if (!pIzq)
                rect[2] = new Rectangle(25, 50, 35, 150);
            else
                rect[2] = new Rectangle(25, 50, 35, 100);
            //Izq abajo
            rect[3] = new Rectangle(25, 200, 35, 100);
            //Der arriba
            if (!pDer)
                rect[4] = new Rectangle(440, 50, 35, 150);
            else
                rect[4] = new Rectangle(440, 50, 35, 100);
            //Der abajo
            rect[5] = new Rectangle(440, 200, 35, 100);
            //Abajo derecha
            if (!pAbajo)
                rect[6] = new Rectangle(25, 265, 250, 35);
            else
                rect[6] = new Rectangle(25, 265, 200, 35);
            //Abajo izquierda
            rect[7] = new Rectangle(275, 265, 200, 35);
            return rect;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(fondo, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(room, new Vector2(25, 50), Color.White);
            base.Draw(gameTime);
        }
    }
}