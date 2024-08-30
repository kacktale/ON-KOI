using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideObj : MonoBehaviour
{
    public GameObject BreathUI;
    public Player Player;
    public Slider BreathRemaning;

    private bool breathReload = false;
    public bool Isbreath =false;
    private bool IsPlayerTouched = false;
    public bool IsHide = false;
    // Start is called before the first frame update
    void Start()
    {
        Isbreath = true;
        BreathUI.SetActive(false);
        Player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        BreathUI.transform.position = new Vector2(transform.position.x,transform.position.y+2);
        if (Input.GetKeyDown(KeyCode.LeftControl)&&IsPlayerTouched)
        {
            if (!IsHide)
            {
                BreathUI.SetActive(true);
                Player.gameObject.SetActive(false);
                IsHide = true;
                Isbreath = false;
                IsPlayerTouched = true;
            }
            else
            {
                BreathUI.SetActive(false);
                Player.gameObject.SetActive(true);
                StartCoroutine(OFFTwoSee());
                IsHide = false;
                Isbreath = true;
                IsPlayerTouched = true;
            }
        }
        if(Input.GetKey(KeyCode.LeftShift) && IsHide)
        {
            if(BreathRemaning.value > 0 && !breathReload)
            {
                Isbreath = true;
                BreathRemaning.value -= Time.deltaTime;
            }
            else
            {
                Isbreath = false;
                breathReload = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && IsHide)
        {
            Isbreath = false;
        }
        if (!Isbreath)
        {
            BreathRemaning.value += Time.deltaTime * 2f;
        }
        if(BreathRemaning.value == 7)
        {
            breathReload = false;
        }
    }
    IEnumerator OFFTwoSee()
    {
        Player.IsDefault = true;
        Player.TwoSee.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.4f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(0.4f);

        Player.TwoSeeObj.SetActive(false);
        Player.IsTwoSeed = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerTouched = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerTouched = false;
        }
    }
}
