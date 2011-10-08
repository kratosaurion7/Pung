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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ball : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Game gameRef; // Reference to the game
        SpriteBatch spriteBatch;

        Texture2D ballTexture;
        Vector2 ballPosition;
        Vector2 ballDirection;

        Rectangle ballRectangle;

        Random randomer = new Random();

        const float DEFAULTSPEED = 150;
        float speed = 150;


        Rectangle screenBounds;

        #region Properties
        public Vector2 Position
        {
            get { return ballPosition; }
            set { ballPosition = value; }
        }
        public Rectangle Rectangle
        {
            get { return ballRectangle; }
            set { ballRectangle = value; }
        }
        public Vector2 Direction
        {
            get { return ballDirection; }
            set { ballDirection = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }


        #endregion

        public Ball(Game game)
            : base(game)
        {
            gameRef = game; // Not sure if this is good practice, may not be needed. Replaceable by Game ?

            ballPosition = new Vector2();
            ballDirection = new Vector2();

            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            

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

            move(gameTime);

            // Refresh the rectangle info's for the current position
            ballRectangle = new Rectangle((int)ballPosition.X, (int)ballPosition.Y, ballTexture.Width, ballTexture.Height);
            base.Update(gameTime);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            ballTexture = theContentManager.Load<Texture2D>(theAssetName);
            
            
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Draw(ballTexture, ballPosition, Color.White);
            

            base.Draw(gameTime);
        }

        /// <summary>
        /// Move the ball according to it's direction and speed.
        /// </summary>
        /// <param name="gameTime"></param>
        public void move(GameTime gameTime)
        {
            ballPosition += ballDirection * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Place the ball in it's default position
        /// </summary>
        /// <param name="screenBounds">Rectangle representing the size of the window in which the pallet may move.</param>
        /// <remarks>
        /// This is the only place where screenBounds is given, since there may come a situation when we want to
        /// spawn the ball anywhere else than it's starting position (thus, not calling this procedure) we have
        /// no way of knowing the screen bounds leading to unexpected results.
        /// </remarks>
        public void placeInDefaultPosition(Rectangle screenBounds)
        {
            this.screenBounds = screenBounds;

            // Middle of the screen, centered for the ball.
            ballPosition.X = screenBounds.Width / 2;
            ballPosition.Y = screenBounds.Height / 2;
            speed = DEFAULTSPEED; // Reset the speed
            GiveInitialImpulse(); // Send a ball in a random direction.
        }

        /// <summary>
        /// Increment the speed of the ball by a default amound.
        /// </summary>
        internal void IncrementSpeed()
        {
            speed += speed * 0.2f; // 20% more speed
        }

        /// <summary>
        /// Increment the speed by a specified amount.
        /// </summary>
        /// <param name="amount">Float percentage of speed to increase the speed with.</param>
        internal void IncrementSpeed(float amount)
        {
            speed *= amount;
        }
        
        /// <summary>
        /// Give an impulse to the ball toward a random direction.
        /// </summary>
        private void GiveInitialImpulse()
        {
            // Find an initial direction based on random values.
            Vector2 randomPoint = new Vector2(randomer.Next(screenBounds.Width),randomer.Next(screenBounds.Height));
            Vector2 movement = randomPoint - ballPosition;

            if (movement != Vector2.Zero)
            {
                movement.Normalize(); // What if I normalized it first ?
                ballDirection = movement;
                
            }

        }
    }
}
