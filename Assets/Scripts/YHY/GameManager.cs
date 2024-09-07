using UnityEngine;
using System.Collections.Generic;

public class CarManager : MonoBehaviour
{
    public static CarManager Instance; // Singleton �ν��Ͻ�
    public List<CarController> cars;    // ���� �ִ� ��� ����
    private CarController selectedCar;  // ���� ���õ� ����

    private void Awake()
    {
        // Singleton ������ �����Ͽ� �ν��Ͻ��� �ߺ����� �ʵ��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ������Ʈ�� �� ��ȯ �ÿ��� �����ǵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ�� ����
        }
    }

    private void Start()
    {
        // ���� �ִ� ��� ������ ã�� ����Ʈ�� ����
        cars = new List<CarController>(FindObjectsOfType<CarController>());
    }

    // ���� ���� ó��
    public void SelectCar(CarController car)
    {
        // ���� ���õ� ������ ������ ���� ����
        if (selectedCar != null && selectedCar != car)
        {
            selectedCar.IsSelected = false;
            selectedCar.SpriteRenderer.color = Color.white;
        }

        // ���� ���õ� ������ ���� ���� �������� ����
        selectedCar = car;
        selectedCar.IsSelected = true;
        selectedCar.SpriteRenderer.color = Color.red;
    }

    private void Update()
    {
        // ���� ���õ� ������ ���� ���, �ش� ������ �̵� ó��
        if (selectedCar != null)
        {
            HandleCarMovement(selectedCar);
        }
    }

    private void HandleCarMovement(CarController car)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && car.Vertical)
        {
            car.transform.position += new Vector3(0, car.moveSpeed, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && car.Vertical)
        {
            car.transform.position -= new Vector3(0, car.moveSpeed, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !car.Vertical)
        {
            car.transform.position -= new Vector3(car.moveSpeed, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !car.Vertical)
        {
            car.transform.position += new Vector3(car.moveSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ���� ������Ʈ�� �������� Ȯ��
        if (collision.gameObject.CompareTag("car"))
        {
            // �浹�� ������ CarController�� ��������
            CarController car = collision.gameObject.GetComponent<CarController>();

            // ������ null�� �ƴ��� Ȯ��
            if (car != null)
            {
                // ������ ���� ó��
                SelectCar(car);
            }
        }
    }
}
