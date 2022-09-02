using UnityEngine;
using UnityEngine.UI;

public class LeaderboardGUI : MonoBehaviour
{
    
    private void Awake()
    {
        // Show mouse cursor when in menus
        Cursor.lockState = CursorLockMode.None;
    }


    private void OnGUI()
    {
        //GUI.skin.label.fontSize = 40;
        //GUI.color = Color.yellow;


        //// Display Player Time
        //GUILayout.BeginArea(new Rect((Screen.width / 14) * 1, (Screen.height / 8) * 2, Screen.width, Screen.height));
        
        //GUILayout.Label("TIME COMPLETED");
        //GUILayout.Space(10);

        //float minutes = PlayerPrefs.GetFloat("minutes");
        //float seconds = PlayerPrefs.GetFloat("seconds");

        //string text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //GUILayout.Label("            " + text);
        
        //GUILayout.EndArea();


        //// Display Player salvage count
        //GUILayout.BeginArea(new Rect((Screen.width / 14) * 1, (Screen.height / 8) * 3.5f, Screen.width, Screen.height));

        //GUILayout.Label("BINS COLLECTED");
        //GUILayout.Space(10);

        //int salvageCount = PlayerPrefs.GetInt("SalvageCount");
        //GUILayout.Label("               " + salvageCount);

        //GUILayout.EndArea();


        //// Display Total Cash
        //GUILayout.BeginArea(new Rect((Screen.width / 10) * 1, (Screen.height / 8) * 5, Screen.width, Screen.height));

        //GUILayout.Label("TOTAL CASH");
        //GUILayout.Space(10);

        //GUILayout.Label("       " + PlayerPrefs.GetFloat("TotalCash"));
        
        //GUILayout.EndArea();

    }
}