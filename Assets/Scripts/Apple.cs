using UnityEngine;

public class Apple : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody2D playerRigidBody;
    bool hasCollided = false;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (player != null)
        {
            playerRigidBody = player.GetComponent<Rigidbody2D>();

            if (playerRigidBody == null)
            {
                Debug.LogError("Player Rigidbody not found.");
            }
        }
        else
        {
            Debug.LogError("Player not found.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.PlayerTag) && !hasCollided)
        {
            hasCollided = true;
            Destroy(gameObject);
            ReverseGravityOnPlayer(other);
        }
    }

    void ReverseGravityOnPlayer(Collider2D other)
    {
        Vector2 newLocalScale = new Vector2(other.transform.localScale.x, -other.transform.localScale.y);
        other.transform.localScale = newLocalScale;
        playerRigidBody.gravityScale = -playerRigidBody.gravityScale;
    }
}
