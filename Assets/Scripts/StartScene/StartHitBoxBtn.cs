using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartHitBoxBtn : MonoBehaviour
{
    public Warning warning;
    public void OnMouseDown()
    {
        StartCoroutine(warning.WarningPannul());
    }
}
