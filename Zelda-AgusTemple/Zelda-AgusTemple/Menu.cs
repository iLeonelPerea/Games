using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteFont fuenteNormal, fuenteSeleccionada;
        protected Color colorNormal = Color.White, colorSeleccionado = Color.Red;
        protected Vector2 posicion;
        protected int selectedIndex = 0;
        protected readonly StringCollection menuItems;
        protected int height, width;
        protected AudioComponent audioComponent;
        KeyboardState oldKeyboardState;
        protected SpriteBatch spriteBatch=null;

        public Menu(Game game,SpriteFont regularFont, SpriteFont selectedFont)
            : base(game)
        {
            menuItems = new StringCollection();
            fuenteNormal = regularFont;
            fuenteSeleccionada = selectedFont;
            
            audioComponent = (AudioComponent)game.Services.GetService(typeof(AudioComponent));
            oldKeyboardState = Keyboard.GetState();
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
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
                //Sonido
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
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            
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