using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour//https://youtu.be/H7pjj1K91HE for code
{
    public Vector3 topLeftBound;
    public Vector3 bottomRightBound;
    public Transform testTransform;
    Vector3 _origin;
    Vector3 _difference;

    Camera _mainCamera;

    bool _isDragging;

    void Awake()
    {
        _mainCamera = Camera.main;
        testTransform.position = transform.position;
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started) _origin = GetMousePosition;
        _isDragging = ctx.started || ctx.performed;
    }

    void LateUpdate()
    {
        if (!_isDragging) return;

        _difference = GetMousePosition - testTransform.position;
        testTransform.position = _origin - _difference;
        if (!(testTransform.position.x < topLeftBound.x || testTransform.position.x > bottomRightBound.x || testTransform.position.y > topLeftBound.y || testTransform.position.y < bottomRightBound.y))
        {
            _difference = GetMousePosition - transform.position;
            transform.position = _origin - _difference;
        }
        else
        {
            testTransform.position = transform.position;
        }
    }

    Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
