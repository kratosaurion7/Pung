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
        /* Random number generator used to determine a random location.
         * Isn't much good since the generation pattern is always similar.
         * For size try and reduce PungGame.TIME_UNTIL_BLOCK to 5ms and the blocks will always 
         * appear in the same 'wave'ish fashion.*/
        Random randomer = new Random(); 

        #region Properties
        // This space has been purposefully left blank. 
        #endregion

        public Block(PungGame game)
            : base(game)
        {
            // Start the block at a random position.
            position = new Vector2(randomer.Next(Game.Window.ClientBounds.Width), randomer.Next(Game.Window.ClientBounds.Height));

        }

        /// <summary>
        /// Constructor used to instance a block at a particular position rather than a random one.
        /// </summary>
        /// <param name="game">Reference to the main Game class.</param>
        /// <param name="startingPosition">Position to spawn the block.</param>
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {

            base.LoadContent(theContentManager, theAssetName);
        }

    }
}
