using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField] private InputReader inputReader;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundMask;

    [Header("Gliding")]
    [SerializeField] private float gliderDrag;
    [SerializeField] private float timeInAirBeforeDeploy;
    [SerializeField] private float distanceToGroundToDeploy;

    private Rigidbody _rb;

    private Vector2 _movementInput;
    private bool _jumpInput;

    private bool _isGrounded;
    private bool _gliderDeployed;
    private float _timeInAir;
    private float _initialDrag;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _initialDrag = _rb.drag;
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMove;
        inputReader.JumpEvent += OnJump;
        inputReader.JumpCancelEvent += OnJumpCanceled;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        inputReader.JumpEvent -= OnJump;
        inputReader.JumpCancelEvent -= OnJumpCanceled;
    }

    private void Update()
    {
        if (!_isGrounded)
        {
            _timeInAir += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGround();

        HandleMovement();

        if (_isGrounded)
            HandleJump();
        else
            HandleAir();
    }

    private void HandleMovement()
    {
        Vector3 targetPosition = _rb.position + transform.forward * _movementInput.y * moveSpeed * Time.fixedDeltaTime;
        Vector3 lerpedPosition = Vector3.Lerp(_rb.position, targetPosition, 0.2f);

        Quaternion targetRotation = _rb.rotation * Quaternion.Euler(new Vector3(0, _movementInput.x * turnSpeed * Time.fixedDeltaTime, 0));
        Quaternion slerpRotation = Quaternion.Slerp(_rb.rotation, targetRotation, 0.2f);

        _rb.MovePosition(lerpedPosition);
        _rb.MoveRotation(slerpRotation);
    }

    private void HandleJump()
    {
        if (_jumpInput)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            //Stay Grounded
        }
        _timeInAir = 0;
    }

    private void HandleAir()
    {
        if (_gliderDeployed)
        {

            if (_jumpInput)
            {
                //Gliding
            }
            else
            {
                RetractGlider();
            }
        }
        else
        {
            if (_jumpInput && CanDeploy())
            {
                DeployParachute();
            }
            else
            {
                //Fall
            }
        }
    }

    private void OnMove(Vector2 vector) => _movementInput = vector;

    private void OnJump() => _jumpInput = true;

    private void OnJumpCanceled() => _jumpInput = false;

    private bool CheckGround()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundMask);
    }

    private void DeployParachute()
    {
        _gliderDeployed = true;
        _rb.drag = gliderDrag;
    }

    private void RetractGlider()
    {
        _gliderDeployed = false;
        _rb.drag = _initialDrag;
    }

    private bool CanDeploy()
    {
        if (_timeInAir >= timeInAirBeforeDeploy)
        {
            RaycastHit hit;
            return !Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGroundToDeploy, groundMask);
        }
        return false;
    }
}