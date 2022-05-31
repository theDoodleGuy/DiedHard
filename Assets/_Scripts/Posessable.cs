using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posessable : MonoBehaviour
{
    bool CanPosess;

    private void Update()
    {
        if (!CanPosess) return;
        if (CanPosess && Input.GetKeyDown(KeyCode.Space))
        {
            Posess();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CompareTag("Player")) return;
        if (!collision.GetComponent<Sleep>().isAsleep) return;
        CanPosess = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CompareTag("Player")) return;
        CanPosess = false;
    }

    void Posess()
    {
        Debug.Log("Do something");
    }
}
