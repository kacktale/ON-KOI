using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorLock : MonoBehaviour
{
    public TextMeshPro txt_CurPassWord;
    Player Player;
    public Transform Outline,PassWord;
    public RectTransform Txt_Info;
    public TextMeshProUGUI Txt_Name;
    public bool isPassWordAppear = false;
    bool isPlayerApproched = false;
    // Start is called before the first frame update
    void Start()
    {
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
                    txt_CurPassWord.text = "";
                    PassWord.DOLocalMoveY(0.6f, 0.4f).SetEase(Ease.InQuint);
                    isPassWordAppear = true;
                    Player.IsPassword = true;
                    Player.CurPlayerSpeed = 0;
                    Player.CurPlayerJump = 0;
                }
                else
                {
                    txt_CurPassWord.text = "";
                    PassWord.DOLocalMoveY(-983.8799f, 0.4f).SetEase(Ease.InQuint);
                    Player.IsPassword = false;
                    isPassWordAppear = false;
                    Player.CurPlayerSpeed = Player.PlayerSpeed;
                    Player.CurPlayerJump = Player.PlayerJump;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Outline.DOScale(new Vector3(1.09f, 1.09f, 1.09f),0.4f).SetEase(Ease.OutQuint);
            Txt_Info.DOMoveY(-3.9f, 0.4f).SetEase(Ease.OutQuint);
            Txt_Name.text = "DoorLock";
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
