using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject endUI;
    Animator faderAnim;

    void Start()
    {
        faderAnim = FindObjectOfType<Fade>().GetComponent<Animator>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        PlayerController playerController = collision.GetComponent<PlayerController>();
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerController.enabled = false;
        faderAnim.SetTrigger("EndGame");
    }
}
