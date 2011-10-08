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

    public class Pallet : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /// <summary>
        /// Values representing a player number.
        /// </summary>
        public enum PlayerNumber
        {
            PlayerOne,
            PlayerTwo
        }

        // Graphical devices
        SpriteBatch spriteBatch;

        // Pallet graphical information
        Texture2D palletTexture;
        Vector2 palletPosition;
        Rectangle palletRectangle;

        // Speed information
        const float DEFAULTSPEED = 450;
        float speed = 450;

        // Player ownership of the pallet
        PlayerNumber playerIndex;

        // Limits of the windows in which the pallet must move.
        Rectangle screenBounds;

        #region Properties
        public Vector2 PalletPosition
        {
            get { return palletPosition; }
            set { palletPosition = value; }
        }

        public Rectangle PalletRectangle
        {
            get { return palletRectangle; }
            set { palletRectangle = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        #endregion

        /// <summary>
        /// Create a new instance of the Pallet class using the game reference and a player number.
        /// </summary>
        /// <param name="game">Reference to the game class.</param>
        /// <param name="playerNumber">Player number.</param>
        public Pallet(Game game, PlayerNumber playerNumber)
            : base(game)
        {
            playerIndex = playerNumber; // Associate the pallet to a player using it's player number.

            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            palletPosition = new Vector2();

        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            // Set the rectangle from the dimensions of the texture
            palletRectangle = new Rectangle((int)palletPosition.X, (int)palletPosition.Y, palletTexture.Width, palletTexture.Height);

            if (playerIndex == PlayerNumber.PlayerOne)
            {// Player 1 is pressing his keys
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    moveUp(gameTime);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    moveDown(gameTime);
                }

            }
            else if (playerIndex == PlayerNumber.PlayerTwo)
            {// Player 2 is pressing his keys
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    moveUp(gameTime);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    moveDown(gameTime);
                }

            }
            else
            {// The pallet is not associated with a player. Not supposed to get here
                throw new UnspecifiedPlayerException("Pallet not associated with any player.");
            }

            base.Update(gameTime);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            palletTexture = theContentManager.Load<Texture2D>(theAssetName);
            palletRectangle = palletTexture.Bounds;
            
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(palletTexture, palletPosition, Color.White);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Moves the pallet up the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public void moveUp(GameTime gameTime)
        {
            // Check to see if a mouvement would put the ball out of the screen's bounds
            if (palletPosition.Y >= 0 + speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
            {
                palletPosition.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
        }

        /// <summary>
        /// Moves the pallet down the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public void moveDown(GameTime gameTime)
        {
            // Check to see if a mouvement would put the ball out of the screen's bounds
            if (palletPosition.Y + palletRectangle.Height <= screenBounds.Height - speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
            {
                palletPosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
        }

        /// <summary>
        /// Speeds up the pallet.
        /// </summary>
        public void IncrementSpeed()
        {
            speed += speed * 0.1f;
        }

        /// <summary>
        /// Speeds down the pallet.
        /// </summary>
        public void DecrementSpeed()
        {
            speed -= speed * 0.1f;
        }

        /// <summary>
        /// Place the pallet in it's default position depending on it's player number.
        /// </summary>
        /// <param name="screenBounds">Rectangle representing the size of the window in which the pallet may move.</param>
        /// <remarks>
        /// This is the only place where screenBounds is given, since there may come a situation when we want to
        /// spawn the pallet anywhere else than it's starting position (thus, not calling this procedure) we have
        /// no way of knowing the screen bounds leading to unexpected results.
        /// </remarks>
        public void placeInDefaultPosition(Rectangle screenBounds)
        {
            this.screenBounds = screenBounds; // Associate the pallet's screenBound with the one given. TODO : Change this.

            // Half of the screen centered for the pallet.
            int STARTING_HEIGHT_MIDPOINT = screenBounds.Height / 2 - palletRectangle.Height / 2;

            // Depending on the player number he will spawn on one end of the screen or the other.
            if (playerIndex == PlayerNumber.PlayerOne)
            {
                palletPosition = new Vector2(20, STARTING_HEIGHT_MIDPOINT);
            }
            else if (playerIndex == PlayerNumber.PlayerTwo)
            {
                palletPosition = new Vector2(screenBounds.Width - palletRectangle.Width - 20, STARTING_HEIGHT_MIDPOINT);
            }
            else
            {
                throw new UnspecifiedPlayerException("Pallet is not associated with any player.");
            }
            
        }
    }




}
