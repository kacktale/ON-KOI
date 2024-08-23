using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Password : MonoBehaviour
{
    public DoorGimic[] doorGimics;
    public DoorProsess Doorlist;
    Transition Transition;
    public DoorLock DoorLock;
    Player Player;
    public int[] PassWordAnser;
    public TextMeshPro txt_CurPassWord;
    public bool FinalPhaze = false;
    public int PassWordSetting = 0;
    public bool ArrowPassWord = false;
    // Start is called before the first frame update
    void Start()
    {
        Transition = FindAnyObjectByType<Transition>();
        //DoorLock = FindAnyObjectByType<DoorLock>();
        Player = FindAnyObjectByType<Player>();
        txt_CurPassWord.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if (!ArrowPassWord)
        {
            string passwordLength = txt_CurPassWord.text;
            int passwordAnser;
            if (passwordLength.Length == 4)
            {
                if (int.TryParse(txt_CurPassWord.text, out passwordAnser))
                {
                    if (passwordAnser == PassWordAnser[PassWordSetting])
                    {
                        if (!FinalPhaze)
                        {
                            Debug.Log("탈출 성공");
                            DoorLock.isPassWordAppear = false;
                            Player.CurPlayerSpeed = Player.PlayerSpeed;
                            Player.CurPlayerJump = Player.PlayerJump;
                            StartCoroutine(Transition.MoveToNextScene());
                        }
                        else if (FinalPhaze)
                        {
                            Debug.Log("문이 잠겼습니다.");

                            DoorLock.OffPassWord();

                            DoorLock.isPassWordAppear = false;

                            Player.IsPassword = false;
                            Player.CurPlayerSpeed = Player.PlayerSpeed;
                            Player.CurPlayerJump = Player.PlayerJump;

                            PassWordSetting = Random.Range(0, PassWordAnser.Length);
                            if (!doorGimics[0].Opend)
                            {
                                doorGimics[0].IsOpend = false;
                                doorGimics[0].Processing.value = 12;
                                Doorlist.ProCessings[0].color = new Color(0, 255, 0);
                            }
                        }
                    }
                    else if (passwordAnser != PassWordAnser[PassWordSetting])
                    {
                        DoorLock.OffPassWord();
                        DoorLock.isPassWordAppear = false;
                        Player.IsPassword = false;
                        Player.CurPlayerSpeed = Player.PlayerSpeed;
                        Player.CurPlayerJump = Player.PlayerJump;
                        txt_CurPassWord.text = "Err";
                    }
                }
            }
        }
        else
        {
            string passwordString = txt_CurPassWord.text;
            string hintText = "";

            foreach (char digit in passwordString)
            {
                if (digit == '↑')
                {
                    hintText += "1";
                }
                else if (digit == '←')
                {
                    hintText += "2";
                }
                else if (digit == '↓')
                {
                    hintText += "3";
                }
                else
                {
                    hintText += "4";
                }
                txt_CurPassWord.text = hintText;
                //txt_CurPassWord.color = new Color(0, 0, 0, 0);
                string passwordLength = txt_CurPassWord.text;
                int passwordAnser;
                if (int.TryParse(txt_CurPassWord.text, out passwordAnser))
                {
                    if (passwordAnser == PassWordAnser[PassWordSetting])
                    {
                        if (!FinalPhaze)
                        {
                            Debug.Log("탈출 성공");
                            DoorLock.isPassWordAppear = false;
                            Player.CurPlayerSpeed = Player.PlayerSpeed;
                            Player.CurPlayerJump = Player.PlayerJump;
                            StartCoroutine(Transition.MoveToNextScene());
                        }
                        else if (FinalPhaze)
                        {
                            txt_CurPassWord.color = new Color(0, 0, 0, 0);
                            Debug.Log("문이 잠겼습니다.");

                            if (!doorGimics[1].Opend)
                            {
                                doorGimics[1].IsOpend = false;
                                doorGimics[1].Processing.value = 12;
                                Doorlist.ProCessings[1].color = new Color(0, 255, 0);
                            }

                            DoorLock.OffPassWord();

                            DoorLock.isPassWordAppear = false;

                            Player.IsPassword = false;
                            Player.CurPlayerSpeed = Player.PlayerSpeed;
                            Player.CurPlayerJump = Player.PlayerJump;

                            PassWordSetting = Random.Range(0, PassWordAnser.Length);
                        }
                    }
                    else if (passwordAnser != PassWordAnser[PassWordSetting])
                    {
                        DoorLock.OffPassWord();
                        DoorLock.isPassWordAppear = false;
                        Player.IsPassword = false;
                        Player.CurPlayerSpeed = Player.PlayerSpeed;
                        Player.CurPlayerJump = Player.PlayerJump;
                        txt_CurPassWord.text = "Err";
                    }
                }
            }
        }
    }
}
