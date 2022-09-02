using LootLocker.Requests;
using System;
using UnityEngine;


public class UserRegistration : MonoBehaviour
{
    private string UUID = "";
    [SerializeField] private TMPro.TMP_InputField userInitials;



    private void Awake()
    {
        UUID = Guid.NewGuid().ToString();
    }


    public void SaveUserInitials()
    {
        PlayerPrefs.SetString("UserInitials", userInitials.text);

        LootLockerSDKManager.StartSession(UUID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Session with LootLocker started");
            }
            else
            {
                Debug.Log("Failed to start session:" + response.Error);
            }
        });
    }
}