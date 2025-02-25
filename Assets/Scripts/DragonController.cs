using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class DragonController : MonoBehaviour
{
    [SerializeField] private PlayerManager Ref_PlayerManager;
    [SerializeField] GameObject MoveFeedback_Prefab;

    [SerializeField]
    private LayerMask layerMask;
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

        InputControls.KeyBindings.Fly.performed += Fly;
        InputControls.KeyBindings.Fly.Enable();

        InputControls.KeyBindings.Ability_1.performed += Fireball;
        InputControls.KeyBindings.Ability_1.Enable();

        InputControls.KeyBindings.Ability_2.performed += TailSwipe;
        InputControls.KeyBindings.Ability_2.Enable();

        InputControls.KeyBindings.Ability_3.performed += FireBreath;
        InputControls.KeyBindings.Ability_3.Enable();
    }

    private void Fly(InputAction.CallbackContext context)
    {
        Ref_PlayerManager.ToggleFly();
    }

    private void FireBreath(InputAction.CallbackContext context)
    {
        //Ref_PlayerManager.ToggleFly();
    }
    private void Fireball(InputAction.CallbackContext context)
    {
        Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, layerMask);
        Vector3 pos = hit.point;
        Ref_PlayerManager.Fire_Fireball(pos);
    }

    private void TailSwipe(InputAction.CallbackContext context)
    {
        Ref_PlayerManager.TailSwipe();
    }

    private void Unknown(InputAction.CallbackContext context)
    {
        //Ref_PlayerManager.ToggleFly();
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, layerMask))
            return;

        MoveFeedback_Prefab.SetActive(false);
        MoveFeedback_Prefab.SetActive(true);
        Vector3 pos = new Vector3(hit.point.x, 0.2f, hit.point.z);
        MoveFeedback_Prefab.transform.position = pos;
        Ref_PlayerManager.AssignDestination(hit.point);
    }
}
