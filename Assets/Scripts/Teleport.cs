using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Scene teleportTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(teleportTo.handle);
    }
}
