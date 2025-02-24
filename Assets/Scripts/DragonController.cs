using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class DragonController : MonoBehaviour
{
    [SerializeField] private PlayerManager Ref_PlayerManager;
    [SerializeField] GameObject MoveFeedback_Prefab;
    private Camera _mainCamera;
    private static Controls InputControls;

    void Awake()
    {
        InputControls = new Controls();
        _mainCamera = Camera.main;
    }

    void Start()
    {
        InputControls.KeyBindings.Move.performed += Move;
        InputControls.KeyBindings.Move.Enable();

        InputControls.KeyBindings.Dodge.performed += Fly;
        InputControls.KeyBindings.Dodge.Enable();
    }

    private void Fly(InputAction.CallbackContext context)
    {
        Ref_PlayerManager.ToggleFly();
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            return;

        MoveFeedback_Prefab.SetActive(false);
        MoveFeedback_Prefab.SetActive(true);
        Vector3 pos = new Vector3(hit.point.x, 0.2f, hit.point.z);
        MoveFeedback_Prefab.transform.position = pos;
        Ref_PlayerManager.AssignDestination(hit.point);
    }
}
