using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerZoom : MonoBehaviour//https://youtu.be/HxnpWhxjJwE for code
{
    public float zoom;
    public float zoomMultiplier = 4f;
    public float minZoom = 1.5f;
    public float maxZoom = 5f;
    public float velocity = 0.2f;
    public float smoothTime = 1f;

    public Camera cam;

    void Start()
    {
        cam.orthographicSize = 3f;
        zoom = cam.orthographicSize;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}
