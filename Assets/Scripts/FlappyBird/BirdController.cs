using UnityEngine;

public class BirdController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    //public Sprite[] sprites;
    // private int spriteIndex;
    public int indexPlayer;
    private float strength = 5f;
    private float gravity = -9.81f;
    private float tilt = 5f;
    private bool isFlyUp;
    private Vector3 direction;
    private bool isReverse;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (transform.name == "Player1")
        {
            ReverseGravityController.ReversePlayer1 += ChangerDirection;
            indexPlayer = 0;
            isReverse = false;
        }
        else if (transform.name == "Player2")
        {
            ReverseGravityController.ReversePlayer2 += ChangerDirection;
            indexPlayer = 1;
            isReverse = true;
        }
        else if (transform.name == "Player3")
        {
            ReverseGravityController.ReversePlayer3 += ChangerDirection;
            indexPlayer = 2;
            isReverse = true;
        }
        else if (transform.name == "Player4")
        {
            ReverseGravityController.ReversePlayer4 += ChangerDirection;
            indexPlayer = 3;
            isReverse = false;
        }

        if (isReverse)
        {
            gravity = 9.81f;
        }
    }

    private void ChangerDirection(bool obj)
    {
        isFlyUp = true;
    }

    private void Start()
    {
        // InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (isFlyUp == true)
        {
            if (isReverse)
            {
                direction = Vector3.down * strength;
            }
            else
            {
                direction = Vector3.up * strength;
            }

            isFlyUp = false;
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    // private void AnimateSprite()
    // {
    //     spriteIndex++;
    //
    //     if (spriteIndex >= sprites.Length) {
    //         spriteIndex = 0;
    //     }
    //
    //     if (spriteIndex < sprites.Length && spriteIndex >= 0) {
    //         spriteRenderer.sprite = sprites[spriteIndex];
    //     }
    // }

    
}