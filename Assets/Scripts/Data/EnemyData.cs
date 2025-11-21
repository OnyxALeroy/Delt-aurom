using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Enemy", menuName = "Delt-aurom/Enemy", order = 2)]
public class EnemyData : ScriptableObject
{
    public Sprite enemySprite;
    public string enemyName = "Kris";
    public int maxHp = 123;
    public List<EnemyAttack> attacks = new List<EnemyAttack>();

    public EnemyAttack GetAttackOfTurn(int turn) {  return attacks[turn % attacks.Count]; }
}
