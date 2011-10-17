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

        private double livingTime;

        private double maxLifeTime = Utilities.Randomizer.CreateRandom(10, 180);

        #region Properties

        public double LivingTime
        {
            get { return livingTime; }
            set { livingTime = value; }
        }

        public double MaxLifeTime
        {
            get { return maxLifeTime; }
            set { maxLifeTime = value; }
        }

        #endregion

        public Block(PungGame game) 
            : base(game)
        {
            // Create a new block without knowledge of the safe zone so it can be will be placed anywhere randomly.
            position = new Vector2(Utilities.Randomizer.CreateRandom(0, Game.Window.ClientBounds.Width),
                Utilities.Randomizer.CreateRandom(0, Game.Window.ClientBounds.Height));

        }

        public Block(PungGame game, Rectangle spawnZone)
            : base(game)
        {
            // Start the block at a random position within the block spawning zone.
            position = new Vector2(Utilities.Randomizer.CreateRandom(spawnZone.X, spawnZone.Width),
                Utilities.Randomizer.CreateRandom(spawnZone.Y, spawnZone.Height));   
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
            livingTime = 0;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            livingTime += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);

            // Record the time when the block was first drawn.
            
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {

            base.LoadContent(theContentManager, theAssetName);
        }

    }
}
