using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float speed = 10.0f;
    float stopX1 = -28.55F;
    float stopX2 = 10.57F;
    bool moveLeft,moveRight = false;
    void Update()
    {

      
        if (moveRight)
        {
            transform.position += new Vector3(1 * Time.deltaTime * speed,
                                        0.0f, 0.0f);
        }
        if (moveLeft)
        {
            transform.position -= new Vector3(1 * Time.deltaTime * speed,
                                        0.0f, 0.0f);
        }


        if (transform.position.x < stopX1)
        {
            transform.position = new Vector3(stopX1, transform.position.y, transform.position.z);
        }
        if (transform.position.x > stopX2)
        {
            transform.position = new Vector3(stopX2, transform.position.y, transform.position.z);
        }

    }
    public void leftCameraButton()
    {
        moveLeft = true;
        moveRight = false;

    }
    public void leftCameraButtonUp()
    {
        moveLeft = false;
    }
    public void rightCameraButton()
    {
        moveRight = true;
        moveLeft = false;
    }
    public void rightCameraButtonUp()
    {
        moveRight = false;
    }
}
