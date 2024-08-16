using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool isHorizontal = true; // 차량이 수평 또는 수직으로 이동할지 설정
    public float moveSpeed = 5f;     // 이동 속도
    public float destroyDelay = 1f;  // 사라지는 지연 시간

    public SpriteRenderer SpriteRenderer;

    public bool IsSelected = false;

    [Header("가로세로 유무(체크가 세로)")]
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
