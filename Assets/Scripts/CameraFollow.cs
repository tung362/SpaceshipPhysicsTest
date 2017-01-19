using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Camera Follow
    public float FollowSmoothness = 0.3f;
    private Vector3 Velo = Vector3.zero;
    private Vector3 CamTargetDifference;

    public float ScrollSpeed = 5;
    public float ZoomSpeed = 3;
    public float ZoomRate = 0.5f;
    public float ZoomMin = -10;
    public float ZoomMax = -3;
    public float ZoomForward = 15;
    private float newMouseZ = 0;

    public GameObject Player;

    void Start()
    {
        CamTargetDifference = transform.position - Player.transform.position;
        transform.LookAt(Player.transform);
        newMouseZ = transform.position.z;
    }

    void FixedUpdate()
    {
        MouseLock();
        MouseLook();
        MouseZoom();
    }

    void MouseLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void MouseLook()
    {
        transform.RotateAround(new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z), new Vector3(0, 0, 1), -Input.GetAxis("Mouse X") * 2);
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * 2);
    }

    void MouseZoom()
    {
        /*Zoom*/
        //back
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            newMouseZ += ZoomRate;
            if (newMouseZ > ZoomMax) newMouseZ = ZoomMax;
        }
        //forward
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            newMouseZ -= ZoomRate;
            if (newMouseZ < ZoomMin) newMouseZ = ZoomMin;
        }

        float normalizedMouseZ = (newMouseZ - ZoomMin) / (ZoomMax - ZoomMin);
        Vector3 playerDirection = (transform.position - Player.transform.position).normalized;

        if(playerDirection.x == 0 && playerDirection.y == 0 && newMouseZ != normalizedMouseZ)
        {
            playerDirection = -Player.transform.up;
        }

        Debug.Log("MOO: " + normalizedMouseZ);
        Debug.Log("X: " + playerDirection.x + "Y: " + playerDirection.y);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.transform.position.x + (playerDirection.x * ZoomForward * normalizedMouseZ), Player.transform.position.y + (playerDirection.y * ZoomForward * normalizedMouseZ), newMouseZ), ScrollSpeed * Time.deltaTime);
    }
}
