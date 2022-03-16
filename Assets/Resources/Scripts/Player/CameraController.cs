using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    float xRotation = 0.0f;
    float yRotation = 0.0f;
    public GameObject body;
    public bool isFrozen = true;
    public PhotonView view;
    private void Start()
    {
        if (!view.IsMine)
        {
            gameObject.SetActive(false);
        }
        xRotation = gameObject.transform.rotation.x;
        yRotation = gameObject.transform.rotation.y;
    }

    void LateUpdate()
    {
        if (view.IsMine)//Not Neccessary as we're disabling the object if it isn't ours, but nice insurance
        {
            if (Input.GetButtonDown("MouseMode"))
            {
                isFrozen = !isFrozen;
            }
            if (!isFrozen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * 1.5f;
                float mouseY = Input.GetAxis("Mouse Y") * 1.5f;

                yRotation += mouseX;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -75, 75);
                body.transform.eulerAngles = new Vector3(0f, yRotation, 0f);
                transform.eulerAngles = new Vector3(xRotation, body.transform.eulerAngles.y, 0.0f);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
