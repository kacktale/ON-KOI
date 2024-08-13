using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public float PlayerSpeed = 5f;
    public float PlayerJump = 5f;
    public bool IsGround = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 PlayerPos = new Vector3(h, 0, 0);
        transform.position += PlayerPos * PlayerSpeed * Time.deltaTime;
    }
    private void Update()
    {
        if (IsGround)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                rigid.AddForce(Vector3.up * PlayerJump, ForceMode2D.Impulse);
                IsGround = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
        }
    }
}
