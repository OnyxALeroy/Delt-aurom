using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    [SerializeField] private SoulManager soulManager;

    private InputAction moveAction;
    private Vector2 moveInput;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        soulManager.Move(moveInput);
    }
}
