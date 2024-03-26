using Unity.VisualScripting;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerRB _rb;
    [SerializeField] private float turnSpeed;

    private float turnInput;

    private void OnMoveInput(Vector2 vector) => turnInput = vector.x;

    private void FixedUpdate()
    {
        _rb.AddRotation(turnInput * Time.deltaTime * turnSpeed);
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMoveInput;

    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMoveInput;
    }
}
