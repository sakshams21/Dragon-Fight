using UnityEngine;

public class Attacks : MonoBehaviour, IAttack
{
    [SerializeField] private AbilitesData Ability_Data;
    public float GetDamageNumber()
    {
        return Ability_Data.Damage;
    }
    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
