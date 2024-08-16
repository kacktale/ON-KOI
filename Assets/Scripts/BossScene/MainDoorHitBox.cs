using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainDoorHitBox : MonoBehaviour
{
    public Transform Outline;
    public RectTransform Txt_Info;
    public TextMeshProUGUI Txt_Name;
    // Start is called before the first frame update
    void Start()
    {
        Txt_Info.position = new Vector3(0, -6, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Outline.DOScale(new Vector3(1.09f, 1.09f, 1.09f), 0.4f).SetEase(Ease.OutQuint);
            Txt_Info.DOMoveY(-3.9f, 0.4f).SetEase(Ease.OutQuint);
            Txt_Name.text = "유일한 탈출구";
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Outline.DOScale(new Vector3(1, 1, 1), 0.4f).SetEase(Ease.OutQuint);
            Txt_Info.DOMoveY(-6, 0.4f).SetEase(Ease.OutQuint);
        }
    }
}
