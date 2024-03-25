using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [Header("Indicator")]
    [SerializeField] private GameObject interactIndicator = null;
    [SerializeField] private float interactDistance;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && IsWithinInteractDistance())
        {
            Interact();
        }

        if (interactIndicator.activeSelf && !IsWithinInteractDistance())
        {
            interactIndicator.SetActive(false);
        }
        else if (!interactIndicator.activeSelf && IsWithinInteractDistance())
        {
            interactIndicator.SetActive(true);
        }
    }

    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(_playerTransform.position, transform.position) < interactDistance)
            return true;
        else
            return false;
    }
}