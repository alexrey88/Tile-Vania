using UnityEngine;

public class Bounce : MonoBehaviour
{
    AudioPlayer audioPlayer;

    void Start()
    {
        audioPlayer = AudioPlayer.Instance;

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer.Instance not found.");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.PlayerTag))
        {
            audioPlayer.PlayBouncingClip();
        }
    }
}
