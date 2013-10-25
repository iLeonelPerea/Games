#region File Description
//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
    class GameOverMessageBoxScreen : GameScreen
    {
        #region Fields

        string message;
        Texture2D gradientTexture;
        KeyboardState previousKeyboardState = Keyboard.GetState();

        #endregion

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;
        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor automatically includes the standard "A=ok, B=cancel"
        /// usage text prompt.
        /// </summary>
        public GameOverMessageBoxScreen(string message)
            : this(message, true)
        { }


        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public GameOverMessageBoxScreen(string message, bool includeUsageText)
        {
            const string usageText = "\nNombre:"; 
            
            if (includeUsageText)
                this.message = message + usageText;
            else
                this.message = message;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }


        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            gradientTexture = content.Load<Texture2D>("gradient");
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            // We pass in our ControllingPlayer, which may either be null (to
            // accept input from any player) or a specific index. If we pass a null
            // controlling player, the InputState helper returns to us which player
            // actually provided the input. We pass that through to our Accepted and
            // Cancelled events, so they can tell which player triggered them.
            KeyboardState teclado = Keyboard.GetState();
            if (!teclado.IsKeyDown(Keys.Enter))
            {
                escuchaTeclado(teclado);
            }
            else
            {
                if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
                {
                    // Raise the accepted event, then exit the message box.
                    if (Accepted != null)
                        Accepted(this, new PlayerIndexEventArgs(playerIndex));

                    ExitScreen();
                }
                else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
                {
                    // Raise the cancelled event, then exit the message box.
                    if (Cancelled != null)
                        Cancelled(this, new PlayerIndexEventArgs(playerIndex));

                    ExitScreen();
                }
            }
        }

        public void escuchaTeclado(KeyboardState teclado) {
            Keys[] teclas = teclado.GetPressedKeys();
            if (teclado.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                message += " ";
            if (teclado.IsKeyDown(Keys.A)&&previousKeyboardState.IsKeyUp(Keys.A))
                message += "A";
            if (teclado.IsKeyDown(Keys.B) &&previousKeyboardState.IsKeyUp(Keys.B))
                message += "B";
            if (teclado.IsKeyDown(Keys.C) && previousKeyboardState.IsKeyUp(Keys.C))
                message += "C";
            if (teclado.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
                message += "D";
            if (teclado.IsKeyDown(Keys.E) && previousKeyboardState.IsKeyUp(Keys.E))
                message += "E";
            if (teclado.IsKeyDown(Keys.F) && previousKeyboardState.IsKeyUp(Keys.F))
                message += "F";
            if (teclado.IsKeyDown(Keys.G) && previousKeyboardState.IsKeyUp(Keys.G))
                message += "G";
            if (teclado.IsKeyDown(Keys.H) && previousKeyboardState.IsKeyUp(Keys.H))
                message += "H";
            if (teclado.IsKeyDown(Keys.I) && previousKeyboardState.IsKeyUp(Keys.I))
                message += "I";
            if (teclado.IsKeyDown(Keys.J) && previousKeyboardState.IsKeyUp(Keys.J))
                message += "J";
            if (teclado.IsKeyDown(Keys.K) && previousKeyboardState.IsKeyUp(Keys.K))
                message += "K";
            if (teclado.IsKeyDown(Keys.L) && previousKeyboardState.IsKeyUp(Keys.L))
                message += "L";
            if (teclado.IsKeyDown(Keys.M) && previousKeyboardState.IsKeyUp(Keys.M))
                message += "M";
            if (teclado.IsKeyDown(Keys.N) && previousKeyboardState.IsKeyUp(Keys.N))
                message += "N";
            if (teclado.IsKeyDown(Keys.O) && previousKeyboardState.IsKeyUp(Keys.O))
                message += "O";
            if (teclado.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
                message += "P";
            if (teclado.IsKeyDown(Keys.Q) && previousKeyboardState.IsKeyUp(Keys.Q))
                message += "Q";
            if (teclado.IsKeyDown(Keys.R) && previousKeyboardState.IsKeyUp(Keys.R))
                message += "R";
            if (teclado.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S))
                message += "S";
            if (teclado.IsKeyDown(Keys.T) && previousKeyboardState.IsKeyUp(Keys.T))
                message += "T";
            if (teclado.IsKeyDown(Keys.U) && previousKeyboardState.IsKeyUp(Keys.U))
                message += "U";
            if (teclado.IsKeyDown(Keys.V) && previousKeyboardState.IsKeyUp(Keys.V))
                message += "V";
            if (teclado.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W))
                message += "W";
            if (teclado.IsKeyDown(Keys.X) && previousKeyboardState.IsKeyUp(Keys.X))
                message += "X";
            if (teclado.IsKeyDown(Keys.Y) && previousKeyboardState.IsKeyUp(Keys.Y))
                message += "Y";
            if (teclado.IsKeyDown(Keys.Z) && previousKeyboardState.IsKeyUp(Keys.Z))
                message += "Z";
            previousKeyboardState = Keyboard.GetState();
        }

        #endregion

        #region Draw


        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            // Fade the popup alpha during transitions.
            Color color = new Color(255, 255, 255, TransitionAlpha);

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(gradientTexture, backgroundRectangle, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, color);

            spriteBatch.End();
        }


        #endregion
    }
}
