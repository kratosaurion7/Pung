using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pung
{
    class GroupList : List<Group>
    {
        public Group GetGroup(string GroupName)
        {
            foreach (Group item in this)
            {
                if (item.GroupName == GroupName)
                {
                    return item;
                }
            }
            return null;
        }

    }
}
