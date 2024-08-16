using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDoor : MonoBehaviour
{
    public Slider Progress;
    public GameObject ProgressObj;
    Player Player;
    public Transform SeeWa;
    public bool IsPlayerTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        ProgressObj.SetActive(false);
        Progress.value = 0;
        Player = FindAnyObjectByType<Player>();
        SeeWa.position = new Vector3(0.21f, 69, 0);
        SeeWa.localScale = new Vector3(25.13f, 32f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (IsPlayerTouched)
            {
                Player.IsDoor = true;
                ProgressObj.SetActive(true);
                Player.CurPlayerJump = 0;
                Player.CurPlayerSpeed = 0;
                Player.IsPassword = true;
                SeeWa.DOScale(new Vector3(1, 1.28f, 1), 0.4f).SetEase(Ease.OutQuint);
                SeeWa.DOLocalMove(new Vector3(0.21f,2.7f,0),0.4f).SetEase(Ease.OutQuint);
                if(Progress.value > 180)
                {
                    Progress.value = 180;
                }
                Progress.value += Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ProgressObj.SetActive(false);
            StartCoroutine(PlayerRecover());
            SeeWa.DOScale(new Vector3(25.13f, 32f, 1), 0.4f).SetEase(Ease.InQuint);
            SeeWa.DOLocalMove(new Vector3(0.21f, 69, 0), 0.4f).SetEase(Ease.InQuint);
        }
    }
    IEnumerator PlayerRecover()
    {
        yield return new WaitForSeconds(0.4f);

        Player.IsDoor = false;
        Player.CurPlayerJump = Player.PlayerJump;
        Player.CurPlayerSpeed = Player.PlayerSpeed;
        Player.IsPassword = false;
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
        if (collision.CompareTag("Player"))
        {
            IsPlayerTouched = false;
        }
    }
}
