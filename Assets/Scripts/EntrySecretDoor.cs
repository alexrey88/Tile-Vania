using UnityEngine;

public class EntrySecretDoor : MonoBehaviour
{
    [SerializeField] GameObject secretDoorInitialPos;

    PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (player == null)
        {
            Debug.LogError("Player not found.");
        }

        if (secretDoorInitialPos == null)
        {
            Debug.LogError("The field secretDoorInitialPos is null.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.PlayerTag))
        {
            player.transform.position = secretDoorInitialPos.transform.position;
        }
    }
}
