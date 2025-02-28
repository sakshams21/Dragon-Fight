using UnityEngine;
using DG.Tweening;
public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private EnemyManager Ref_EnemyManager;
    [SerializeField] private GameObject Tail_Vfx;
    private Animator _enemyAnimator;

    readonly int _breath_AnimationHash = Animator.StringToHash("FireBreath");
    readonly int _dead_AnimationHash = Animator.StringToHash("isDead");
    readonly int _tornado_AnimationHash = Animator.StringToHash("Tornado");
    readonly int _tail_AnimationHash = Animator.StringToHash("TailSwipe");
    readonly int _hit_AnimationHash = Animator.StringToHash("Hit");
    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
    }

    public void UpdateAnimation_Dead()
    {
        _enemyAnimator.SetBool(_dead_AnimationHash, true);
    }

    public void UpdateAnimation_Hit()
    {
        _enemyAnimator.SetTrigger(_hit_AnimationHash);
    }

    public void UpdateAnimation_TailAttack()
    {
        _enemyAnimator.SetTrigger(_tail_AnimationHash);
    }

    public void UpdateAnimation_TornadoAttack()
    {
        _enemyAnimator.SetTrigger(_tornado_AnimationHash);
    }

    public void UpdateAnimation_BreathAttack(bool status)
    {
        _enemyAnimator.SetBool(_breath_AnimationHash, status);
    }
    public void Tail_VFX_Enable()
    {
        Tail_Vfx.SetActive(false);
        Tail_Vfx.SetActive(true);
    }

    public void TornadoEnable()
    {
        Ref_EnemyManager.Tornado_Attack_Main();
    }

    public void Tail_VFX_Disable()
    {
        DOVirtual.DelayedCall(0.4f, () =>
        {
            Tail_Vfx.SetActive(false);
            Ref_EnemyManager.Tail_Attack_End();
        });
    }

}
