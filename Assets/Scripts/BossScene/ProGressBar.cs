using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ProGressBar : MonoBehaviour
{
    //보스전용
    public Transform Door;
    // Start is called before the first frame update
    void Start()
    {
        //UI를 문 위에 뜨게 하기
        transform.position = Door.position + new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //UI를 문 위에 뜨게 하기
        transform.position = Door.position + new Vector3(0,2,0);
    }
}
