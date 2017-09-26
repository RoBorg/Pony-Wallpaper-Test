using UnityEngine;

namespace RoBorg.Pegasi
{
    public static class HelperExtensions
    {
        public static Transform FindInChildren(this Transform self, string name)
        {
            var count = self.childCount;

            for (int i = 0; i < count; i++)
            {
                var child = self.GetChild(i);

                if (child.name == name)
                {
                    return child;
                }

                var subChild = child.FindInChildren(name);

                if (subChild != null)
                {
                    return subChild;
                }
            }

            return null;
        }

        public static GameObject FindInChildren(this GameObject self, string name)
        {
            var child = self.transform.FindInChildren(name);

            return child == null ? null : child.gameObject;
        }
    }
}
