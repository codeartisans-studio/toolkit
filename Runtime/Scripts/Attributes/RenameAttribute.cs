using UnityEngine;

namespace Toolkit
{
    public class RenameAttribute : PropertyAttribute
    {
        public string name;

        public RenameAttribute(string name)
        {
            this.name = name;
        }
    }
}
