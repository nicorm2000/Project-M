using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerRB playerRB;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    private void OnEnable()
    {
        inputReader.JumpEvent += OnJumpInput;
    }

    private void OnDisable()
    {
        inputReader.JumpEvent -= OnJumpInput;
    }
    void OnJumpInput()
    {
        if (!playerRB.IsGrounded) return;
        playerRB.Jump(jumpForce);
    }
}
