using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class EndingSummaryManager : MonoBehaviour
{
    [Header("Content")]
    [SerializeField] private Text totalScoreText;      // Optional


    private void Start()
    {
        var library = GameLibrary.Instance;
        if (library == null)
        {
            Debug.LogError("EndingSummaryManager: GameLibrary.Instance not found.");
            return;
        }

        var owned = library.GetOwnedGames();
        if (owned == null || owned.Count == 0)
        {
            //totalScoreText?.SetText("Total Score: 0");
            return;
        }

        float total = 0f;
        foreach(var gameData in owned)
        {
            float currentScore = 0;
            float moneySaved = gameData.originalPrice - gameData.originalPrice * gameData.discount;
            float ratingMultiplier = RatingToMultiplier((int)gameData.rating);
            int seriesCount = 0;
            float seriesCountMultiplier = seriesCountToMultiplier(seriesCount);
            string type = gameData.type;
            int typeCount = 0;
            foreach(var i in owned)
            {
                if (i.type == type)
                {
                    typeCount++;
                }
            }
            float typeMultiplier = typeCountToMultiplier(typeCount);
            currentScore = moneySaved * ratingMultiplier * seriesCountMultiplier * typeMultiplier;
            total += currentScore;
        }
        Debug.Log("Total score is " + total);
        if (totalScoreText)
            totalScoreText.text = $"Total Score: {total:F1}";
    }
    private float RatingToMultiplier(int rating)
    {
        if (rating == 0)
        {
            return 0.01f;
        } else if (rating == 1)
        {
            return 0.1f;
        } else if (rating == 2)
        {
            return 0.3f;
        } else if (rating == 3)
        {
            return 0.5f;
        } else if (rating == 4)
        {
            return 0.7f;
        } else
        {
            return 1f;
        }
    }

    float seriesCountToMultiplier(int count)
    {
        if (count >= 5)
        {
            return 2.0f;
        } else if (count >= 4)
        {
            return 1.5f;
        } else if (count >= 3)
        {
            return 1.2f;
        }
        else
        {
            return 1f;
        }
    }

    float typeCountToMultiplier(int count)
    {
        if(count >= 10)
        {
            return 1.5f;
        } else if (count >= 5) 
        { 
            return 1.3f; 
        } else if (count >= 3)
        {
            return 1.1f;
        }
        else
        {
            return 1;
        }
    }
}