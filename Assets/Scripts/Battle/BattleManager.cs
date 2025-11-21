using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BattleState {
    BattleTurnStart,
    PlayerAction,
    EnemyAction,
}

public class BattleManager : MonoBehaviour
{
    [Header("Battle Menu")]
    [SerializeField] public PlayerData[] playableCharacters;
    [SerializeField] public GameObject menuPrefab;
    [SerializeField] public GameObject menusStorage;

    [Header("Player")]
    [SerializeField] public Controls controls;
    [SerializeField] public SoulManager soulManager;

    [Header("Enemy")]
    [SerializeField] public Playground playground;
    [SerializeField] public EnemyData enemyData;
    [SerializeField] public GameObject enemyStorage;

    // UI-related
    private CharacterMenu[] menus = new CharacterMenu[3];
    private int currentMenuIndex = -1;

    // Battle-related
    public event Action<BattleState> OnStateChanged;
    private BattleState currentState = BattleState.BattleTurnStart;
    public BattleState CurrentState => currentState;
    private int turn = 0;
    public int Turn => turn;

    private void Start() {
        // Menu setup
        foreach (Transform child in menusStorage.transform) { Destroy(child.gameObject); }
        for (int i = 0; i < playableCharacters.Length; i++) {
            GameObject menuObject = Instantiate(menuPrefab, menusStorage.transform);
            CharacterMenu menu = menuObject.GetComponent<CharacterMenu>();
            menu.playerData = playableCharacters[i];
            menus[i] = menu;
            menus[i].SetMainCharacter(i == 0);
        }

        SetState(BattleState.BattleTurnStart);
    }

    private void Update() {
        switch (currentState) { 
            case BattleState.BattleTurnStart:
                currentMenuIndex = 0;
                foreach(CharacterMenu menu in menus) { menu.Reset(); }
                menus[0].ActivateMenu();
                SetState(BattleState.PlayerAction);
                break;
            case BattleState.PlayerAction:
                if (menus[currentMenuIndex].HasInput)
                {
                    currentMenuIndex++;
                }

                if (currentMenuIndex >= menus.Length)
                {
                    ProcessInput();
                    GetCurrentEnemyAttack().StartAttack(enemyData.GetAttackOfTurn(turn), this);
                    SetState(BattleState.EnemyAction);
                }
                break;
            case BattleState.EnemyAction:
                AttackBehavior attackBehavior = GetCurrentEnemyAttack();

                if (attackBehavior.IsFinished())
                {
                    attackBehavior.EndAttack();
                    SetState(BattleState.BattleTurnStart);
                    turn++;
                } else
                {
                    attackBehavior.UpdateAttack();
                }
                break;
            default:
                break;
        }
    }

    // --------------------------------------------------------------------------------------------

    public AttackBehavior GetCurrentEnemyAttack()
    {
        GameObject prefab = enemyData.attacks[turn].behaviorPrefab;
        return prefab.GetComponent<AttackBehavior>();
    }

    // --------------------------------------------------------------------------------------------

    public void SetState(BattleState newState)
    {
        if (newState == currentState)
            return;

        currentState = newState;
        OnStateChanged?.Invoke(newState);

        Debug.Log("New state! " +  newState);
    }

    public void OnCurrentMoveIndexChange()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == currentMenuIndex)
            {
                menus[i].ActivateMenu();
            } else
            {
                menus[i].DeactivateMenu();
            }
        }
    }

    // --------------------------------------------------------------------------------------------

    #region MenuInput

    public void OnMenuMoveInput(Vector2 input)
    {
        menus[currentMenuIndex].HandleMoveInput(input);
    }

    public void OnInputMenuValidate(InputAction.CallbackContext ctx)
    {
        if (0 <= currentMenuIndex && currentMenuIndex < menus.Length)
        {
            if(menus[currentMenuIndex].Validate(ref currentMenuIndex))
            {
                OnCurrentMoveIndexChange();
            }
        }
    }

    public void OnInputMenuCancel(InputAction.CallbackContext ctx)
    {
        if (0 <= currentMenuIndex && currentMenuIndex < menus.Length)
        {
            if (menus[currentMenuIndex].Cancel(ref currentMenuIndex))
            {
                OnCurrentMoveIndexChange();
            }
        }
    }

    public void ProcessInput() {
        foreach (CharacterMenu menu in menus) { menu.DeactivateMenu(); }
    }

    #endregion
}
