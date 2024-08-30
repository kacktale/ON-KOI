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

    Enemy enemy;
    Player Player;
    public Transform Outline, LetterObj;
    public RectTransform Txt_Info;
    public TextMeshProUGUI Txt_Name;

    public bool isPassWordAppear = false;
    public bool IsBossAppear = false;
    bool isPlayerApproched = false;
    bool isOutpaper = false;
    // Start is called before the first frame update

    private void Awake()
    {
        Player = FindAnyObjectByType<Player>();
        enemy = FindAnyObjectByType<Enemy>();
    }

    void Start()
    {
        HiddenObj.SetActive(false);
        
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
                    LetterObj.DOLocalMoveY(63.9f, 0.4f).SetEase(Ease.InQuint);
                    isOutpaper = true;
                    isPassWordAppear = true;
                    Player.IsPassword = true;
                    Player.CurPlayerSpeed = 0;
                    Player.CurPlayerJump = 0;
                }
                else if(isPassWordAppear && isOutpaper)
                {
                    LetterObj.DORotate(new Vector3(0,180,90),0.4f);
                    isOutpaper = false;
                }
                else
                {
                    StartCoroutine(GetBack());
                }
            }
        }
    }

    IEnumerator GetBack()
    {
        LetterObj.DOLocalMoveY(-1025.61f, 0.4f).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(0.4f);
        Player.IsPassword = false;
        if(IsBossAppear)
            enemy.IsChase = true;
        //isPassWordAppear = false;
        Player.CurPlayerSpeed = Player.PlayerSpeed;
        Player.CurPlayerJump = Player.PlayerJump;
        HiddenObj.SetActive(true);
        Destroy(gameObject);
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
