using System;
using UnityEngine;

public interface IInputReader
{
    event Action<Vector3> OnDirectionChanged;
}