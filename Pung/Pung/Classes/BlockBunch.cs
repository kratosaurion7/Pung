using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pung
{
    class BlockBunch : GameObject
    {
        // List of all blocks spawned in the game. Available to every object.
        static List<Block> blocks;

        // Graphical components
        /* BlockBunch is given a texture because not all blocks will be carrying a texture.
         * Instead BlockBunch carries the texture and is applied to each children.
         */
        Texture2D blockTexture; 

        #region Properties

        public static List<Block> Blocks
        {
            get { return BlockBunch.blocks; }
            set { BlockBunch.blocks = value; }
        }
        #endregion
        // 


        public BlockBunch(PungGame game)
            : base(game)
        {
            blocks = new List<Block>();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Block item in blocks)
            {// Draws each block
                item.Draw(gameTime);
            }
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // BlockBunch cannot call his Base.Update because it updates the ObjectRectangle using the texture size. Which this object lack.
            // Solution : Make Blockbunch inherits from something else
            //base.Update(gameTime);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            blockTexture = theContentManager.Load<Texture2D>(theAssetName);
            
        }

        /// <summary>
        /// Add a block into the game.
        /// </summary>
        /// <param name="newBlock">Block to be added to the game.</param>
        /// <remarks>
        /// This procedure should be re-written since the group/grouplist structures 
        /// will be reworked.
        /// </remarks>
        public void addBlock(Block newBlock)
        {
            /* Get the group list service and get the group named Collisions.
             * take the new block and load it's texture then put it in the BlockBunch group then 
             * add the newBlock to the collisionGroup
             */
            GroupList groupList = (GroupList)Game.Services.GetService(typeof(GroupList));
            Group collisionGroup = groupList.GetGroup("Collisions");
            newBlock.LoadContent(Game.Content, "Block");
            blocks.Add(newBlock);
            collisionGroup.Add(newBlock);

        }

        /// <summary>
        /// Remove a block from the game.
        /// </summary>
        /// <param name="targetBlock">The block to be removed.</param>
        public void removeBlock(Block targetBlock)
        {
            // Get the groups the same way as addBlock but uses those objects to remove a block from the collections.
            GroupList groupList = (GroupList)Game.Services.GetService(typeof(GroupList));
            Group collisionGroup = groupList.GetGroup("Collisions");
            collisionGroup.Remove(targetBlock);
            blocks.Remove(targetBlock);

        }


    }
}
