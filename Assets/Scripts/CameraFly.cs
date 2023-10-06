using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFly : MonoBehaviour
{
    public float flySpeed = 3f;
    public float mouseSpeed = 3f;


    Vector3 movementThisFrame;
    private float MouseX;
    private float MouseY;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraRotate();
    }

    private void CameraRotate()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * mouseSpeed, -Input.GetAxis("Mouse X") * mouseSpeed, 0));
            MouseX = transform.rotation.eulerAngles.x;
            MouseY = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(MouseX, MouseY, 0);
        }
    }

    private void CameraMove()
    {
        movementThisFrame = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementThisFrame += transform.forward * Time.deltaTime * flySpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementThisFrame += transform.forward * -1.0f * Time.deltaTime * flySpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementThisFrame += transform.right * Time.deltaTime * flySpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementThisFrame += transform.right * -1.0f * Time.deltaTime * flySpeed;
        }
        this.transform.position += movementThisFrame;
    }
}
