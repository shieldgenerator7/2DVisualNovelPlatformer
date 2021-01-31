using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AreaManager>().rejumpToDoor();
            collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
