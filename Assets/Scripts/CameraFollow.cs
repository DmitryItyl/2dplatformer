using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;

    float smoothDampTime = 0.15f;
    Vector3 smoothDampVelocity = Vector3.zero;

    private float cameraWidth, cameraHeight, levelMinX, levelMaxX;


    // Start is called before the first frame update
    void Start()
    {
        cameraWidth = Camera.main.orthographicSize * 2;
        cameraHeight = cameraWidth * Camera.main.aspect;

        float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;

        levelMinX = leftBounds.position.x + cameraWidth / 2 + leftBoundsWidth;
        levelMaxX = rightBounds.position.x - cameraWidth / 2 - rightBoundsWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));
            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);

            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}
