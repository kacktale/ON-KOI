using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public GameObject HiddenObj;
    Player Player;
    public Transform Outline, LetterObj;
    public RectTransform Txt_Info;
    public TextMeshProUGUI Txt_Name;
    public bool isPassWordAppear = false;
    bool isPlayerApproched = false;
    bool isOutpaper = false;
    // Start is called before the first frame update
    void Start()
    {
        HiddenObj.SetActive(false);
        Player = FindAnyObjectByType<Player>();
        Outline.localScale = new Vector3(1, 1, 1);
        Txt_Info.position = new Vector3(0, -6, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayerApproched)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!isPassWordAppear)
                {
                    LetterObj.DOLocalMoveY(0.6f, 0.4f).SetEase(Ease.InQuint);
                    isPassWordAppear = true;
                    Player.IsPassword = true;
                    Player.CurPlayerSpeed = 0;
                    Player.CurPlayerJump = 0;
                    isOutpaper = true;
                }
                else if(isPassWordAppear && isOutpaper)
                {
                    isOutpaper = false;
                }
                else
                {
                    LetterObj.DOLocalMoveY(-9.11f, 0.4f).SetEase(Ease.InQuint);
                    Player.IsPassword = false;
                    isPassWordAppear = false;
                    Player.CurPlayerSpeed = Player.PlayerSpeed;
                    Player.CurPlayerJump = Player.PlayerJump;
                    HiddenObj.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Outline.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f).SetEase(Ease.OutQuint);
            Txt_Info.DOMoveY(-3.9f, 0.4f).SetEase(Ease.OutQuint);
            Txt_Name.text = "Letter";
            isPlayerApproched = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Outline.DOScale(new Vector3(1, 1, 1), 0.4f).SetEase(Ease.OutQuint);
            Txt_Info.DOMoveY(-6, 0.4f).SetEase(Ease.OutQuint);
            isPlayerApproched = false;
        }
    }
}
