using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public BoxCollider2D mapBounds;
    public Camera camera;

    public float speed;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float clampedX;
    private float clampedY;
    private float cameraRatio;
    private float cameraOrthsize;

    void Start()
    {
        GetMinMaxValues();
        transform.position = playerTransform.position;
    }

    void Update()
    {
        FollowPlayer(); 
    }

    private void GetMinMaxValues(){
        minX = mapBounds.bounds.min.x;
        maxX = mapBounds.bounds.max.x;
        minY = mapBounds.bounds.min.y;
        maxY = mapBounds.bounds.max.y;
        cameraOrthsize = camera.orthographicSize;
        cameraRatio = (maxX + cameraOrthsize) / 2.0f;
    }

    private void FollowPlayer()
    {
        clampedY = Mathf.Clamp(playerTransform.position.y, minY + cameraOrthsize, maxY - cameraOrthsize);
        clampedX = Mathf.Clamp(playerTransform.position.x, minX + cameraRatio, maxX - cameraRatio);

        if(playerTransform != null)
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
    }
}
