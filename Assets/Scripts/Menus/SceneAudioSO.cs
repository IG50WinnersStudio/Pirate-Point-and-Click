using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SceneAudioSO : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip musicClip;
    
    [SerializeField] private GameSceneSO gameSceneSO;


    private void Awake()
    {       
        audioSource = GetComponent<AudioSource>();
        
        if (gameSceneSO.MusicClip != null)
        {
            musicClip = gameSceneSO.MusicClip;

            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.volume = gameSceneSO.MusicVolume;

            audioSource.Play();
        }
    }
}
