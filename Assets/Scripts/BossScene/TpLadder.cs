using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpLadder : MonoBehaviour
{
    public Password Password;
    private GameObject currentEnemy;
    public CamMove CamMove;
    public Transform PlayerPos;
    public bool PlayerEnter, SecondFloor = false;
    // Start is called before the first frame update
    void Start()
    {
        Password = FindAnyObjectByType<Password>();
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemy = GameObject.Find("Enemy");
        if (PlayerEnter)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                if(!SecondFloor)
                {
                    PlayerPos.position += new Vector3 (0, 7, 0);
                    CamMove.minY = 7.5f;
                    CamMove.maxY = 7.5f;
                    SecondFloor = true;
                }
                else
                {
                    PlayerPos.position -= new Vector3(0, 7, 0);
                    CamMove.minY = 0;
                    CamMove.maxY = 0;
                    SecondFloor = false;
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerEnter = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerEnter = false;
        }
    }
}
