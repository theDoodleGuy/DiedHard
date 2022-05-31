using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour
{
    public bool bumped;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hostile")) {
            other.GetComponent<Animator>().SetTrigger("EnterDoor");
            Destroy(other.gameObject, 1);
        }
        if (!other.CompareTag("Player")) return;
        bumped = true;
    }
}
