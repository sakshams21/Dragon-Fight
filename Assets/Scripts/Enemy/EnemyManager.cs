using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private EnemyAnimationController Ref_AnimationController;
    [SerializeField] private Transform PlayerPosition;
    [Header("Ability 1")]
    [Space(10f)]
    [SerializeField] private AbilitesData FireBreath_Data;
    [SerializeField] private GameObject Firebreath_Go;
    [SerializeField] private GameObject FireBreath_Collider_Go;

    [SerializeField] private float RotationSpeed = 2f;

    [Header("Ability 2")]
    [Space(10f)]
    [SerializeField] private AbilitesData TailSwipe_Data;
    [SerializeField] private GameObject TailSwipe_Go;

    [Header("Ability 3")]
    [Space(10f)]
    [SerializeField] private AbilitesData Tornado_Data;
    [SerializeField] private GameObject Tornado_Go;
    [SerializeField] private GameObject Tornado_Collider_Go;
    [SerializeField] private Transform Tornado_AttackStart_Pos;


    [SerializeField] private float TimeBetweenAttacks = 20f;
    private float _currentTimer = 0;
    private bool _canUseAbility = false;
    private bool _triggerUseAbility = false;
    private bool timerStart = true;



    private void Update()
    {
        if (timerStart)
        {
            _currentTimer += Time.deltaTime;

            if (_currentTimer >= TimeBetweenAttacks)
            {
                _canUseAbility = true;
                _currentTimer = 0;
            }
        }


        if (_canUseAbility && !_triggerUseAbility)
        {
            _canUseAbility = false;
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0)
            {
                Tornado_Attack();
            }
            else
            {
                Breath_Attack();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canUseAbility)
            {
                _canUseAbility = false;
                _triggerUseAbility = true;
                Tail_Attack();
            }
        }

        if (other.CompareTag("Player_Attack"))
        {
            var damageData = other.GetComponent<IAttack>();
            Ref_AnimationController.UpdateAnimation_Hit();
            if (GameManager.Instance.DamageEnemy(damageData.GetDamageNumber()))
            {
                Ref_AnimationController.UpdateAnimation_Dead();
                GameManager.Instance.DeadScreen(false);
                Ref_AnimationController.UpdateAnimation_Hit();
            }

        }

    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canUseAbility)
            {
                _canUseAbility = false;
                Tail_Attack();
            }
        }
    }


    #region TailSwipe

    [EasyButtons.Button]
    public void Tail_Attack()
    {
        timerStart = false;
        Ref_AnimationController.UpdateAnimation_TailAttack();
    }

    public void Tail_Attack_End()
    {
        timerStart = true;
        _triggerUseAbility = false;
    }
    #endregion

    #region Tornado
    [EasyButtons.Button]
    public void Tornado_Attack()
    {
        timerStart = false;

        Ref_AnimationController.UpdateAnimation_TornadoAttack();
    }

    public void Tornado_Attack_Main()
    {
        Tornado_Go.gameObject.SetActive(true);
        Tornado_Collider_Go.SetActive(true);
        Tornado_Go.transform.position = Tornado_AttackStart_Pos.position;
        StartCoroutine(TornadoMove_Coro());
    }

    private IEnumerator TornadoMove_Coro()
    {
        Vector3 dir = (PlayerPosition.position - transform.position).normalized;
        float elapsedTime = 0f;
        while (elapsedTime < Tornado_Data.Duration)
        {
            Tornado_Go.transform.position += dir * Time.deltaTime * Tornado_Data.Speed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Tornado_Go.gameObject.SetActive(false);
        timerStart = true;
    }
    #endregion


    #region FIRE BREATH


    [EasyButtons.Button]
    public void Breath_Attack()
    {
        timerStart = false;
        Firebreath_Go.SetActive(true);
        StartCoroutine(FireBreathAttack_Coro());
        Ref_AnimationController.UpdateAnimation_BreathAttack(true);
    }

    private IEnumerator FireBreathAttack_Coro()
    {
        float elapsedTime = 0f;
        float nextTriggerTime = 1f;
        while (elapsedTime < 5)
        {
            Vector3 direction = PlayerPosition.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate only on the Y-axis
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }
            if (elapsedTime >= nextTriggerTime)
            {
                FireBreath_Collider_Go.SetActive(true);
                nextTriggerTime += 1f; // Schedule the next call
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Firebreath_Go.SetActive(false);
        Ref_AnimationController.UpdateAnimation_BreathAttack(false);
        timerStart = true;
    }
    #endregion

    // void OnDrawGizmos()
    // {
    //     Vector3 origin = transform.position;
    //     Vector3 direction = transform.forward;

    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(origin, SphereCastRadius);

    //     if (Physics.SphereCast(origin, SphereCastRadius, direction, out RaycastHit hit, SphereCastRadius))
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawWireSphere(hit.point, SphereCastRadius);
    //         Gizmos.DrawLine(origin, hit.point);
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.green;
    //         Gizmos.DrawLine(origin, origin + direction * SphereCastRadius);
    //     }
    // }
}
