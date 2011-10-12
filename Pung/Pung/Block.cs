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
    public class Block : GameObject
    {
        // Random number generator used to determine a random location.
        Random randomer = new Random();

        #region Properties


        #endregion


        public Block(PungGame game)
            : base(game)
        {

            position = new Vector2(randomer.Next(Game.Window.ClientBounds.Width), randomer.Next(Game.Window.ClientBounds.Height));

        }

        public Block(PungGame game, Vector2 startingPosition)
            : base(game)
        {
            position = startingPosition;

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {


            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            base.LoadContent(theContentManager, theAssetName);
            objectRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

    }
}
