using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Camera Follow
    public float FollowSmoothness = 0.3f;
    private Vector3 Velo = Vector3.zero;
    private Vector3 CamTargetDifference;

    private float boop = 0;
    private float breakpoint = int.MaxValue;
    private bool Augoo = true;

    public GameObject Player;

    void Start()
    {
        CamTargetDifference = transform.position - Player.transform.position;
        transform.LookAt(Player.transform);
    }

    void FixedUpdate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Camera Follow
        transform.RotateAround(new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z), new Vector3(0, 0, 1), -Input.GetAxis("Mouse X") * 2);
        boop += Input.GetAxis("Mouse Y") * 0.5f;
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * 2);
        transform.position += new Vector3(0, 0, Input.GetAxis("Mouse Y") * 0.5f);
        if (transform.position.z >= -3)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -3);
            if(Augoo == true)
            {
                breakpoint = boop;
                Augoo = false;
            }
        }

        Debug.Log("Sheet: " + boop);
        Debug.Log("Break: " + breakpoint);
    }
}
