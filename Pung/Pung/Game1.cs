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

namespace Pung
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont scoreFont;

        // Players
        Player player1;
        Player player2;

        // THE one Ball
        Ball ball;

        // Keyboard States
        KeyboardState previousState;
        KeyboardState currentState;
        private const string VERSION_NUMBER = "v0.1";

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.Title = "Pung " + VERSION_NUMBER;

            // Setting the window size
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {


            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Publish the service, allowing everything to use the spriteBatch
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            player1 = new Player(this, Pallet.PlayerNumber.PlayerOne);
            player1.Name = "Player 1";
            player2 = new Player(this, Pallet.PlayerNumber.PlayerTwo);
            player2.Name = "Player 2";

            ball = new Ball(this);



            base.Initialize();

            // Placed after general initialisation so the texture is loaded and its size initialized
            player1.Pallet.placeInDefaultPosition(Window.ClientBounds);
            player2.Pallet.placeInDefaultPosition(Window.ClientBounds);

            ball.placeInDefaultPosition(Window.ClientBounds);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            // Load the content of the actors
            player1.Pallet.LoadContent(this.Content, "Pallet");
            player2.Pallet.LoadContent(this.Content, "Pallet");
            ball.LoadContent(this.Content, "Ball");

            // Load the fonts
            scoreFont = this.Content.Load<SpriteFont>("scoreFont");

            // Add the components to the main list.
            Components.Add(player1.Pallet);
            Components.Add(player2.Pallet);
            Components.Add(ball);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currentState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ball.placeInDefaultPosition(Window.ClientBounds);

            }

            CheckCollisions();

            // TODO: Add your update logic here

            base.Update(gameTime);

            previousState = currentState;

        }

        private void CheckCollisions()
        {
            // Check for the ball against the edges of the screen
            if (ball.Position.Y + ball.Rectangle.Height > Window.ClientBounds.Height) // Bottom of the screen
            {
                //TODO : Implement a procedure, duplicate code
                ball.Direction *= new Vector2(1, -1);
                ball.IncrementSpeed();
            }
            else if (ball.Position.Y < 0) // Top of the screen
            {
                ball.Direction *= new Vector2(1, -1);
                ball.IncrementSpeed();
            }
            if (ball.Position.X + ball.Rectangle.Width > Window.ClientBounds.Width) // Right of the screen
            {
                // Player 1 scores
                player1.AddScore();
                player1.Pallet.DecrementSpeed();
                ball.placeInDefaultPosition(Window.ClientBounds);
            }
            else if (ball.Position.X < 0) // Left of the screen
            {
                // Player 2 scores
                player2.AddScore();
                player2.Pallet.DecrementSpeed();
                ball.placeInDefaultPosition(Window.ClientBounds);
            }

            // Check for collisions against the pallets
            if (player1.Pallet.PalletRectangle.Intersects(ball.Rectangle))
            {
                ball.Direction *= new Vector2(-1, 1);
            }
            else if (player2.Pallet.PalletRectangle.Intersects(ball.Rectangle))
            {
                ball.Direction *= new Vector2(-1, 1);
            }


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            drawText();
            base.Draw(gameTime);

            spriteBatch.End();
        }

        private void drawText()
        {
            String scoresboard_string = "Scoreboard";
            
            String player1_string = player1.Name + ": " + player1.Score;
            String player2_string = player2.Name + ": " + player2.Score;

            Vector2 scoreboard_Position = new Vector2(Window.ClientBounds.Width / 2, 10);
            Vector2 positionOffset = new Vector2(0, 20);


            spriteBatch.DrawString(scoreFont, scoresboard_string, scoreboard_Position , Color.White);
            spriteBatch.DrawString(scoreFont, player1_string, scoreboard_Position + positionOffset, Color.White);
            spriteBatch.DrawString(scoreFont, player2_string, scoreboard_Position + (positionOffset * 2), Color.White);


        }
    }
}
