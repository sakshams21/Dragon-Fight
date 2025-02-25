using UnityEngine;

[CreateAssetMenu(fileName = "Abilites", menuName = "Scriptable Objects/Abilites")]
public class AbilitesData : ScriptableObject
{
    public float Range;

    public float Damage;

    public float CoolDown;

    public float Speed;
}
