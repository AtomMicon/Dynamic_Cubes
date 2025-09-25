using System;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    public event Action Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Clicked?.Invoke();
        }
    }
}
