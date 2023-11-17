using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : Singleton<GameSession>
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] List<Image> heartImages = new List<Image>();
    [SerializeField] int scoreValue = 0;

    void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        foreach (Image heartImage in heartImages)
        {
            heartImage.gameObject.SetActive(true);
        }
        scoreText.text = scoreValue.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Image hearthToRemove = heartImages[playerLives];
        hearthToRemove.gameObject.SetActive(false);
    }

    void ResetGameSession()
    {
        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();

        if (scenePersist != null)
        {
            scenePersist.ResetScenePersist();
        }

        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddToScore(int pointsToAdd)
    {
        scoreValue += pointsToAdd;
        scoreText.text = scoreValue.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
}
