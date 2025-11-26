using UnityEngine;
using System.Collections.Generic;

public class BattleRenderer : MonoBehaviour
{
    [Header("Sprite Positions")]
    [SerializeField] public float defaultX = 0;
    [SerializeField] public float defaultY = 0;

    private BattleManager battleManager;
    private List<GameObject> renderedObjects = new List<GameObject>();

    void Start()
    {
        battleManager = GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // --------------------------------------------------------------------------------------------

    #region Setup Battle

    public void SetupBattle()
    {
        // Initialize battle rendering components here
        renderedObjects.Add(SpriteHelper.CreateSpriteChild(
            this.battleManager.enemyStorage,
            this.battleManager.enemyData.enemySprite,
            defaultX, defaultY,
            "Enemy"));
    }

    public void UpdateRenderer()
    {
        // Update battle rendering components here
        Debug.Log("Battle Renderer: Updating battle visuals.");
    }

    public void OnDestroy()
    {
        // Cleanup battle rendering components here
        for (int i = renderedObjects.Count - 1; i >= 0; i--)
        {
            Destroy(renderedObjects[i]);
        }
        renderedObjects.Clear();
    }

    #endregion
}
