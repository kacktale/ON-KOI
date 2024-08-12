using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    public Transform BtnTransform,TitleTransform;     //버튼 위치 불러오기
    // Start is called before the first frame update
    void Start()
    {
        TitleTransform.position += new Vector3(-723, 0, 0);
        BtnTransform.position += new Vector3(-723, 0, 0);
        BtnTransform.DOLocalMoveX(0,1f).SetEase(Ease.OutQuint);
        StartCoroutine(TitleAppear());
    }
    IEnumerator TitleAppear()
    {
        yield return new WaitForSeconds(0.3f);
        TitleTransform.DOLocalMoveX(0, 1f).SetEase(Ease.OutQuint);
    }
    public void StartGame()
    {
        Debug.Log("");
    }
}
