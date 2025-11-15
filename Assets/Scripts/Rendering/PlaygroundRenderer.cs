using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlaygroundRenderer : MonoBehaviour
{
    [SerializeField] private Playground playground;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateSize();
    }

    public void UpdateSize()
    {
        float width = playground.maxX - playground.minX;
        float height = playground.maxY - playground.minY;

        // Set the size of the sliced sprite
        sr.drawMode = SpriteDrawMode.Sliced;
        sr.size = new Vector2(width, height);

        // Move to center of the bounds
        transform.position = new Vector3(playground.centerX, playground.centerY, 0);
    }
}
