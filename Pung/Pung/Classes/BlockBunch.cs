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
        static List<Block> blocks;

        public static List<Block> Blocks
        {
            get { return BlockBunch.blocks; }
            set { BlockBunch.blocks = value; }
        }

        Texture2D blockTexture;

        public BlockBunch(PungGame game)
            : base(game)
        {
            blocks = new List<Block>();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Block item in blocks)
            {
                item.Draw(gameTime);
            }
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            blockTexture = theContentManager.Load<Texture2D>(theAssetName);
            
        }

        public void addBlock(Block newBlock)
        {
            GroupList groupList = (GroupList)Game.Services.GetService(typeof(GroupList));
            Group collisionGroup = groupList.GetGroup("Collisions");
            collisionGroup.Add(newBlock);

            newBlock.LoadContent(Game.Content, "Block");
            blocks.Add(newBlock);
        }


        public void removeBlock(Block targetBlock)
        {
            blocks.Remove(targetBlock);
        }


    }
}
