using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class MainMenu : MonoBehaviour
{
    // List of the scenes to load from Main Menu
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    [SerializeField] private GameObject enterNamePanel;
    //[SerializeField] private GameObject userInitialsGO;
    

    private void Awake()
    {
        if (enterNamePanel.activeSelf == true)
        {
            enterNamePanel.SetActive(false);
        }
    }


    public void StartGameSO()
    {
        // Load the Scene asynchronously in the background
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level1"));
    }


    public void EnableEnterNamePanel()
    {
        if (enterNamePanel.activeSelf == false)
        {
            enterNamePanel.SetActive(true);
        }
    }
    

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}