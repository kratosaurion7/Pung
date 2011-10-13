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
    /// <summary>
    /// GameObject encapsulates DrawableGameComponent for the need to add additional properties fo the DGC classes.
    /// </summary>
    public class GameObject : DrawableGameComponent
    {
        // Graphical properties of the Object.
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle objectRectangle;

        // Services
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
        public Rectangle ObjectRectangle
        {
            get { return objectRectangle; }
            set { objectRectangle = value; }
        }

        #endregion

        public GameObject(PungGame game)
            : base(game)
        {            
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

        /// <summary>
        /// This method returns a list of groups that contains this instance of GameObject.
        /// </summary>
        /// <returns></returns>
        public List<Group> GetGroupsContainingThisGameObject()
        {
            List<Group> GroupsContainingMe = new List<Group>();

            GroupList groups = (GroupList)Game.Services.GetService(typeof(GroupList));
            foreach (Group item in groups)
            {// Looks through each groups and if they contain this object they will be returned.
                if (item.Contains(this))
                {
                    GroupsContainingMe.Add(item);
                }
            }

            return GroupsContainingMe;

        }

    }
}
