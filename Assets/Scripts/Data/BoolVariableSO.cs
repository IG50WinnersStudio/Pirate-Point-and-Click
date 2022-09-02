using UnityEngine;


[CreateAssetMenu(fileName = "Bool Var", menuName = "Scriptable Objects/Variables/Bool")]
public class BoolVariableSO : ScriptableObject
{
    [SerializeField] private bool value;

    public bool Value { get => value; set => this.value = value; }
}
