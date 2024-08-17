using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool isHorizontal = true; // ������ ���� �Ǵ� �������� �̵����� ����
    public float moveSpeed = 5f;     // �̵� �ӵ�
    public float destroyDelay = 1f;  // ������� ���� �ð�

    public SpriteRenderer SpriteRenderer;

    public bool IsSelected = false;

    [Header("���μ��� ����(üũ�� ����)")]
    public bool Vertical = false;
    private void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        // float v = Input.GetAxisRaw("Vertical");
        // if (!Vertical)
        // {
        //     transform.position += new Vector3(0, v * moveSpeed, 0);
        // }
        // else
        // {
        //     transform.position += new Vector3(h * moveSpeed, 0, 0);
        // }
        if (IsSelected)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Vertical)
                {
                    transform.position += new Vector3(0, moveSpeed, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Vertical)
                {
                    transform.position -= new Vector3(0, moveSpeed, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (!Vertical)
                {
                    transform.position -= new Vector3(moveSpeed, 0, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (!Vertical)
                {
                    transform.position += new Vector3(moveSpeed, 0, 0);
                }
            }
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("");
        if (!IsSelected)
        {
            IsSelected = true;
            SpriteRenderer.color = Color.red;
        }
        else
        {
            IsSelected = false;
            SpriteRenderer.color = Color.white;
        }
    }
}
