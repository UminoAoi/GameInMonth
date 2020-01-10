using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject playerObject;
    public float speed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        playerObject = GameObject.FindGameObjectWithTag("PlayerPINK");
        if (playerObject == null)
            playerObject = GameObject.FindGameObjectWithTag("PlayerGRAY");

        Vector3 target = new Vector3(playerObject.transform.position.x + offset.x, transform.position.y + offset.y, offset.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
    }
}
