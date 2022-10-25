using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public Transform target;
    public float smoothFactor = 5f;
    public Vector3 offset = new Vector3(0,0,-10f);
    public bool smoothCamera = true;
    Vector3 desiredPosition;
    Vector3 smoothedPosition;
    // Start is called before the first frame update
    void Start()
    {
        smoothCamera = false;
        desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        smoothCamera = true;
        // Invoke("CameraStartPos", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }


    private void FixedUpdate() {
        if(smoothCamera)
        {
            desiredPosition = target.position + offset;
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothFactor * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        else
        {
            transform.position = target.position + offset;
        }
    }

    void CameraStartPos()
    {
        smoothCamera = false;
        desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        smoothCamera = true;
    }
}
