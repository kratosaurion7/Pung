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
