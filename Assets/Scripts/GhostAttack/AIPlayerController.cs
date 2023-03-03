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
        if (aiDestinationSetter.target == null)
        {
            SetDestination();
        }

        FindAnotherWay();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enviroment") || collision.transform.CompareTag("Player"))
            SetDestination();
    }

    private void FindAnotherWay()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            int x1 = Mathf.FloorToInt(lastPosition.x);
            int y1 = Mathf.FloorToInt(lastPosition.y);
            int z1 = Mathf.FloorToInt(lastPosition.z);

            int x2 = Mathf.FloorToInt(transform.position.x);
            int y2 = Mathf.FloorToInt(transform.position.y);
            int z2 = Mathf.FloorToInt(transform.position.z);

            if (x1 != x2 || y1 != y2 || z1 != z2)
            {
                Debug.Log("newpos");
                //SetDestination();
            }

            timer = 0;
            lastPosition = transform.position;
        }
    }

    private void SetDestination()
    {
        if (pointPrefab == null) return;
        float x = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect,
            Camera.main.orthographicSize * Camera.main.aspect);
        float y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        Vector2 randomPoint = new Vector2(x, y);
        // Tạo một điểm ngẫu nhiên trên màn hình
        // Tạo một pointPrefab tại điểm ngẫu nhiên
        GameObject newPoint = Instantiate(pointPrefab, randomPoint, Quaternion.identity);
        // Đặt mục tiêu mới cho player
        currentDestination = newPoint.transform.position;
        if (aiDestinationSetter != null) aiDestinationSetter.target = newPoint.transform;
        // Xóa pointPrefab sau khi đã đặt mục tiêu
        Destroy(newPoint, 5f);
    }
}