using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _playerAnimator;
    readonly int velocity_AnimatorHash = Animator.StringToHash("Velocity");
    readonly int isFlying_AnimatorHash = Animator.StringToHash("IsFlying");

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void UpdateAnimation(float value, bool isFlying)
    {
        //_playerAnimator.SetLayerWeight(2, isFlying ? 0 : 1);
        //_playerAnimator.SetLayerWeight(3, isFlying ? 1 : 0);
        _playerAnimator.SetBool(isFlying_AnimatorHash, isFlying);
        _playerAnimator.SetFloat(velocity_AnimatorHash, value);
    }

}
