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


namespace Tetris
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {

        private SpriteBatch sBatch;
        private SpriteFont font;
        MenuItem[] menuItem = new MenuItem[4];
        int selectedItem = 0;
        private Boolean iniciar = false, salir = false, instrucciones=false;
        private Texture2D fondoMenu;
        private Texture2D[] arrImagenes;

        public Boolean Salir
        {
            get { return salir; }
            set { salir = value; }
        }

        KeyboardState oldState;


        public Menu(Game game,Texture2D FM,Texture2D[] arrImg)
            : base(game)
        {
            sBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            font = (SpriteFont)game.Services.GetService(typeof(SpriteFont));
            Visible = false;
            Enabled = false;
            fondoMenu = FM;
            arrImagenes = arrImg;

            oldState = Keyboard.GetState();

            Color baseColor = Color.White;
            Color selectedColor = Color.GreenYellow;

            menuItem[0] = new MenuItem("Iniciar", "Iniciar Juego", font, new Vector2(50f, 100f), baseColor, selectedColor, true,arrImagenes[0]);
            menuItem[1] = new MenuItem("Instrucciones", "Instrucciones", font, new Vector2(50f, 150f), baseColor, selectedColor, false, arrImagenes[1]);
            menuItem[2] = new MenuItem("Puntajes", "Puntajes", font, new Vector2(50f, 200f), baseColor, selectedColor, false, arrImagenes[2]);
            menuItem[3] = new MenuItem("Exit", "Salir Juego", font, new Vector2(50f, 300f), baseColor, selectedColor, false, arrImagenes[3]);

        }

        public Boolean Iniciar
        {
            get { return iniciar; }
            set { iniciar = value; }
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
            for (int i = 0; i < menuItem.Length; i++)
            {
                menuItem[i].Selected = false;
            }

            KeyboardState kb = Keyboard.GetState();
            if (!instrucciones)
            {
                if ((kb.IsKeyDown(Keys.Up)) && (oldState.IsKeyUp(Keys.Up)))
                {
                    selectedItem -= 1;
                    if (selectedItem == -1)
                    {
                        selectedItem = menuItem.Length - 1;
                    }
                }

                if ((kb.IsKeyDown(Keys.Down)) && (oldState.IsKeyUp(Keys.Down)))
                {
                    selectedItem += 1;
                    if (selectedItem == menuItem.Length)
                    {
                        selectedItem = 0;
                    }
                }
            }

            if ((kb.IsKeyDown(Keys.Space)) && (oldState.IsKeyUp(Keys.Space)))
            {
                if (menuItem[selectedItem].Name == "Iniciar")
                {
                    //Exit();
                    iniciar = true;
                }
                if (menuItem[selectedItem].Name == "Exit")
                {
                    //Exit();
                    salir = true;
                }
                if (menuItem[selectedItem].Name == "Instrucciones")
                {
                    if (!instrucciones)
                    {
                        instrucciones = true;
                    }
                    else
                    {
                        instrucciones = false;
                    }
                }
            }

            menuItem[selectedItem].Selected = true;

            oldState = kb;

            base.Update(gameTime);
        }

        

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                sBatch.Draw(fondoMenu, new Vector2(0, 0), Color.LightBlue);
                if (!instrucciones)
                {
                    for (int i = 0; i < menuItem.Length; i++)
                    {
                        menuItem[i].Draw(sBatch);
                    }

                    sBatch.DrawString(font, menuItem[selectedItem].Name + " seleccionado.", new Vector2(50f, 525f), Color.White);
                    sBatch.DrawString(font, "Precione SPACE para activar la seleccion.", new Vector2(50f, 550f), Color.White);
                }
                else
                {
                    sBatch.DrawString(font, "Mover Pieza:", new Vector2(50f, 100f), Color.Yellow);
                    sBatch.DrawString(font, "Izquerda, Derecha", new Vector2(300f, 100f), Color.Black);

                    sBatch.DrawString(font, "Rotar Pieza:", new Vector2(50f, 140f), Color.Yellow);
                    sBatch.DrawString(font, "A, S", new Vector2(300f, 140f), Color.Black);

                    sBatch.DrawString(font, "Pausa:", new Vector2(50f, 180f), Color.Yellow);
                    sBatch.DrawString(font, "P", new Vector2(300f, 180f), Color.Black);

                    sBatch.DrawString(font, "Precione SPACE para regresar.", new Vector2(50f, 550f), Color.Red);
                }
     
                
            }
            base.Draw(gameTime);
        }

        public void show()
        {
            Visible = true;
            Enabled = true;
        }
        public void hide()
        {
            Visible = false;
            Enabled = false;
        }

        public void Comenzar()
        {
            iniciar = false;
        }

    }//fin del la clase


    public class MenuItem
    {
        private string _name = "";
        private string _text = "";
        private SpriteFont _font;
        private Vector2 _position = Vector2.Zero;
        private Color _baseColor;
        private Color _selectedColor;
        private Texture2D imagen;

        private bool _selected = false;

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public MenuItem(string name, string text, SpriteFont font, Vector2 position, Color baseColor, Color selectedColor, bool selected,
            Texture2D img)
        {
            _name = name;
            _text = text;
            _font = font;
            _position = position;
            _baseColor = baseColor;
            _selectedColor = selectedColor;
            _selected = selected;
            imagen = img;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_selected)
            {
                //spriteBatch.DrawString(_font, _text, _position, _selectedColor);
                spriteBatch.Draw(imagen,_position,_selectedColor);
            }
            else
            {
                //spriteBatch.DrawString(_font, _text, _position, _baseColor);
                spriteBatch.Draw(imagen, _position, _baseColor);
            }
        }
    }
}