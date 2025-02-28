using DG.Tweening;
using Microlight.MicroBar;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] MicroBar PlayerHealth_MicroBar;
    [SerializeField] MicroBar EnemyHealth_MicroBar;

    [SerializeField] private CanvasGroup DeadScreen_CanvasGroup;
    [SerializeField] private TextMeshProUGUI DeadScreen_Text;



    [SerializeField] private AttackButton[] Abilities;

    float _playerHp = 100f;
    float _enemyHp = 100f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        PlayerHealth_MicroBar.Initialize(_playerHp);
        EnemyHealth_MicroBar.Initialize(_enemyHp);
    }

    [EasyButtons.Button]
    public bool DamagePlayer(float dmgValue)
    {
        _playerHp -= dmgValue;
        if (_playerHp < 0f)
        {
            _playerHp = 0f;
            return true;
        }

        PlayerHealth_MicroBar.UpdateBar(_playerHp, false, UpdateAnim.Damage);
        return false;
    }

    [EasyButtons.Button]
    public bool DamageEnemy(float dmgValue)
    {
        _enemyHp -= dmgValue;
        if (_enemyHp < 0f)
        {
            _enemyHp = 0f;
            return true;
        }


        EnemyHealth_MicroBar.UpdateBar(_enemyHp, false, UpdateAnim.Damage);
        return false;
    }

    public void DeadScreen(bool isPlayerDead)
    {
        DeadScreen_CanvasGroup.alpha = 0;
        DeadScreen_CanvasGroup.gameObject.SetActive(true);
        DeadScreen_CanvasGroup.DOFade(1f, 1f);
        DeadScreen_Text.text = isPlayerDead ? "Better luck next time :(" : "YOU DID IT!!";
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    [EasyButtons.Button]
    public void CDTEST()
    {
        Abilities[0].StartCoolDown(4f);
    }

    public void AbilityCoolDown(int index, float cooldownValue)
    {
        Abilities[index].StartCoolDown(cooldownValue);
    }
}
