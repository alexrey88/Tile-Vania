using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletAsboluteSpeed = 5f;

    Rigidbody2D bulletRigidBody;
    PlayerMovement player;
    AudioPlayer audioPlayer;
    float bulletSpeed;

    void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();

        if (bulletRigidBody == null)
        {
            Debug.LogError("Rigidbody2D not found on the bullet.");
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (player == null)
        {
            Debug.LogError("Player not found.");
        }

        audioPlayer = AudioPlayer.Instance;

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer.Instance not found.");
        }

        audioPlayer.PlayShootingClip();
        bulletSpeed = player.transform.localScale.x * bulletAsboluteSpeed;
    }

    void Update()
    {
        bulletRigidBody.velocity = new Vector2(bulletSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (bulletRigidBody.IsTouchingLayers(LayerMask.GetMask(GameConstants.PlayerLayerName)))
        {
            return;
        }

        if (bulletRigidBody.IsTouchingLayers(LayerMask.GetMask(GameConstants.EnemyLayerName)))
        {
            KillEnemyIfInView(other);
        }

        Destroy(gameObject);
    }

    void KillEnemyIfInView(Collision2D other)
    {
        Vector3 enemyViewportPosition = Camera.main.WorldToViewportPoint(other.gameObject.transform.position);

        if (enemyViewportPosition.x >= 0 && enemyViewportPosition.x <= 1 &&
            enemyViewportPosition.y >= 0 && enemyViewportPosition.y <= 1)
        {
            audioPlayer.PlayEnemyDeadClip();
            Destroy(other.gameObject);
        }
    }
}
