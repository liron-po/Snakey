using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeInput : MonoBehaviour
{
    private SnakeMovement _movement;

    void Start()
    {
        _movement = GetComponent<SnakeMovement>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
        {
            _movement.SetDirection(Vector3.forward);
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
        {
            _movement.SetDirection(Vector3.back);
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
        {
            _movement.SetDirection(Vector3.left);
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            _movement.SetDirection(Vector3.right);
        }
    }

    public void OnMoveUp()
    {
        _movement.SetDirection(Vector3.forward);
    }

    public void OnMoveDown()
    {
        _movement.SetDirection(Vector3.back);
    }

    public void OnMoveLeft()
    {
        _movement.SetDirection(Vector3.left);
    }

    public void OnMoveRight()
    {
        _movement.SetDirection(Vector3.right);
    }
}
