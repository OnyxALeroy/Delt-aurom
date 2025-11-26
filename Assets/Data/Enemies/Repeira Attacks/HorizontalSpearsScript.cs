using UnityEngine;
using System.Collections.Generic;

public class HorizontalSpearsScript : AttackBehavior
{
    private class SpearData
    {
        public GameObject obj;
        public bool facingRight;
        public float timeLeft;

        public SpearData(GameObject obj, bool facingRight, float duration)
        {
            this.obj = obj;
            this.facingRight = facingRight;
            this.timeLeft = duration;
        }
    }

    // ------------------------------------------------------------------------------------------------

    [SerializeField] public Sprite spearSprite;
    [SerializeField] public int spearAmount;
    [SerializeField] public float spearSpeed;
    [SerializeField] public float timeBetweenSpawns;
    [SerializeField] public float spearDuration;
    [SerializeField] private float xOffset = 5f;
    [SerializeField] private float spearScale = 2f;   // 1 = normal size

    private int currentSpearAmount;
    private float lastSpearTime;
    private float currentSpearTimeLeft;
    public float CurrentSpearTimeLeft => currentSpearTimeLeft;

    private GameObject storage;
    private float leftX, rightX;
    private float yMin, yMax;
    private readonly List<SpearData> spears = new List<SpearData>();

    public override void StartAttack(EnemyAttack attack, BattleManager mgr) {
        Playground playground = mgr.playground;
        storage = mgr.enemyStorage;

        // Reset values
        currentSpearAmount = 0;
        lastSpearTime = 0.0f;
        currentSpearTimeLeft = attack.baseDuration;

        // Set bounds for the spawn
        leftX = playground.minX - xOffset;
        rightX = playground.maxX + xOffset;
        yMin = playground.minY;
        yMax = playground.maxY;
    }

    public override void UpdateAttack() {
        // Update Time
        if (currentSpearTimeLeft > 0.0f) { currentSpearTimeLeft -= Time.deltaTime; }
        if (lastSpearTime > 0.0f) { lastSpearTime -= Time.deltaTime; }

        // Spear Spawn
        if (lastSpearTime <= 0.0f)
        {
            lastSpearTime = timeBetweenSpawns;
            currentSpearAmount++;

            bool isLeft = RandomHelper.Chance(0.5f);
            float yPos = RandomHelper.Float(yMin, yMax);
            SpawnSpear(isLeft ? leftX : rightX, yPos, isLeft);
        }

        // Update spear movement & expiration
        for (int i = spears.Count - 1; i >= 0; i--)
        {
            SpearData s = spears[i];
            float dir = s.facingRight ? 1f : -1f;
            s.obj.transform.position += new Vector3(spearSpeed * dir * Time.deltaTime, 0f, 0f);

            s.timeLeft -= Time.deltaTime;
            if (s.timeLeft <= 0f)
            {
                Destroy(s.obj);
                spears.RemoveAt(i);
            }
        }
    }

    public override bool IsFinished() {
        bool timerExceeded = currentSpearTimeLeft <= 0.0;
        bool allSpearsSpawned = currentSpearAmount >= spearAmount;
        return timerExceeded || allSpearsSpawned;
    }

    public override void EndAttack() {
        foreach (SpearData s in spears)
        {
            Destroy(s.obj);
            spears.Remove(s);
        }
    }

    #region Spawn

    private void SpawnSpear(float x, float y, bool facingRight)
    {
        GameObject spear = new GameObject("Spear");
        spear.transform.position = new Vector3(x, y, 0f);
        spear.transform.SetParent(storage.transform);
        spear.transform.localScale = new Vector3(spearScale, spearScale, 1f);
        SpriteRenderer sr = spear.AddComponent<SpriteRenderer>();
        sr.sprite = spearSprite;
        sr.sortingOrder = 10;

        if (!facingRight) { sr.flipX = true; }

        SpearData data = new SpearData(
            spear,
            facingRight,
            spearDuration
        );
        spears.Add(data);
    }

    #endregion
}
