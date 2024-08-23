using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiddenPassWord : MonoBehaviour
{
    TextMeshPro txt_Hint;
    public Password Password;
    public bool DoorLockPassword = true;
    // Start is called before the first frame update
    void Start()
    {
        txt_Hint = GetComponent<TextMeshPro>();
        txt_Hint.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorLockPassword)
        {
            txt_Hint.text = Password.PassWordAnser[Password.PassWordSetting].ToString();
        }
        else
        {
            string passwordString = Password.PassWordAnser[Password.PassWordSetting].ToString();
            string hintText = "";

            foreach(char digit in passwordString)
            {
                if(digit == '1')
                {
                    hintText += "��";
                }
                else if (digit == '2')
                {
                    hintText += "��";
                }
                else if (digit == '3')
                {
                    hintText += "��";
                }
                else
                {
                    hintText += "��";
                }
                txt_Hint.text = hintText;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TwoSee"))
        {
            txt_Hint.DOColor(Color.white, 0.5f).SetEase(Ease.OutQuint);
            //Debug.Log("������");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TwoSee"))
        {
            txt_Hint.DOColor(new Color(0, 0, 0, 0), 0.4f).SetEase(Ease.InQuint);
            //Debug.Log("������");
        }
    }
}
