using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour//https://youtu.be/H7pjj1K91HE for code
{
    public Vector3 topLeftBound;
    public Vector3 bottomRightBound;
    public Vector3 testTransform;
    public float smoothTime = 1f;
    Vector3 _origin;
    Vector3 _difference;

    Camera _mainCamera;

    bool _isDragging;

    void Awake()
    {
        _mainCamera = Camera.main;
        testTransform = transform.position;
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started) _origin = GetMousePosition;
        _isDragging = ctx.started || ctx.performed;
    }

    void LateUpdate()
    {
        if (!_isDragging) return;

        _difference = GetMousePosition - testTransform;
        testTransform = _origin - _difference;
        testTransform.x = Mathf.Clamp(testTransform.x, topLeftBound.x, bottomRightBound.x);
        testTransform.y = Mathf.Clamp(testTransform.y, bottomRightBound.y, topLeftBound.y);
        transform.position = testTransform;
    }

    Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
