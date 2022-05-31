using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject uiHelper;
    [SerializeField] GameObject spawnLocation;
    [SerializeField] bool spawnFacingRight;

    [SerializeField] bool isLocked;
    [SerializeField] bool canEnter;

    Animator faderAnim;
    Animator anim;
    Animator uiAnim;
    Vector2 spawn;
    GameObject player;
    Vector2 offset;

    private void Start()
    {
        faderAnim = FindObjectOfType<Fade>().GetComponent<Animator>();
        anim = GetComponent<Animator>();
        uiAnim = uiHelper.GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>().gameObject;
        spawn = spawnLocation.transform.position;
        offset = new Vector2(0, -1.5f);
    }

    private void Update()
    {
        if (!canEnter) return;
        if (canEnter && Input.GetKeyDown(KeyCode.Space))
        {
            UseDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hostile"))
        {
            isLocked = false;
            anim.SetTrigger("OpenDoor");
        }
        if (isLocked) return;
        if (!collision.CompareTag("Player")) return;
        uiAnim.SetBool("showUI", true);
        canEnter = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        canEnter = false;
        uiAnim.SetBool("showUI", false);
    }

    void UseDoor()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerController.enabled = false;
        faderAnim.SetTrigger("FadeOut");
        StartCoroutine(DelayAndFadeIn(playerController));
    }

    IEnumerator DelayAndFadeIn(PlayerController playerController)
    {
        Vector2 scale = player.transform.localScale;
        yield return new WaitForSecondsRealtime(1.5f);
        player.gameObject.transform.position = spawn + offset;
        player.transform.localScale = spawnFacingRight ? new Vector2(-scale.x, scale.y) : new Vector2(scale.x, scale.y);
        faderAnim.SetTrigger("FadeIn");
        playerController.enabled = true;
    }
}
