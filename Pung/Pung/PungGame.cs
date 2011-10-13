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
    public class PungGame : Microsoft.Xna.Framework.Game
    {
        // Graphical managers.
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // Fonts
        SpriteFont scoreFont;

        // Players
        Player player1;
        Player player2;

        // THE one Ball
        Ball ball;

        // Keyboard States
        KeyboardState previousState;
        KeyboardState currentState;

        // Game version
        private string VERSION_NUMBER = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        // Groups Definition
        Group collisionGroup;
        Group playerGroup;

        // Blocks
        BlockBunch BlockList;
        const int TIME_UNTIL_BLOCK = 5000;

        public PungGame()
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

            // Player classes.
            player1 = new Player(this, Pallet.PlayerNumber.PlayerOne);
            player1.Name = "Player 1";
            player2 = new Player(this, Pallet.PlayerNumber.PlayerTwo);
            player2.Name = "Player 2";

            // Ball classes.
            ball = new Ball(this);
            
            // Collision groups
            collisionGroup = new Group("Collisions");
            playerGroup = new Group("Players");

            playerGroup.Add(player1.Pallet);
            playerGroup.Add(player2.Pallet);

            // Groups
            GroupList GameGroups = new GroupList();
            GameGroups.Add(collisionGroup);
            GameGroups.Add(playerGroup);
            //Add the list of groups into the services so objects can retrieve info about items in specific groups
            Services.AddService(typeof(GroupList), GameGroups);

            // Blocks
            BlockList = new BlockBunch(this);

            // Will initialize the basic graphical objects.
            base.Initialize(); // Goes to LoadContent()

            // Placed after general initialisation so the texture is already loaded and its size initialized
            player1.Pallet.placeInDefaultPosition();
            player2.Pallet.placeInDefaultPosition();

            // Places the ball in the center position and give it a random angle.
            ball.placeInDefaultPosition();
            
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
            ball.LoadContent(this.Content, "RedBall");

            // Load the fonts
            scoreFont = this.Content.Load<SpriteFont>("scoreFont");

            // Add the components to the main list.
            Components.Add(player1.Pallet);
            Components.Add(player2.Pallet);
            Components.Add(ball);
            Components.Add(BlockList);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Record the current state of the keyboard. 
            currentState = Keyboard.GetState();

            // Check for user input.
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ball.placeInDefaultPosition();
            }

            // Add your update logic here
            
            // Update the other components of the game
            base.Update(gameTime);

            CheckBallCollisions();

            // Block generation code
            if (Math.Floor(gameTime.TotalGameTime.TotalMilliseconds) % TIME_UNTIL_BLOCK == 0 && gameTime.TotalGameTime.TotalMilliseconds > 0)
            {// If enough time has ellapsed the game will pop a new block.
                Block newBlock = new Block(this);
                BlockList.addBlock(newBlock);
            }
            
            // Records the current event as past so the program can know what what key was hit during the last update
            previousState = currentState;
        }

        /// <summary>
        /// This will test collisions on all objects that requires collision checking.
        /// </summary>
        /// <remarks>
        /// In the future this could be replaced as an enumeration through each member sharing the ICollidable interface.
        /// This procedure is not against blocks. Soon the code here will be distributed on each object so it can do
        /// the work by itself.
        /// </remarks>
        private void CheckBallCollisions()
        {
            // Check for the ball against the edges of the screen
            if (ball.Position.Y + ball.ObjectRectangle.Height > Window.ClientBounds.Height) // Bottom of the screen
            {
                ball.DownCollision(); // TODO : Rebound against the edges of the screen should be implemented in the Ball class.
                
            }
            else if (ball.Position.Y < 0) // Top of the screen
            {

                ball.UpCollision();
            }
            if (ball.Position.X + ball.ObjectRectangle.Width > Window.ClientBounds.Width) // Right of the screen
            {
                // Player 1 scores
                player1.AddScore();
                ball.placeInDefaultPosition();
            }
            else if (ball.Position.X < 0) // Left of the screen
            {
                // Player 2 scores
                player2.AddScore();
                ball.placeInDefaultPosition();
            }

            // Check for collisions against the pallets
            if (player1.Pallet.ObjectRectangle.Intersects(ball.ObjectRectangle))
            {
                ball.LeftCollision(player1.Pallet);
            }
            else if (player2.Pallet.ObjectRectangle.Intersects(ball.ObjectRectangle))
            {
                ball.RightCollision(player2.Pallet);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Add your drawing code here
            spriteBatch.Begin();

            base.Draw(gameTime);

            drawText();

            spriteBatch.End();
        }

        /// <summary>
        /// Draw the scoreboard on the screen.
        /// </summary>
        private void drawText()
        {
            // Text used in the scoreboard and scores and font fore color.
            String SCOREBOARD_STRING = "Scoreboard";
            String Player1_String = player1.Name + ": " + player1.Score;
            String Player2_String = player2.Name + ": " + player2.Score;
            Color SCOREBOARD_COLOR = Color.Yellow;

            // Create the position of the scoreboard and offset that subsequent lines will be spaced with.
            Vector2 scoreboard_Position = new Vector2(Window.ClientBounds.Width / 2, 10);
            Vector2 positionOffset = new Vector2(0, 20);

            // Draw each lines according to the starting position and offset.
            spriteBatch.DrawString(scoreFont, SCOREBOARD_STRING, scoreboard_Position, SCOREBOARD_COLOR);
            spriteBatch.DrawString(scoreFont, Player1_String, scoreboard_Position + positionOffset, SCOREBOARD_COLOR);
            spriteBatch.DrawString(scoreFont, Player2_String, scoreboard_Position + (positionOffset * 2), SCOREBOARD_COLOR);
            
        }
    }
}
