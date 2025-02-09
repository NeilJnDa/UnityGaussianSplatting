using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFly : MonoBehaviour
{
    public float flySpeed = 3f;
    public float sprintSpeed = 5f;
    public float mouseSpeed = 3f;

    private float currentFlySpeed;

    Vector3 movementThisFrame;
    private float MouseX;
    private float MouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraRotate();
    }

    private void CameraRotate()
    {

        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * mouseSpeed, Input.GetAxis("Mouse X") * mouseSpeed, 0));
        MouseX = transform.rotation.eulerAngles.x;
        MouseY = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(MouseX, MouseY, 0);
        

    }

    private void CameraMove()
    {

        currentFlySpeed = Mathf.Lerp(currentFlySpeed, Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : flySpeed, Time.deltaTime);

        movementThisFrame = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementThisFrame += transform.forward * Time.deltaTime * currentFlySpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementThisFrame += transform.forward * -1.0f * Time.deltaTime * currentFlySpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementThisFrame += transform.right * Time.deltaTime * currentFlySpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementThisFrame += transform.right * -1.0f * Time.deltaTime * currentFlySpeed;
        }
        this.transform.position += movementThisFrame;
    }
}
