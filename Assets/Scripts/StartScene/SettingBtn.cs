using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingBtn : MonoBehaviour
{
    public Transform SettingPannul;
    bool IsAppear = false;
    public void OnMouseDown()
    {
        if (!IsAppear)
        {
            SettingPannul.DOMoveY(0, 0.5f).SetEase(Ease.OutQuint);
            IsAppear = true;
        }
        else
        {
            SettingPannul.DOMoveY(13, 0.5f).SetEase(Ease.OutQuint);
            IsAppear = false;
        }
    }
}
