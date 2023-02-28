using Pathfinding;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    // Tốc độ di chuyển của player
    public float speed = 5f;

    // Đối tượng point mới được spawn
    public GameObject pointPrefab;

    // Điểm đến hiện tại của player
    private Vector2 currentDestination;

    // Component Rigidbody2D của player
    private Rigidbody2D rigidbody2D;
    private AIDestinationSetter aiDestinationSetter;
    private float timer;
    private Vector3 lastPosition;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        // Đặt mục tiêu đầu tiên cho player
        SetDestination();
    }

    private void Update()
    {
        // Tính khoảng cách giữa vị trí hiện tại của player và mục tiêu
        float distance = Vector2.Distance(transform.position, currentDestination);
        // Nếu khoảng cách nhỏ hơn 0.1, tạo mục tiêu mới
        if (distance < 3f)
        {
            SetDestination();
        }

        FindAnotherWay();
    }

    private void FindAnotherWay()
    {
        timer += Time.deltaTime;

        if (timer >= 0.025f)
        {
            if (lastPosition == transform.position)
            {
                SetDestination();
            }
            timer = 0;
            lastPosition = transform.position;
        }
    }

    private void SetDestination()
    {
        float x = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect,
            Camera.main.orthographicSize * Camera.main.aspect);
        float y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        Vector2 randomPoint = new Vector2(x, y);
        // Tạo một điểm ngẫu nhiên trên màn hình
        // Tạo một pointPrefab tại điểm ngẫu nhiên
        GameObject newPoint = Instantiate(pointPrefab, randomPoint, Quaternion.identity);
        // Đặt mục tiêu mới cho player
        currentDestination = newPoint.transform.position;
        aiDestinationSetter.target = newPoint.transform;
        // Xóa pointPrefab sau khi đã đặt mục tiêu
        Destroy(newPoint, 2f);
    }

    

}