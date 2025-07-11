using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.DOColor(new Color(0, 0, 0, 0), 0.5f);
    }
    public IEnumerator MoveToNextScene()
    {
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.DOColor(new Color(0, 0, 0, 255), 0.5f);
        yield return new WaitForSeconds(0.5f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void GameOverTransition()
    {
        StartCoroutine(ReLoadTransition());
    }
    public IEnumerator ReLoadTransition()
    {
        SpriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
