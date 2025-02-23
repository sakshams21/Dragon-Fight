using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private AnimationController Ref_AnimationController;

    private NavMeshAgent _playerAgent;


    private bool _isFlying;

    private void Awake()
    {
        _playerAgent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        Ref_AnimationController.UpdateAnimation(_playerAgent.velocity.sqrMagnitude, _isFlying);
    }

    public void ToggleFly()
    {
        _isFlying = !_isFlying;
        StartCoroutine(ChangeBaseOffset(_isFlying ? 1f : 0, 0.6f));
    }

    IEnumerator ChangeBaseOffset(float targetValue, float time)
    {
        float startValue = _playerAgent.baseOffset;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            _playerAgent.baseOffset = Mathf.Lerp(startValue, targetValue, elapsedTime / time);
            yield return null;
        }

        _playerAgent.baseOffset = targetValue; // Ensure it reaches the exact value
    }


    public void AssignDestination(Vector3 destination)
    {
        _playerAgent.SetDestination(destination);
    }
}