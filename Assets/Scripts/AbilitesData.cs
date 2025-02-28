using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Abilites", menuName = "Scriptable Objects/Abilites")]
public class AbilitesData : ScriptableObject
{
    public float Damage;

    public float CoolDown;

    public float Speed;

    public float Duration;

    public bool isAbilityOnCoolDown;

    public DateTime LastUsedTime;
}
