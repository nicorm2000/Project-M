using UnityEngine;

public class PlayerRB : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private Rigidbody _rb;

    [Header("Jump")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float distanceToCheckGround;
    public bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, distanceToCheckGround, groundMask);

    public void Jump(float force)
    {
        _rb.AddForce(Vector3.up * force * _rb.mass, ForceMode.Impulse);
    }
    public void AddMove(float value)
    {
        Vector3 targetPosition = _rb.position + transform.forward * value;
        Vector3 lerpedPosition = Vector3.Lerp(_rb.position, targetPosition, 0.2f);

        _rb.MovePosition(lerpedPosition);
    }
    public void AddRotation(float value)
    {
        Quaternion targetRotation = _rb.rotation * Quaternion.Euler(new Vector3(0, value, 0));
        Quaternion slerpRotation = Quaternion.Slerp(_rb.rotation, targetRotation, 0.2f);
        _rb.MoveRotation(slerpRotation);
    }
}
