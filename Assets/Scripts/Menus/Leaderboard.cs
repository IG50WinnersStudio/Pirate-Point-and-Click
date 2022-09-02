using UnityEngine;
using System.Collections.Generic;


public static class Leaderboard
{
    private static List<LeaderboardEntry> s_Entries;
    public const int EntryCount = 10;


    public struct LeaderboardEntry
    {
        public string name;
        public float cash;

        public LeaderboardEntry(string name, float cash)
        {
            this.name = name;
            this.cash = cash;
        }
    }


    private static List<LeaderboardEntry> Entries
    {
        get 
        {
            if (s_Entries == null)
            {
                s_Entries = new List<LeaderboardEntry>();
                
                LoadLeaderboard();
            }
            return s_Entries;
        }
    }


    private const string PlayerPrefsBaseKey = "leaderboard";

    private static void SortLeaderboard() // sort by cash
    {
        s_Entries.Sort((a, b) => b.cash.CompareTo(a.cash));
    }


    public static void LoadLeaderboard()
    {
        s_Entries.Clear();

        for (int i = 0; i < EntryCount; ++i)
        {
            LeaderboardEntry entry;
            entry.name = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + i + "].name", "AAA");
            entry.cash = PlayerPrefs.GetFloat(PlayerPrefsBaseKey + "[" + i + "].cash", 0);
            s_Entries.Add(entry);
        }

        SortLeaderboard();
    }


    public static void SaveLeaderboard()
    {
        // s_Entries.Clear(); // *** Uncomment this line to reset Leaderboard ***
        for (int i = 0; i < EntryCount; ++i)
        {
            var entry = s_Entries[i];   // PlayerPrefsBaseKey[0].name, PlayerPrefsBaseKey[0].cash
            PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", entry.name);
            PlayerPrefs.SetFloat(PlayerPrefsBaseKey + "[" + i + "].cash", entry.cash);
        }
        PlayerPrefs.Save();
    }


    // Get the LeaderboardEntry struct by index
    public static LeaderboardEntry GetLeaderboardEntry(int index)
    {
        return Entries[index];
    }


    public static void Record(string name, float cash)
    {
        Entries.Add(new LeaderboardEntry(name, cash));
        SortLeaderboard();
        Entries.RemoveAt(Entries.Count - 1);
        SaveLeaderboard();
    }


    public static void Clear()
    {
        for (int i = 0; i < EntryCount; ++i)
        {
            s_Entries[i] = new LeaderboardEntry("AAA", 0);
        }

        SaveLeaderboard();
    }
}