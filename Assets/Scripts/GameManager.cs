using Microlight.MicroBar;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] MicroBar PlayerHealth_MicroBar;
    [SerializeField] MicroBar EnemyHealth_MicroBar;


    [SerializeField] private AttackButton[] Abilities;

    float _playerHp = 100f;
    float _enemyHp = 100f;
    void Start()
    {
        PlayerHealth_MicroBar.Initialize(_playerHp);
        EnemyHealth_MicroBar.Initialize(_enemyHp);
    }

    [EasyButtons.Button]
    public void DamagePlayer(float dmgValue)
    {
        _playerHp -= dmgValue;
        if (_playerHp < 0f) _playerHp = 0f;

        PlayerHealth_MicroBar.UpdateBar(_playerHp, false, UpdateAnim.Damage);
    }

    [EasyButtons.Button]
    public void CDTEST()
    {
        Abilities[0].StartCoolDown(4f);
    }
}
