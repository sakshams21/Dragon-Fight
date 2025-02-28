using System;
using System.Collections;
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
    private Rigidbody _rb;
    private Vector3 _targetPosition;
    private bool _shouldMove;
    public static bool _anotherAnimationPlaying;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_shouldMove || _anotherAnimationPlaying) return;
        Vector3 direction = (_targetPosition - transform.position).normalized;
        direction.y = 0;
        transform.position += direction * (_isFlying ? MoveSpeed * 2 : MoveSpeed) * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * 10f);
        Ref_AnimationController.UpdateAnimation(1, _isFlying);

        if (Vector3.Distance(transform.position, _targetPosition) < 2f)
        {
            Ref_AnimationController.UpdateAnimation(0, _isFlying);
            _shouldMove = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy_Attack"))
        {
            Ref_AnimationController.UpdateAnimation_Hit();
            if (GameManager.Instance.DamagePlayer(other.GetComponent<IAttack>().GetDamageNumber()))
            {
                Ref_AnimationController.UpdateAnimation_Dead();
                GameManager.Instance.DeadScreen(true);
            }
        }
    }

    public static void AnimationPlayingStatusUpdate(bool status)
    {
        _anotherAnimationPlaying = status;
    }

    public void Fire_Fireball(Vector3 direction)
    {
        if ((DateTime.Now - Fireball_Data.LastUsedTime).TotalSeconds < Fireball_Data.CoolDown) return;
        _shouldMove = false;
        Vector3 dir = (direction - transform.position).normalized;
        dir.y = 0;
        GameManager.Instance.AbilityCoolDown(0, Fireball_Data.CoolDown);
        Fireball_Data.LastUsedTime = DateTime.Now;
        StartCoroutine(RotateTowardsTargetAndFire(dir));
    }

    private IEnumerator RotateTowardsTargetAndFire(Vector3 targetDirection)
    {
        float rotationSpeed = 10f;

        // Rotate player smoothly
        while (Vector3.Dot(transform.forward, targetDirection) < 0.99f)
        {
            transform.forward = Vector3.Lerp(transform.forward, targetDirection, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        transform.forward = targetDirection;

        // Fire Fireball
        Fireball_Attack_Go.gameObject.SetActive(true);
        Fireball_Attack_Go.transform.position = Fireball_Start_Pos.position;
        Fireball_Attack_Go.transform.forward = targetDirection;
        Fireball_Attack_Go.linearVelocity = targetDirection * Fireball_Data.Speed;

        // Stop movement & trigger animation
        Ref_AnimationController.UpdateAnimation_Fireball();
        Ref_AnimationController.UpdateAnimation(0, _isFlying);
    }



    public void TailSwipe()
    {
        if ((DateTime.Now - TailSwipe_Data.LastUsedTime).TotalSeconds < TailSwipe_Data.CoolDown && _isFlying) return;
        _shouldMove = false;
        GameManager.Instance.AbilityCoolDown(1, TailSwipe_Data.CoolDown);
        TailSwipe_Data.LastUsedTime = DateTime.Now;
        Ref_AnimationController.UpdateAnimation(0, _isFlying);
        Ref_AnimationController.UpdateAnimation_TailSwipe();
    }

    public void ToggleFly()
    {
        _isFlying = !_isFlying;
        _rb.useGravity = !_isFlying;

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