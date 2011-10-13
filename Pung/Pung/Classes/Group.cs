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
    /// Groups are containers of gameObjects designed to classify which object, if they do, belong to
    /// a group. This can be used for collisions for instance if the player has to rebound on certain
    /// surfaces and go through others.
    /// </summary>
    public class Group : List<GameObject>
    {
        string groupName;
        #region Properties
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        #endregion


        public Group() { }

        public Group(string GroupName)
        {
            groupName = GroupName;
        }

    }
}
