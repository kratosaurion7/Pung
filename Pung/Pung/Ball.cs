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
        const float MAX_SPEED = 1000;
        const float SPEED_MULTIPLIER = 0.2f;
        float speed = 150;

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
        public void placeInDefaultPosition()
        {
            // Middle of the screen, centered for the ball.
            position.X = Game.Window.ClientBounds.Width / 2;
            position.Y = Game.Window.ClientBounds.Height / 2;
            speed = DEFAULTSPEED; // Reset the speed
            GiveInitialImpulse(); // Send a ball in a random direction.
        }

        /// <summary>
        /// Increment the speed of the ball by a default amound.
        /// </summary>
        internal void IncrementSpeed()
        {
            if (speed + speed * SPEED_MULTIPLIER < MAX_SPEED)
            {
                speed += speed * SPEED_MULTIPLIER;
            }

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
            Vector2 randomPoint = new Vector2(randomer.Next(Game.Window.ClientBounds.Width), randomer.Next(Game.Window.ClientBounds.Height));
            Vector2 movement = randomPoint - position;

            if (movement != Vector2.Zero)
            {
                movement.Normalize(); // What if I normalized it first ?
                direction = movement;
            }

        }

        #region ICollidable Members

        /// <summary>
        /// Will check for collisions on the Ball.
        /// </summary>
        public void CheckCollisions()
        {
            foreach (Block item in BlockManager.Blocks)
            {// Check each block against the ball
                if (this.objectRectangle.Intersects(item.ObjectRectangle))
                {// Enters only if both the ball and the block intersects

                    /* Make the difference between the center of the ball and the center
                     * of the rectangle. 
                     * Substract them so the difference can be normalized and expressed
                     * as a direction and used on the ball. */
                    Vector2 BallCenter = this.position;
                    Vector2 BlockCenter = new Vector2(item.ObjectRectangle.Center.X, item.ObjectRectangle.Center.Y);

                    Vector2 Difference = BallCenter - BlockCenter;

                    Difference.Normalize();

                    this.direction = Difference;

                }
            }

        }

        /// <summary>
        /// Collision on the top of an object. Do NOT use this procedure.
        /// </summary>
        /// <param name="target">Object colliding with the ball.</param>
        /// <remarks>
        /// This procedure is not used for now but is kept as to be able to give the program ability to simulate a collision on a target
        /// at any time and without actual collision. If I wanted to simplify the procedure even further I could just change the parameter
        /// type from GameObject to target and rework the get-group block to work with rectangles instead since a rebound can be done with
        /// only the target's rectangle.
        /// 
        /// DO NOT USE THIS CODE, IT IS CRAP.
        /// </remarks>
        public void UpCollision(GameObject target)
        {
            // This block retrieve the groups of objects as to be able to execute specific actions depending on which group the 
            // target is member of.
            List<Group> GroupsContainingTarget = target.GetGroupsContainingThisGameObject();
            Group targetGroup = new Group();
            foreach (Group item in GroupsContainingTarget)
            {// Iterate through each object in the group that contains target to associate the target with a group.
                if (item.Contains(target))
                {
                    targetGroup = item; // TODO : That is a FUCKING long way to find out the Target's group. Let's put a variable somewhere.
                }
            }

            // Now depending on which group the target is in, will execute specific action.
            switch (targetGroup.GroupName)
            {
                case "Players":
                    direction *= new Vector2(1, -1);
                    IncrementSpeed();
                    break; // Will not work if the target is member of two groups since the break will make the code skip the other cases.
                case "Collisions":
                    direction *= new Vector2(1, -1);
                    break;
            }
        }
        /// <summary>
        /// Forcing an Up collision on the ball.
        /// </summary>
        public void UpCollision()
        {
            direction *= new Vector2(1, -1);
            IncrementSpeed();
        }

        /// <summary>
        /// Collision on the bottom of an object. Do NOT use this procedure.
        /// </summary>
        /// <param name="target">Object colliding with the ball.</param>
        /// <see cref="UpCollision"/>
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

        /// <summary>
        /// Forcing a Down collision on the object.
        /// </summary>
        public void DownCollision()
        {
            direction *= new Vector2(1, -1);
            IncrementSpeed();

        }

        /// <summary>
        /// Collision on the left of an object. Do NOT use this procedure.
        /// </summary>
        /// <param name="target">Object colliding with the ball.</param>
        /// <see cref="UpCollision"/>
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

        /// <summary>
        /// Forcing a Left collision on the ball.
        /// </summary>
        public void Leftcollision()
        {
            direction *= new Vector2(-1, 1);
            IncrementSpeed();

        }

        /// <summary>
        /// Collision on the right of an object. Do NOT use this procedure.
        /// </summary>
        /// <param name="target">Object colliding with the ball.</param>
        /// <see cref="UpCollision"/>
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

        /// <summary>
        /// Forcing a Right collision on the ball.
        /// </summary>
        public void RightCollision()
        {
            direction *= new Vector2(-1, 1);
            IncrementSpeed();

        }

        #endregion
    }

}
