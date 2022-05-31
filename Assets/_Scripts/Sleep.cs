using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    public bool isAsleep;
    public bool canBePosessed;
    public bool posessed;

    public float posessionTime = 5f;
    public float delayBeforeSleep = 2f;

    public GameObject leftEye;
    public GameObject rightEye;

    public Color eyeColorPosessed;
    public Color eyeColorNormal;

    Animator anim;
    GameObject player;
    CameraFollow camFollow;
    Camera mainCam;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        camFollow = mainCam.GetComponent<CameraFollow>();
        if (isAsleep) anim.SetTrigger("Sleep");
    }

    private void Update()
    {
        if (!canBePosessed) return;
        if (isAsleep && Input.GetKeyDown(KeyCode.Space))
        {
            Posess();
            StartCoroutine(CancelPosession());
        }
    }

    public void GoToSleep()
    {
        anim.SetTrigger("Sleep");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        player = other.gameObject;
        canBePosessed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        canBePosessed = false;
    }

    void Posess()
    {
        isAsleep = false;
        posessed = true;
        leftEye.GetComponent<SpriteRenderer>().color = eyeColorPosessed;
        rightEye.GetComponent<SpriteRenderer>().color = eyeColorPosessed;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerController>().enabled = false;
        player.gameObject.SetActive(false);
        anim.SetTrigger("Posessed");
        gameObject.AddComponent<PlayerController>();
        camFollow.target = gameObject.GetComponent<Transform>();
    }

    IEnumerator CancelPosession()
    {
        yield return new WaitForSeconds(posessionTime);

        posessed = false;
        leftEye.GetComponent<SpriteRenderer>().color = eyeColorNormal;
        rightEye.GetComponent<SpriteRenderer>().color = eyeColorNormal;
        Destroy(GetComponent<PlayerController>());
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.gameObject.SetActive(true);
        camFollow.target = player.GetComponent<Transform>();
        player.GetComponent<PlayerController>().enabled = true;
        gameObject.transform.localScale = new Vector2(1, 1);

        yield return new WaitForSeconds(delayBeforeSleep);
        Reset();
    }

    private void Reset()
    {
        anim.SetTrigger("Sleep");
        isAsleep = true;
    }
}
