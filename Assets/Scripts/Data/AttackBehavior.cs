using UnityEngine;

public abstract class AttackBehavior : MonoBehaviour
{
    public abstract void StartAttack(EnemyAttack attack, BattleManager mgr);
    public abstract void UpdateAttack();
    public abstract bool IsFinished();
    public abstract void EndAttack();
}
