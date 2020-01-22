using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Scene teleportTo;
    public Animator animator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.CompareTag("PlayerPINK") || player.CompareTag("PlayerGRAY"))
        {
            if (Input.GetKeyDown("space"))
                StartCoroutine(LoadScene(teleportTo));
        }
    }

    public IEnumerator LoadScene(Scene scene)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene.handle);
    }
}
