using UnityEngine;

static class Utils
{
    public static Vector3 Snap(Vector3 v)
    {
        return new Vector3(
            (float)Mathf.RoundToInt(v.x),
            (float)Mathf.RoundToInt(v.y),
            (float)Mathf.RoundToInt(v.x));
    }
}
