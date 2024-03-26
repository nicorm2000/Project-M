using UnityEngine;
using Utilities;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerRB _rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private float _movementInput;

    private void OnMoveInput(Vector2 vector) => _movementInput = vector.y;

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMoveInput;
        
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMoveInput;
    }
    private void FixedUpdate()
    {
        _rb.AddMove(_movementInput * moveSpeed * Time.fixedDeltaTime);

    }
}
