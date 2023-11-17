using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int pointsForCoinPickup = 100;

    public UnityEvent onCoinPickup;
    bool wasCollected = false;

    void Start()
    {
        if (GameSession.Instance != null)
        {
            onCoinPickup.AddListener(() => GameSession.Instance.AddToScore(pointsForCoinPickup));
        }
        else { Debug.LogError("GameSession.Instance not found."); }

        if (AudioPlayer.Instance != null)
        {
            onCoinPickup.AddListener(AudioPlayer.Instance.PlayCoinPickupClip);
        }
        else { Debug.LogError("AudioPlayer.Instance not found."); }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.PlayerTag) && !wasCollected)
        {
            wasCollected = true;
            onCoinPickup.Invoke();
            Destroy(gameObject);
        }
    }
}
