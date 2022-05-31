using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSkinTrigger : MonoBehaviour
{
    [SerializeField] Interact interact;
    [SerializeField] GameObject doorToUnlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hostile"))
        {
            interact.FallOver();
            doorToUnlock.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
