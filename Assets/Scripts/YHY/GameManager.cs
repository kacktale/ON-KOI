using UnityEngine;
using System.Collections.Generic;

public class CarManager : MonoBehaviour
{
    public static CarManager Instance; // Singleton 인스턴스
    public List<CarController> cars;    // 씬에 있는 모든 차량
    private CarController selectedCar;  // 현재 선택된 차량

    private void Awake()
    {
        // Singleton 패턴을 적용하여 인스턴스가 중복되지 않도록 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 게임 오브젝트가 씬 전환 시에도 유지되도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 오브젝트를 삭제
        }
    }

    private void Start()
    {
        // 씬에 있는 모든 차량을 찾아 리스트에 저장
        cars = new List<CarController>(FindObjectsOfType<CarController>());
    }

    // 차량 선택 처리
    public void SelectCar(CarController car)
    {
        // 현재 선택된 차량이 있으면 선택 해제
        if (selectedCar != null && selectedCar != car)
        {
            selectedCar.IsSelected = false;
            selectedCar.SpriteRenderer.color = Color.white;
        }

        // 새로 선택된 차량을 현재 선택 차량으로 설정
        selectedCar = car;
        selectedCar.IsSelected = true;
        selectedCar.SpriteRenderer.color = Color.red;
    }

    private void Update()
    {
        // 현재 선택된 차량이 있을 경우, 해당 차량만 이동 처리
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
        // 충돌한 게임 오브젝트가 차량인지 확인
        if (collision.gameObject.CompareTag("car"))
        {
            // 충돌한 차량의 CarController를 가져오기
            CarController car = collision.gameObject.GetComponent<CarController>();

            // 차량이 null이 아닌지 확인
            if (car != null)
            {
                // 차량을 선택 처리
                SelectCar(car);
            }
        }
    }
}
