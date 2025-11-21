using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttack", menuName = "Delt-aurom/Enemy Attack", order = 4)]
public class EnemyAttack : ScriptableObject
{
    public string attackName;
    public float baseDuration;
    public float damage;
    public GameObject behaviorPrefab;
}
