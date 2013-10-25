using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace BarritaGirando.Core
{
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteFont fuenteNormal, fuenteSeleccionada;
        protected Color colorNormal = Color.White, colorSeleccionado = Color.Red;
        protected Vector2 posicion;
        protected int selectedIndex = 0;
        protected readonly List<String>menuItems;
        protected int height, width;
        protected AudioComponent audioComponent;
        GamePadState oldGamePadState;
        KeyboardState oldKeyboardState;
        protected SpriteBatch spriteBatch=null;
        private Rectangle btnInicio, btnInstrucciones, btnPuntajes, btnSalir;
        private MouseState mouse, mouseAnterior;
        
        public Menu(Game game,SpriteFont regularFont, SpriteFont selectedFont)
            : base(game)
        {
            menuItems = new List<String>();
            fuenteNormal = regularFont;
            fuenteSeleccionada = selectedFont;
            audioComponent = (AudioComponent)game.Services.GetService(typeof(AudioComponent));
            oldKeyboardState = Keyboard.GetState();
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            btnInicio = new Rectangle(394, 303, 94, 24);
            btnInstrucciones = new Rectangle(392, 342, 119, 24);
            btnPuntajes = new Rectangle(394, 370, 150, 24);
            btnSalir = new Rectangle(394, 400, 41, 24);
        }


        public void SetMenuItems(string[] items)
        {
            menuItems.Clear();
            menuItems.AddRange(items);
            CalculateBounds();
        }
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }
        public Color ColorNormal
        {
            get { return colorNormal; }
            set { colorNormal = value; }
        }
        public Color ColorSeleccionado
        {
            get { return colorSeleccionado; }
            set { colorSeleccionado = value; }
        }
        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }

        public void CalculateBounds() {
            width = 0;
            height = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = fuenteSeleccionada.MeasureString(item);
                if (size.X > width)
                {
                    width = (int)size.X;
                }
                height += fuenteSeleccionada.LineSpacing;
            }
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
            bool up, down;
            KeyboardState keyboardState = Keyboard.GetState();

            down = (oldKeyboardState.IsKeyDown(Keys.Down) &&
                (keyboardState.IsKeyUp(Keys.Down)));

            up = (oldKeyboardState.IsKeyDown(Keys.Up) &&
                (keyboardState.IsKeyUp(Keys.Up)));

            if (up || down) {
                audioComponent.PlayCue("menuChange");
            }
            if (up) {
                selectedIndex--;
                if (selectedIndex < 0) {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            if (down) {
                selectedIndex++;
                if (selectedIndex >= menuItems.Count) {
                    selectedIndex = 0;
                }
            }
            oldKeyboardState = keyboardState;

            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            down = (oldGamePadState.IsButtonDown(Buttons.DPadDown) &&
                gamePadState.IsButtonUp(Buttons.DPadDown));

            up = (oldGamePadState.IsButtonDown(Buttons.DPadUp) &&
                gamePadState.IsButtonUp(Buttons.DPadUp));

            if (up || down)
            {
                audioComponent.PlayCue("menuChange");
            }
            if (up)
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            if (down)
            {
                selectedIndex++;
                if (selectedIndex >= menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            oldGamePadState = gamePadState;

            manejaMouse();
            base.Update(gameTime);
        }

        public void manejaMouse()
        {
            mouse = Mouse.GetState();
            Rectangle rectRaton = new Rectangle(mouse.X - 1, mouse.Y - 1, 2, 2);
            if (rectRaton.Intersects(btnInicio))
            {
                selectedIndex = 0;
            }
            else if (rectRaton.Intersects(btnInstrucciones))
            {
                selectedIndex = 1;
            }
            else if (rectRaton.Intersects(btnPuntajes))
            {
                selectedIndex = 2;
            }
            else if (rectRaton.Intersects(btnSalir))
            {
                selectedIndex = 3;
            }
            mouseAnterior = mouse;
            
        }

        public override void Draw(GameTime gameTime)
        {
            posicion.X = 400;
            posicion.Y = 250;
            float y = posicion.Y;
            for (int i = 0; i < menuItems.Count; i++)
            {
                SpriteFont font;
                Color theColor;
                if (i == selectedIndex)
                {
                    font = fuenteSeleccionada;
                    theColor = colorSeleccionado;
                }
                else
                {
                    font = fuenteNormal;
                    theColor = colorNormal;
                }
                
                spriteBatch.DrawString(font, menuItems[i], new Vector2(posicion.X + 1, y + 1), Color.Black);
                // Draw the text item
                spriteBatch.DrawString(font, menuItems[i], new Vector2(posicion.X, y), theColor);
                y += font.LineSpacing;
            }
            base.Draw(gameTime);
        }
        
    }
}
