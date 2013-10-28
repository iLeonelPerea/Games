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
    public class Nivel5 : Escena
    {
        protected Texture2D fondo, room, lin;
        protected SpriteBatch spriteBatch;
        protected AudioComponent audioComponent;
        public Link jugador;
        protected bool pArriba = true, pAbajo = true, pDer = false, pIzq = false;
        protected Tektike t1, t2, t3, t4;
        private Rectangle pantalla;

        public Nivel5(Game game, Link l, float x, float y)
            : base(game)
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            audioComponent = (AudioComponent)Game.Services.GetService(typeof(AudioComponent));
            fondo = Game.Content.Load<Texture2D>("back");
            room = Game.Content.Load<Texture2D>("Fondos/niv5");
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            jugador = l;
            jugador.setX(x);
            jugador.setY(y);
            t1 = new Tektike(game, 100, 100);
            t2 = new Tektike(game, 300, 100);
            t3 = new Tektike(game, 100, 200);
            t4 = new Tektike(game, 300, 200);
            Componentes.Add(t1);
            Componentes.Add(t2);
            Componentes.Add(t3);
            Componentes.Add(t4);
            Componentes.Add(jugador);

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
            t1.setPosL(jugador.getPos());
            t2.setPosL(jugador.getPos());
            t3.setPosL(jugador.getPos());
            t4.setPosL(jugador.getPos());
            LimitarLink();
            Herir();
            Matar();
            if (jugador.getVida() <= 0)
                Componentes.Remove(jugador);
            base.Update(gameTime);
        }
        public void Herir()
        {
            if (t1.Enabled)
                jugador.Herir(t1.getArea());
            if (t2.Enabled)
                jugador.Herir(t2.getArea());
            if (t3.Enabled)
                jugador.Herir(t3.getArea());
            if (t4.Enabled)
                jugador.Herir(t4.getArea());
        }
        public void Matar()
        {
            if (t1.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                t1.Enabled = false;
                Componentes.Remove(t1);
            }
            if (t2.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                t2.Enabled = false;
                Componentes.Remove(t2);
            }
            if (t3.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                t3.Enabled = false;
                Componentes.Remove(t3);
            }
            if (t4.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                t4.Enabled = false;
                Componentes.Remove(t4);
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