using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HideObj HideObj;
    public bool IsChase = false;
    public GameObject Player;
    public LayerMask LayerMask;
    public GameObject EnemyScare;

    private Transition transition;

    private void Awake()
    {
        transition=FindAnyObjectByType<Transition>();
        EnemyScare.SetActive(false);
    }

    void Start()
    {
        HideObj = FindAnyObjectByType<HideObj>();
    }

    void FixedUpdate()
    {
        Vector2 PlayerRay = new Vector2(transform.position.x, transform.position.y + 0.3f);
        Vector2 PlayerHide = new Vector2(transform.position.x, transform.position.y - 0.3f);
        RaycastHit2D PlayerHit = Physics2D.Raycast(PlayerRay, Vector2.left, 6f, LayerMask);
        RaycastHit2D PlayerHitBack = Physics2D.Raycast(PlayerRay, Vector2.left, -6f, LayerMask);
        RaycastHit2D PlayerHideHit = Physics2D.Raycast(PlayerHide, Vector2.left, 2f, LayerMask);
        RaycastHit2D PlayerHideHitBack = Physics2D.Raycast(PlayerHide, Vector2.left, -2f, LayerMask);
        Debug.DrawRay(PlayerHide, Vector2.left * 6, new Color(0, 1, 0));
        Debug.DrawRay(PlayerRay, Vector2.left * 2f, new Color(1, 0, 0));
        if (PlayerHit.collider != null && (PlayerHit.collider.gameObject.CompareTag("Player") || PlayerHitBack.collider.gameObject.CompareTag("Player")))
        {
            IsChase = true;
        }
        if (PlayerHideHit.collider != null && PlayerHideHit.collider.gameObject.CompareTag("Box"))
        {
            if (!HideObj.Isbreath)
            {
                StartCoroutine(JumpScare());
            }
        }
    }

    private void Update()
    {
        if (IsChase)
        {
            transform.DOMoveX(Player.transform.position.x, 4);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) // OnTriggerEnter2D -> OnTriggerStay2D
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            if (!HideObj.Isbreath)
            {
                StartCoroutine(JumpScare());
            }
            if (HideObj.IsHide)
            {
                IsChase = false;
                StartCoroutine(ChaseOFF());
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) // OnCollisionEnter2D -> OnCollisionStay2D
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(JumpScare());
        }
    }

    IEnumerator ChaseOFF()
    {
        yield return new WaitForSeconds(3);
        transform.DOMoveX(-5, 4);
    }
    IEnumerator JumpScare()
    {
        EnemyScare.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transition.GameOverTransition();
    }
}
