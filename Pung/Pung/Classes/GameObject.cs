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
    public class GameObject : DrawableGameComponent
    {

        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle objectRectangle;

        protected Game gameRef;
        protected SpriteBatch spriteBatch;


        #region Properties

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Game GameRef
        {
            get { return gameRef; }
            set { gameRef = value; }
        }
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }
        public Rectangle ObjectRectangle
        {
            get { return objectRectangle; }
            set { objectRectangle = value; }
        }

        #endregion


        public GameObject(PungGame game)
            : base(game)
        {
            gameRef = game; // Not sure if this is good practice, may not be needed. Replaceable by Game ?
            
            
            position = new Vector2();

            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

        }

        protected void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            
            texture = theContentManager.Load<Texture2D>(theAssetName);
            //objectRectangle = texture.Bounds;
            objectRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Color.White);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            objectRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            base.Update(gameTime);
        }

        public List<Group> GetGroupsContainingThisGameObject()
        {
            List<Group> GroupsContainingMe = new List<Group>();

            GroupList groups = (GroupList)Game.Services.GetService(typeof(GroupList));
            foreach (Group item in groups)
            {
                if (item.Contains(this))
                {
                    GroupsContainingMe.Add(item);
                }
            }

            return GroupsContainingMe;

        }



    }
}
