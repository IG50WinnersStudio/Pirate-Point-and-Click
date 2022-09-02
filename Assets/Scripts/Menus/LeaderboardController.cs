using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using System;

public class LeaderboardController : MonoBehaviour
{
    private const int MAX_ENTRIES = 10;
    [SerializeField] private Text[] entries;
    [SerializeField] private Text[] sideInfo;
    [SerializeField] private int leaderboardID;
    //[SerializeField] private InputField PlayerScore;


    private void Start()
    {
        ShowSideInfo();
        UpdatePlayerName();
    }


    private void ShowSideInfo()
    {
        // Display Player Time
        float minutes = PlayerPrefs.GetFloat("minutes");
        float seconds = PlayerPrefs.GetFloat("seconds");
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        sideInfo[0].text = timeText;


        // Display Salvage count
        int salvageCount = PlayerPrefs.GetInt("SalvageCount");

        sideInfo[1].text = salvageCount.ToString();


        // Display Total Cash
        float totalCash = PlayerPrefs.GetFloat("TotalCash");

        sideInfo[2].text = totalCash.ToString();
    }


    private void UpdatePlayerName()
    {
        string playerName = PlayerPrefs.GetString("UserInitials");

        LootLockerSDKManager.SetPlayerName(playerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Update Player name success");
                SubmitScore();
            }
            else
            {
                Debug.Log("Failed to update Player name" + response.Error);
            }
        });
    }


    private void SubmitScore()
    {
        float playerCash = PlayerPrefs.GetFloat("TotalCash") * 100;
        int playerCashInt = (int)playerCash;
        string memberID = PlayerPrefs.GetString("UUID");

        LootLockerSDKManager.SubmitScore(memberID, playerCashInt, leaderboardID, (response) =>
        {
            if (response.success)
            {
                ShowLeaderboard();
                Debug.Log("Submit success");
            }
            else
            {
                Debug.Log("Failed to submit" + response.Error);
            }
        });
    }


    private void ShowLeaderboard()
    {
        LootLockerSDKManager.GetScoreList(leaderboardID, MAX_ENTRIES, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] items = response.items;
                string floatScoreStr = "";

                for (int i = 0; i < items.Length; i++)
                {
                    float floatScore = ((float)items[i].score) / 100;

                    Debug.Log("items.Length - 1: " + (items.Length - 1));
                    Debug.Log("   i   : " + i);

                    if (i == MAX_ENTRIES - 1)
                    {
                        // If the float score has no decimal point, add '.00' to the end
                        if (floatScore % 1 == 0)
                            floatScoreStr = floatScore + ".00";
                        else 
                            floatScoreStr = floatScore + "";

                        entries[i].text = items[i].rank + "          " + items[i].player.name + "         " + floatScoreStr;
                    }
                    else
                    {
                        // If the float score has no decimal point, add '.00' to the end
                        if (floatScore % 1 == 0)
                            floatScoreStr = floatScore + ".00";
                        else
                            floatScoreStr = floatScore + "";

                        entries[i].text = items[i].rank + "           " + items[i].player.name + "         " + floatScoreStr;
                    }
                }

                if (items.Length < MAX_ENTRIES)
                {
                    for (int i = items.Length; i < MAX_ENTRIES; i++)
                    {
                        if (i == MAX_ENTRIES - 1)
                        {
                            entries[i].text = (i + 1).ToString() + "          " + "AAA" + "         " + "0.00";    
                        }
                        else
                        {
                            entries[i].text = (i + 1).ToString() + "           " + "AAA" + "         " + "0.00";
                        }
                    }
                }

            }
            else
            {
                Debug.Log("Failed to show leaderboard" + response.Error);
            }
        });
    }
}