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
        public enum PlayerNumber
        {
            PlayerOne,
            PlayerTwo
        }

        SpriteBatch spriteBatch;

        Texture2D palletTexture;
        Vector2 palletPosition;
        Rectangle palletRectangle;

        const float DEFAULTSPEED = 450;
        float speed = 450;

        PlayerNumber playerIndex;

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

        public Pallet(Game game, PlayerNumber playerNumber)
            : base(game)
        {
            playerIndex = playerNumber;

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

        public void moveUp(GameTime gameTime)
        {
            if (palletPosition.Y >= 0 + speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
            {
                palletPosition.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
        }

        public void moveDown(GameTime gameTime)
        {
            if (palletPosition.Y + palletRectangle.Height <= screenBounds.Height - speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
            {
                palletPosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
        }

        public void IncrementSpeed()
        {
            speed += speed * 0.1f;
        }

        public void DecrementSpeed()
        {
            speed -= speed * 0.1f;
        }

        public void placeInDefaultPosition(Rectangle screenBounds)
        {
            this.screenBounds = screenBounds;

            int STARTING_HEIGHT_MIDPOINT = screenBounds.Height / 2 - palletRectangle.Height / 2;

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
