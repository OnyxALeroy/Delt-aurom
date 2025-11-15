using UnityEngine;

public class SoulManager : MonoBehaviour
{
    [SerializeField] private Playground playground;
    [SerializeField] private GameObject soulObject;
    [SerializeField] private Soul[] souls;
    [SerializeField] public int soulId = 0;

    private SpriteRenderer soulSprite;

    void Start() {
        soulSprite = soulObject.GetComponent<SpriteRenderer>();
        Recenter();
    }

    void Update() {
        
    }

    // --------------------------------------------------------------------------------------------

    public void Recenter()
    {
        soulObject.transform.position = new Vector3(playground.centerX, playground.centerY, 0);
    }

    public void Move(Vector2 moveInput)
    {
        float speed = souls[soulId].speed;

        Vector3 displacement = new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime;
        Vector3 newPos = soulObject.transform.position + displacement;
        
        Vector2 halfSize = soulSprite.bounds.extents;
        newPos = playground.ClampPosition(newPos, halfSize);
        soulObject.transform.position = newPos;
    }
}
