using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileInputReader : MonoBehaviour, IInputReader
{
    public event Action<Vector3> OnDirectionChanged;

    [Header("Swipe Settings")]
    [SerializeField] private float minSwipeDistance = 50f;

    private GameInput _inputActions;
    private Vector2 _touchStartPosition;
    private bool _isSwiping;

    private void Awake()
    {
        _inputActions = new GameInput();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.TouchContact.started += OnTouchStarted;
        _inputActions.Player.TouchContact.canceled += OnTouchCanceled;
    }

    private void OnDisable()
    {
        _inputActions.Player.TouchContact.started -= OnTouchStarted;
        _inputActions.Player.TouchContact.canceled -= OnTouchCanceled;
        _inputActions.Player.Disable();
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        _touchStartPosition = _inputActions.Player.PrimaryTouch.ReadValue<Vector2>();
        _isSwiping = true;
    }

    private void OnTouchCanceled(InputAction.CallbackContext context)
    {
        if (!_isSwiping) return;

        Vector2 touchEndPosition = _inputActions.Player.PrimaryTouch.ReadValue<Vector2>();
        Vector2 swipeVector = touchEndPosition - _touchStartPosition;

        if (swipeVector.magnitude >= minSwipeDistance)
        {
            AnalyzeSwipeDirection(swipeVector);
        }

        _isSwiping = false;
    }

    private void AnalyzeSwipeDirection(Vector2 swipeVector)
    {
        if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
        {
            // אופקי (X)
            Vector3 direction = swipeVector.x > 0 ? Vector3.right : Vector3.left;
            OnDirectionChanged?.Invoke(direction);
        }
        else
        {
            // אנכי מותאם למשחק שלך (Z)
            Vector3 direction = swipeVector.y > 0 ? Vector3.forward : Vector3.back;
            OnDirectionChanged?.Invoke(direction);
        }
    }
}