using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Password : MonoBehaviour
{
    DoorLock DoorLock;
    Player Player;
    public int PassWordAnser;
    public TextMeshPro txt_CurPassWord;
    // Start is called before the first frame update
    void Start()
    {
        DoorLock = FindAnyObjectByType<DoorLock>();
        Player = FindAnyObjectByType<Player>();
        txt_CurPassWord.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        string passwordLength = txt_CurPassWord.text;
        int passwordAnser;
        if(passwordLength.Length > 4)
        {
            Debug.Log("��й�ȣ�� 4���� �̻��� �� �� �����ϴ�.");
        }
        if (int.TryParse(txt_CurPassWord.text, out passwordAnser))
        {
            if (passwordAnser == PassWordAnser)
            {
                Debug.Log("Ż�� ����");
                DoorLock.PassWord.DOLocalMoveY(-9.11f, 0.4f).SetEase(Ease.InQuint);
                DoorLock.isPassWordAppear = false;
                Player.CurPlayerSpeed = Player.PlayerSpeed;
                Player.CurPlayerJump = Player.PlayerJump;
            }
            else
            {
                txt_CurPassWord.text = "Err";
                DoorLock.PassWord.DOLocalMoveY(-9.11f, 0.4f).SetEase(Ease.InQuint);
                DoorLock.isPassWordAppear = false;
                Player.CurPlayerSpeed = Player.PlayerSpeed;
                Player.CurPlayerJump = Player.PlayerJump;
            }
        }
    }
}
