using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeAfterDestroy = 5;
    float currentTime = 0;

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeAfterDestroy && gameObject.layer != 8)
            Destroy(gameObject);
    }
    
}
