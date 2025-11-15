using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private SoulManager soulManager;

    public InputAction cancelAction, moveAction, validateAction;
    private BattleState previousState;
    private Vector2 moveInput;
    private Vector2 lastMoveInput = new Vector2(0, 0);

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        moveAction.Enable();

        cancelAction = InputSystem.actions.FindAction("Cancel");
        validateAction = InputSystem.actions.FindAction("Validate");
        validateAction.performed += battleManager.OnInputMenuValidate;
        cancelAction.performed += battleManager.OnInputMenuCancel;
        validateAction.Enable();
        cancelAction.Enable();
    }

    private void Update()
    {
        if (battleManager.CurrentState == BattleState.EnemyAction)
        {
            soulManager.Move(lastMoveInput);
        }
    }

    private void OnEnable()
    {
        battleManager.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        battleManager.OnStateChanged -= HandleStateChanged;
    }

    // --------------------------------------------------------------------------------------------

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        lastMoveInput = ctx.ReadValue<Vector2>();

        // For continuous movement
        if (battleManager.CurrentState == BattleState.EnemyAction)
        {
            soulManager.Move(lastMoveInput);
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        // Used for menu movement
        if (battleManager.CurrentState == BattleState.PlayerAction)
        {
            battleManager.OnMenuMoveInput(lastMoveInput);
            lastMoveInput = new Vector2(0, 0);
        }
    }

    // --------------------------------------------------------------------------------------------

    private void HandleStateChanged(BattleState state)
    {
        switch (state)
        {
            case BattleState.EnemyAction:
                validateAction.Disable();
                cancelAction.Disable();
                break;

            default:
                validateAction.Enable();
                cancelAction.Enable();
                break;
        }
    }

}
