using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private bool isActive = false;

    [Header("UI Elements")]
    [SerializeField] private GameObject activeMenuRoot;   // Fight / Act / Item / Spare buttons
    [SerializeField] private GameObject passiveInfoRoot;  // HP bar, name, etc.

    [Header("Character Infos")]
    public PlayerData playerData;

    [Header("Active References")]
    [SerializeField] private TextDisplayer activeNameText;
    [SerializeField] private HpBar activeHpBar;
    [SerializeField] private Image activeHead;
    [SerializeField] private Image[] activeColorDependents;
    
    [Header("Passive References")]
    [SerializeField] private TextDisplayer passiveNameText;
    [SerializeField] private HpBar passiveHpBar;
    [SerializeField] private Image passiveHead;
    [SerializeField] private Image[] passiveColorDependents;

    void Start()
    {
        UpdateInfo(playerData.maxHp, playerData.maxHp);
        RefreshMenu();
    }

    void Update()
    {
        // FIXME: remove this later
        if (Input.GetKeyDown(KeyCode.M)) {
            ToggleMenu();   
        }
    }

    private void RefreshMenu()
    {
        if (activeMenuRoot != null) { activeMenuRoot.SetActive(isActive); }
        if (passiveInfoRoot != null) { passiveInfoRoot.SetActive(!isActive); }
    }

    public void ActivateMenu()
    {
        isActive = true;
        RefreshMenu();
    }

    public void DeactivateMenu()
    {
        isActive = false;
        RefreshMenu();
    }

    public void ToggleMenu()
    {
        isActive = !isActive;
        RefreshMenu();
    }

    public void UpdateInfo(int currentHp, int maxHp) {
        // Active
        if (activeColorDependents != null) {
            for (int i = 0; i < activeColorDependents.Length; i++)
            {
                if (activeColorDependents[i] == null) continue;
                activeColorDependents[i].color = playerData.storedColor;
            }
        }

        activeNameText.SetValue(playerData.characterName.ToUpper());
        activeHead.sprite = playerData.headSprite;
        activeHpBar.SetCurrentHP(currentHp);
        activeHpBar.SetMaxHP(maxHp);

        // Passive
        if (passiveColorDependents != null) {
            for (int i = 0; i < passiveColorDependents.Length; i++)
            {
                if (passiveColorDependents[i] == null) continue;
                passiveColorDependents[i].color = playerData.storedColor;
            }
        }

        passiveNameText.SetValue(playerData.characterName.ToUpper());
        passiveHead.sprite = playerData.headSprite;
        passiveHpBar.SetCurrentHP(currentHp);
        passiveHpBar.SetMaxHP(maxHp);
    }
}
