using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerControls.IGameplayActions
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCancelEvent = delegate { };

    private PlayerControls _playerControls;

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.Gameplay.SetCallbacks(this);
            _playerControls.Gameplay.Enable();

            Debug.Log("Input Reader Set Up and Ready!");
        }
    }

    private void OnDisable()
    {
        if (_playerControls != null)
        {
            DisableAllInput();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            JumpEvent.Invoke();

        if (context.phase == InputActionPhase.Canceled)
            JumpCancelEvent.Invoke();
    }

    public void EnableGameplayInput()
    {
        _playerControls.Gameplay.Enable();
    }

    public void DisableAllInput()
    {
        _playerControls.Gameplay.Disable();
    }
}