using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    Rigidbody2D enemyBody;

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();

        if (enemyBody == null)
        {
            Debug.LogError("Rigidbody2D not found on the enemy.");
        }
    }

    void Start()
    {
        if (transform.localScale.x < 0)
        {
            moveSpeed = -moveSpeed;
        }
    }

    void FixedUpdate()
    {
        MoveRightAndLeft();
    }

    void MoveRightAndLeft()
    {
        enemyBody.velocity = new Vector2(moveSpeed, 0f);
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }
}
