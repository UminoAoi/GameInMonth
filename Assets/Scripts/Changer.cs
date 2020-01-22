using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour
{
    public GameObject playerPINK;
    public GameObject playerGRAY;
    public Animator animator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject player = collision.gameObject;
            Vector2 position = player.transform.position;
            animator.SetTrigger("change");

            if (player.CompareTag("PlayerPINK"))
            {
                playerGRAY.transform.position = position;
                GameObject newPlayer = Instantiate(playerGRAY);
                Destroy(player);
            }
            else if (player.CompareTag("PlayerGRAY"))
            {
                playerPINK.transform.position = position;
                GameObject newPlayer = Instantiate(playerPINK);
                Destroy(player);
            }
        }
    }

}
