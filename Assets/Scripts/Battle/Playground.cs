using UnityEngine;

public class Playground : MonoBehaviour
{
    [Header("Playground Bounds & Pos")]
    public float centerX;
    public float centerY;
    public float width;     // full width
    public float height;    // full height

    public float HalfWidth => width * 0.5f;
    public float HalfHeight => height * 0.5f;

    public float minX => centerX - HalfWidth;
    public float maxX => centerX + HalfWidth;
    public float minY => centerY - HalfHeight;
    public float maxY => centerY + HalfHeight;

    // --------------------------------------------------------------------------------------------

    public Vector3 ClampPosition(Vector3 position, Vector2 halfSize)
    {
        return new Vector3(
            Mathf.Clamp(position.x, minX + halfSize.x, maxX - halfSize.x),
            Mathf.Clamp(position.y, minY + halfSize.y, maxY - halfSize.y),
            position.z
        );
    }
}
