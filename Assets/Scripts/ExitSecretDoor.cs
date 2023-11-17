using UnityEngine;

public class ExitSecretDoor : MonoBehaviour
{
    [SerializeField] GameObject entrySecretDoor;

    PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (player == null)
        {
            Debug.LogError("Player not found.");
        }

        if (entrySecretDoor == null)
        {
            Debug.LogError("The field entrySecretDoor is null.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.PlayerTag))
        {
            player.transform.position = entrySecretDoor.transform.position;
            Destroy(entrySecretDoor);
        }
    }
}
