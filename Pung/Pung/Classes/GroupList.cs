using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pung
{
    /// <summary>
    /// This class manages a list of Groups.
    /// </summary>
    /// <remarks>
    /// This class is not well coded, I am working to remove it or code it differently.
    /// </remarks>
    class GroupList : List<Group>
    {
        /// <summary>
        /// Return a group identified by it's groupName.
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public Group GetGroup(string GroupName)
        {
            foreach (Group item in this)
            {// Goes through each of it's groups and if a name matches the corresponding group is returned.
                if (item.GroupName == GroupName)
                {
                    return item;
                }
            }
            return null;
        }

    }
}
