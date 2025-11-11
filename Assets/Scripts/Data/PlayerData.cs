using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Delt-aurom/Playable Character", order = 2)]
public class PlayerData : ScriptableObject
{
    public Sprite headSprite;
    public Color storedColor = new Color(0f, 255f, 255f);
    public string characterName = "Kris";
    public int maxHp = 123;

    public PlayerData() {}
}
