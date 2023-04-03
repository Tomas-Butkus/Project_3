using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceScore : MonoBehaviour
{
    public TMP_Text woodScoreText;
    public TMP_Text rockScoreText;

    public Dictionary<string, int> scoreDictionary;

    private void Start()
    {
        ScoreInitialization();
        UpdateScoreText();
    }

    // Add points to the resource with key
    public void AddPoints(string scoreName, int pointsToAdd)
    {
        if (scoreDictionary.TryGetValue(scoreName, out int value))
        {
            scoreDictionary[scoreName] += pointsToAdd;
            UpdateScoreText();
        }
        else
        {
            Debug.LogError("Dictionary does not contain this resource score key!");
        }
    }

    // Deduct points to the resource with key
    public void DeductPoints(string scoreName, int pointsToDeduct)
    {
        if (scoreDictionary.TryGetValue(scoreName, out int value))
        {
            scoreDictionary[scoreName] = Mathf.Max(0, scoreDictionary[scoreName] - 1);
            UpdateScoreText();
        }
        else
        {
            Debug.LogError("Dictionary does not contain this resource score key!");
        }
    }

    // Update UI text to match the score
    private void UpdateScoreText()
    {
        foreach (KeyValuePair<string, int> score in scoreDictionary)
        {
            string scoreKey = score.Key;
            int scoreValue = score.Value;

            if (scoreKey == "Wood")
            {
                woodScoreText.text = scoreValue.ToString();
            }
            else if (scoreKey == "Rock")
            {
                rockScoreText.text = scoreValue.ToString();
            }
        }
    }

    // Create a dictionary for different score types
    private void ScoreInitialization()
    {
        scoreDictionary = new Dictionary<string, int>();
        scoreDictionary.Add("Wood", 0);
        scoreDictionary.Add("Rock", 0);
    }
}
