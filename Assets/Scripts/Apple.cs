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
            ReverseGravityOnPlayer();
            Destroy(gameObject);
        }
    }

    void ReverseGravityOnPlayer()
    {
        Vector2 newLocalScale = new Vector2(player.transform.localScale.x, -player.transform.localScale.y);
        player.transform.localScale = newLocalScale;
        playerRigidBody.gravityScale = -playerRigidBody.gravityScale;
    }
}
