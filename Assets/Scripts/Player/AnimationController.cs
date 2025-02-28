using DG.Tweening;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private GameObject Tail_Vfx;
    [SerializeField] private GameObject Collider_Go;

    private Animator _playerAnimator;
    readonly int _velocity_AnimatorHash = Animator.StringToHash("Velocity");
    readonly int _isFlying_AnimatorHash = Animator.StringToHash("IsFlying");

    readonly int _fireball_AnimatorHash = Animator.StringToHash("FireBall");
    readonly int _tailSwipe_AnimatorHash = Animator.StringToHash("TailSwipe");
    readonly int _Dead_AnimatorHash = Animator.StringToHash("isDead");
    readonly int _hit_AnimatorHash = Animator.StringToHash("Hit");


    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public bool IsMovementAnimationIsPlaying()
    {
        return _playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("MOVEMENT");
    }

    public void UpdateAnimation(float value, bool isFlying)
    {
        _playerAnimator.SetFloat(_velocity_AnimatorHash, value);
    }

    public void UpdateAnimation_Fly(bool fly_status)
    {
        _playerAnimator.SetBool(_isFlying_AnimatorHash, fly_status);
    }

    public void UpdateAnimation_Fireball()
    {
        _playerAnimator.SetTrigger(_fireball_AnimatorHash);
    }
    public void UpdateAnimation_Dead()
    {
        _playerAnimator.SetBool(_Dead_AnimatorHash, true);
    }
    public void UpdateAnimation_Hit()
    {
        _playerAnimator.SetTrigger(_hit_AnimatorHash);
    }
    public void UpdateAnimation_TailSwipe()
    {
        _playerAnimator.SetTrigger(_tailSwipe_AnimatorHash);
    }

    public void Tail_VFX_Enable()
    {
        Tail_Vfx.SetActive(false);
        Tail_Vfx.SetActive(true);
        Collider_Go.SetActive(true);
    }

    public void Tail_VFX_Disable()
    {
        DOVirtual.DelayedCall(0.4f, () => { Tail_Vfx.SetActive(false); });
    }

}
