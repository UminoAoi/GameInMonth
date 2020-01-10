using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableNames type;
    public int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objectThing = collision.gameObject;

        if (objectThing.CompareTag("PlayerGRAY")|| objectThing.CompareTag("PlayerPINK"))
        {
            PlayerStateManager.AddItems(type, value);
            Destroy(gameObject);
        }
    }
}
