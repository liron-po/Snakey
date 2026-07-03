using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeInput : MonoBehaviour, IInputReader
{
    public event Action<Vector3> OnDirectionChanged;

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
        {
            OnDirectionChanged?.Invoke(Vector3.forward);
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
        {
            OnDirectionChanged?.Invoke(Vector3.back);
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
        {
            OnDirectionChanged?.Invoke(Vector3.left);
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            OnDirectionChanged?.Invoke(Vector3.right);
        }
    }

    public void OnMoveUp() => OnDirectionChanged?.Invoke(Vector3.forward);
    public void OnMoveDown() => OnDirectionChanged?.Invoke(Vector3.back);
    public void OnMoveLeft() => OnDirectionChanged?.Invoke(Vector3.left);
    public void OnMoveRight() => OnDirectionChanged?.Invoke(Vector3.right);
}