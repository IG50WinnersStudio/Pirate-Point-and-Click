using UnityEngine;


[CreateAssetMenu(fileName = "NewLevel", menuName = "Scriptable Objects/Scene Data/Level")]
public class LevelSO : GameSceneSO
{
    [Header("Level specific")]
    [SerializeField] private bool levelSpecific;
}