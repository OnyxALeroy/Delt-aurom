using UnityEngine;

public static class SpriteHelper
{
    public static GameObject CreateSpriteChild(
        GameObject parent,
        Sprite sprite,
        float x = 0, float y = 0,
        string sortingLayer = "Default",
        int orderInLayer = 0)
    {
        GameObject obj = new GameObject("SpriteChild");
        obj.transform.SetParent(parent.transform, false); // local space
        obj.transform.localPosition = new Vector3(x, y, 0);

        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingLayerName = sortingLayer;
        sr.sortingOrder = orderInLayer;

        return obj;
    }
}
