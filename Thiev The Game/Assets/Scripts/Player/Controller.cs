using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.PlayerLoop;
using UnityEngine.Windows;

public class Controller : MonoBehaviour
{
    private PlayerControls pControls;
    private Vector3 playerMovement;
    private Rigidbody rBody;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        pControls = new PlayerControls();
        rBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        pControls.Enable();
        pControls.General.Movement.performed += OnMovement;
        pControls.General.Movement.canceled += OnMovement;
    }

    private void OnDisable()
    {
        pControls.Disable();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray targetPosition = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(targetPosition, out hit))
        {
            Vector3 dir = hit.point - transform.position;
            float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg - 90f;
            Quaternion endRot = Quaternion.AngleAxis(angle, Vector3.down);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRot, 100);
        }

        rBody.velocity = playerMovement * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        playerMovement.x = context.ReadValue<Vector2>().x;
        playerMovement.z = context.ReadValue<Vector2>().y;
    }
}
