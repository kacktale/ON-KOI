using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    public Transform gameObject;
    Image Image;
    public TextMeshProUGUI text1,text2;
    public bool IsAppear = false;
    private void Awake()
    {
        gameObject = GetComponent<Transform>();
        gameObject.position += new Vector3(500, 0, 0);
        Image = GetComponent<Image>();
        Image.color = new Color(0, 0, 0, 0);
        text1.color = new Color(255, 255, 255, 0);
        text2.color = new Color(255, 255, 255, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsAppear)
        {
            text1.color -= new Color(0, 0, 0,0);
            text2.color -= new Color(0, 0, 0,0);
        }
        else if(IsAppear)
        {
            gameObject.position = new Vector3(0, 0, 0);
            Image.color = new Color(0, 0, 0,255);
            text1.color = new Color(255, 255, 255, 255);
            text2.color = new Color(255, 255, 255, 255);
        }
    }
   public IEnumerator WarningPannul()
    {
        IsAppear = true;
        yield return new WaitForSeconds(5f);
        IsAppear = false;
        yield return new WaitForSeconds(2);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
