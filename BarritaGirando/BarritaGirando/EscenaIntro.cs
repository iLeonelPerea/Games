using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using BarritaGirando.Core;


namespace BarritaGirando
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EscenaIntro : Escena
    {

        private SpriteBatch sBatch;
        private Menu menu;
        private Texture2D fondo, imgCrece;
        private Barra[] barras;
        private Rectangle pantalla, rImagen;
        private AudioComponent audioComponent;
        private float angulo;

        public EscenaIntro(Game game, ref SpriteFont fn, ref SpriteFont fs, ref Texture2D fondo, ref Texture2D imgCrece,
            ref Texture2D imgBarra)
            : base(game)
        {
            sBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            menu = new Menu (game, fn, fs);
            string[] menuItems = { "Iniciar", "Instrucciones","Mejores puntajes", "Salir" };
            menu.SetMenuItems(menuItems);
            menu.Posicion = new Vector2(game.Window.ClientBounds.Width/2 - menu.Width/2, game.Window.ClientBounds.Height/2 - menu.Height/2);
            audioComponent = (AudioComponent)game.Services.GetService(typeof(AudioComponent));
            this.fondo = fondo;
            this.imgCrece = imgCrece;
            barras = new Barra[3];
            barras[0] = new Barra(game, imgBarra, 1, 1, 100, 290);
            barras[1] = new Barra(game, imgBarra, 1, 1, 860, 290);
            barras[2] = new Barra(game, imgBarra, 1, 1, 470, 565);
            pantalla = new Rectangle(0, 0, 1000, 650);
            Componentes.Add(menu);  
            // TODO: Construct any child components here
        }

        public int SelectedMenuIndex {
            get { return menu.SelectedIndex; }
        }

        public override void Show()
        {
            menu.Visible = false;
            menu.Enabled = false;
            rImagen = new Rectangle(pantalla.Width/2-15, pantalla.Height/2 -6, 30, 12);
            base.Show();
        }

        public void incrementaImagen() {
            rImagen.Width = rImagen.Width + 3;
            rImagen.Height = rImagen.Height + 1;
            rImagen.X = pantalla.Width / 2 - rImagen.Width / 2;
            rImagen.Y = pantalla.Height / 2 - rImagen.Height / 2 - 200;
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
            // TODO: Add your update code here
            if (!menu.Visible) {
                incrementaImagen();
                if (rImagen.Width >= 600) {
                    menu.Visible = true;
                    menu.Enabled = true;
                }
            }
            barras[0].angulo += 0.01f;
            barras[1].angulo += 0.03f;
            barras[2].angulo -= 0.07f;
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sBatch.Draw(fondo, new Vector2(0, 0), Color.White);
            sBatch.Draw(imgCrece, rImagen, Color.White);
            for (int i = 0; i < barras.Length; i++)
            {
                sBatch.Draw(barras[i].Imagen, barras[i].posicion, null, Color.White, barras[i].angulo, barras[i].origen, 1.0f, SpriteEffects.None, 1);
            }
            if (rImagen.Width >= 600) {
                //Animaciones de fin de intro
            }
            base.Draw(gameTime);
        }
    }
}