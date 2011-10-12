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
    public class Ball : GameObject, ICollidable
    {
        // Direction
        Vector2 direction;

        // Random number generator used to determine a starting direction.
        Random randomer = new Random();

        // Speed properties
        const float DEFAULTSPEED = 150;
        float speed = 150;

        // Limits of the window
        Rectangle screenBounds;

        #region Properties
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        #endregion

        public Ball(PungGame game)
            : base(game)
        {

            direction = new Vector2();



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

            // Check Collisions
            CheckCollisions();

            base.Update(gameTime);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            base.LoadContent(theContentManager, theAssetName);

        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

        /// <summary>
        /// Move the ball according to it's direction and speed.
        /// </summary>
        /// <param name="gameTime"></param>
        public void move(GameTime gameTime)
        {
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            position.X = screenBounds.Width / 2;
            position.Y = screenBounds.Height / 2;
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
            Vector2 randomPoint = new Vector2(randomer.Next(screenBounds.Width), randomer.Next(screenBounds.Height));
            Vector2 movement = randomPoint - position;

            if (movement != Vector2.Zero)
            {
                movement.Normalize(); // What if I normalized it first ?
                direction = movement;

            }

        }

        #region ICollidable Members

        public void CheckCollisions()
        {
            foreach (Block item in BlockBunch.Blocks)
            {
                if (this.objectRectangle.Intersects(item.ObjectRectangle))
                {
                    if (this.objectRectangle.Y + this.objectRectangle.Height >= item.ObjectRectangle.Y)
                    {
                        UpCollision(item);
                    }
                    if (this.objectRectangle.Y <= item.ObjectRectangle.Y + item.ObjectRectangle.Height)
                    {
                        DownCollision(item);
                    }
                    if (this.objectRectangle.X + this.objectRectangle.Width >= item.ObjectRectangle.X)
                    {
                        LeftCollision(item);
                    }
                    if (this.objectRectangle.X <= item.ObjectRectangle.X + item.ObjectRectangle.Width)
                    {
                        RightCollision(item);
                    }                    

                }
            }

        }

        public void UpCollision(GameObject target)
        {
                List<Group> GroupsContainingTarget = target.GetGroupsContainingThisGameObject();
                Group targetGroup = new Group();
                foreach (Group item in GroupsContainingTarget)
                {
                    if (item.Contains(target))
                    {
                        targetGroup = item; // TODO : That is a FUCKING long way to find out the Target's group. Let's put a variable somewhere.
                    }
                }

                switch (targetGroup.GroupName)
                {
                    case "Players":
                        direction *= new Vector2(1, -1);
                        IncrementSpeed();
                        break;
                    case "Collisions":
                        direction *= new Vector2(1, -1);
                        break;
                }
            }

        public void UpCollision()
        {
            direction *= new Vector2(1, -1);
            IncrementSpeed();
        }

        public void DownCollision(GameObject target)
        {
                List<Group> GroupsContainingTarget = target.GetGroupsContainingThisGameObject();
                Group targetGroup = new Group();
                foreach (Group item in GroupsContainingTarget)
                {
                    if (item.Contains(target))
                    {
                        targetGroup = item;
                    }
                }

                switch (targetGroup.GroupName)
                {
                    case "Players":
                        direction *= new Vector2(1, -1);
                        IncrementSpeed();
                        break;
                    case "Collisions":
                        direction *= new Vector2(1, -1);
                        break;
                }
            }

        public void DownCollision()
        {
            direction *= new Vector2(1, -1);
            IncrementSpeed();

        }

        public void LeftCollision(GameObject target)
        {
                List<Group> GroupsContainingTarget = target.GetGroupsContainingThisGameObject();
                Group targetGroup = new Group();
                foreach (Group item in GroupsContainingTarget)
                {
                    if (item.Contains(target))
                    {
                        targetGroup = item; // Works weirdly.
                    }
                }


                switch (targetGroup.GroupName)
                {
                    case "Players":
                        direction *= new Vector2(-1, 1);
                        IncrementSpeed();
                        break;
                    case "Collisions":
                        direction *= new Vector2(-1, 1);
                        break;
                }
            }

        public void Leftcollision()
        {
            direction *= new Vector2(-1, 1);
            IncrementSpeed();

        }

        public void RightCollision(GameObject target)
        {
                List<Group> GroupsContainingTarget = target.GetGroupsContainingThisGameObject();
                Group targetGroup = new Group();
                foreach (Group item in GroupsContainingTarget)
                {
                    if (item.Contains(target))
                    {
                        targetGroup = item;
                    }
                }


                switch (targetGroup.GroupName)
                {
                    case "Players":
                        direction *= new Vector2(-1, 1);
                        IncrementSpeed();
                        break;
                    case "Collisions":
                        direction *= new Vector2(-1, 1);
                        break;
                }
            }

        public void RightCollision()
        {
            direction *= new Vector2(-1, 1);
            IncrementSpeed();

        }

        #endregion
    }

}
