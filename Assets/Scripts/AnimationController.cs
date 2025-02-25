using DG.Tweening;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private GameObject Tail_Vfx;

    private Animator _playerAnimator;
    readonly int velocity_AnimatorHash = Animator.StringToHash("Velocity");
    readonly int isFlying_AnimatorHash = Animator.StringToHash("IsFlying");

    readonly int fireball_AnimatorHash = Animator.StringToHash("FireBall");
    readonly int tailSwipe_AnimatorHash = Animator.StringToHash("TailSwipe");

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public bool IsMovementAnimationIsPlaying()
    {
        return _playerAnimator.GetCurrentAnimatorStateInfo(1).IsTag("MOVEMENT");
    }

    public void UpdateAnimation(float value, bool isFlying)
    {
        _playerAnimator.SetFloat(velocity_AnimatorHash, value);
    }

    public void UpdateAnimation_Fly(bool fly_status)
    {
        _playerAnimator.SetBool(isFlying_AnimatorHash, fly_status);
    }

    public void UpdateAnimation_Fireball()
    {
        _playerAnimator.SetTrigger(fireball_AnimatorHash);
    }

    public void UpdateAnimation_TailSwipe()
    {
        _playerAnimator.SetTrigger(tailSwipe_AnimatorHash);
    }

    public void Tail_VFX_Enable()
    {
        Tail_Vfx.SetActive(false);
        Tail_Vfx.SetActive(true);
    }

    public void Tail_VFX_Disable()
    {
        DOVirtual.DelayedCall(0.4f, () => { Tail_Vfx.SetActive(false); });
    }

}
