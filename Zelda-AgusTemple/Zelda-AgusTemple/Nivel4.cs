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
    public class Nivel4 : Escena
    {
        protected Texture2D fondo, room, lin;
        protected SpriteBatch spriteBatch;
        protected AudioComponent audioComponent;
        public Link jugador;
        protected Tektike tk1, tk2;
        protected Keese k1, k2;
        protected Trap tr1, tr2;
        protected bool pArriba = false, pAbajo = false, pDer = false, pIzq = true;
        private Rectangle pantalla;

        public Nivel4(Game game, Link l, float x, float y)
            : base(game)
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            audioComponent = (AudioComponent)Game.Services.GetService(typeof(AudioComponent));
            fondo = Game.Content.Load<Texture2D>("back");
            room = Game.Content.Load<Texture2D>("Fondos/niv4");
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            jugador = l;
            jugador.setX(x);
            jugador.setY(y);
            tk1 = new Tektike(game, 90, 120);
            tk2 = new Tektike(game, 200, 70);
            k1 = new Keese(game, 50, 80);
            k2 = new Keese(game, 220, 150);
            tr1 = new Trap(game, game.Window.ClientBounds.Width/2, 70);
            tr2 = new Trap(game, game.Window.ClientBounds.Width / 2, 250);
            Componentes.Add(tr1);
            Componentes.Add(tr2);
            Componentes.Add(tk1);
            Componentes.Add(tk2);
            Componentes.Add(k1);
            Componentes.Add(k2);
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
            tr1.setPosL(jugador.getPos());
            tr2.setPosL(jugador.getPos());
            tk1.setPosL(jugador.getPos());
            tk2.setPosL(jugador.getPos());
            k1.setPosL(jugador.getPos());
            k2.setPosL(jugador.getPos());
            tr1.Visto(jugador.getArea());
            tr2.Visto(jugador.getArea());
            jugador.Herir(tr1.getArea());
            jugador.Herir(tr2.getArea());
            LimitarLink();
            Herir();
            Matar();
            base.Update(gameTime);
        }
        public void Herir()
        {
            if (tk1.Enabled)
                jugador.Herir(tk1.getArea());
            if (tk2.Enabled)
                jugador.Herir(tk2.getArea());
            if (k1.Enabled)
                jugador.Herir(k1.getArea());
            if (k2.Enabled)
                jugador.Herir(k2.getArea());
        }
        public void Matar()
        {
            if (tk1.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                tk1.Enabled = false;
                Componentes.Remove(tk1);
            }
            if (tk2.Destruido(jugador.GetEspadaArea()) && jugador.getAtaque())
            {
                tk2.Enabled = false;
                Componentes.Remove(tk2);
            }
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