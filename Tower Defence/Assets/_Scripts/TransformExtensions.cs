using UnityEngine;

public static class TransformExtensions
{
    public static Transform FindChildWithTag(this Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
            Transform foundChild = child.FindChildWithTag(tag);
            if (foundChild != null)
            {
                return foundChild;
            }
        }
        return null;
    }
}
