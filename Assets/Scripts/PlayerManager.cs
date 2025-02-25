using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private AnimationController Ref_AnimationController;
    [SerializeField] private float MoveSpeed = 3.5f;
    [SerializeField] private Rigidbody Fireball_Attack_Go;
    [SerializeField] private Transform Fireball_Start_Pos;




    [SerializeField] private AbilitesData Fireball_Data;
    [SerializeField] private AbilitesData Fireball_Empowered_Data;
    [SerializeField] private AbilitesData TailSwipe_Data;
    [SerializeField] private AbilitesData TailSwipe_Empowered_Data;
    [SerializeField] private AbilitesData Fly;
    private bool _isFlying;

    private Vector3 _targetPosition;
    private bool _shouldMove;
    public static bool _anotherAnimationPlaying;

    private void Update()
    {
        if (!_shouldMove || _anotherAnimationPlaying) return;
        Vector3 direction = (_targetPosition - transform.position).normalized;
        direction.y = 0;
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * 10f);

        Ref_AnimationController.UpdateAnimation(1, _isFlying);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.5f)
        {
            Ref_AnimationController.UpdateAnimation(0, _isFlying);
            _shouldMove = false;
        }

    }

    public static void AnimationPlayingStatusUpdate(bool status)
    {
        _anotherAnimationPlaying = status;
    }

    public void Fire_Fireball(Vector3 direction)
    {
        Vector3 dir = (direction - Fireball_Start_Pos.position).normalized;
        Fireball_Attack_Go.gameObject.SetActive(true);

        Fireball_Attack_Go.transform.position = Fireball_Start_Pos.position;
        Fireball_Attack_Go.linearVelocity = dir * Fireball_Data.Speed;
        _shouldMove = false;
        Ref_AnimationController.UpdateAnimation_Fireball();
        Ref_AnimationController.UpdateAnimation(0, _isFlying);
    }

    public void TailSwipe()
    {
        _shouldMove = false;
        Ref_AnimationController.UpdateAnimation(0, _isFlying);
        Ref_AnimationController.UpdateAnimation_TailSwipe();
    }

    public void ToggleFly()
    {
        _isFlying = !_isFlying;
        Ref_AnimationController.UpdateAnimation_Fly(_isFlying);
        StartCoroutine(ChangeBaseOffset(_isFlying ? 1f : 0, 0.6f));
    }

    IEnumerator ChangeBaseOffset(float targetValue, float time)
    {
        float startValue = transform.position.y;
        float elapsedTime = 0f;

        Vector3 tempPos = transform.position;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            tempPos = transform.position;
            tempPos.y = Mathf.Lerp(startValue, targetValue, elapsedTime / time);
            transform.position = tempPos;
            yield return null;
        }

        tempPos.y = targetValue; // Ensure it reaches the exact value
        transform.position = tempPos;
    }


    public void AssignDestination(Vector3 destination)
    {
        if (!Ref_AnimationController.IsMovementAnimationIsPlaying()) return;
        _shouldMove = true;
        _targetPosition = destination;
    }
}