using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnowBoardController : MonoBehaviour
{
    public static event Action PlayerFell;
    public event Action<bool, Vector2> PlayerOnGround;

    [SerializeField] private float tiltForce = 60.0f;

    private SnowBoardInputAction snowBoardInput;
    private InputAction tiltInput;

    Rigidbody2D rb;

    private void Awake()
    {
        snowBoardInput = new SnowBoardInputAction();
    }

    private void OnEnable()
    {
        tiltInput = snowBoardInput.SnowBoard.Tilt;
        tiltInput.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("Ground")) return;

        PlayerFell?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector2 pointOfContact = col.GetContact(0).point;
            PlayerOnGround?.Invoke(true, pointOfContact);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayerOnGround?.Invoke(false, Vector2.zero);
        }
    }

    void FixedUpdate()
    {
        float direction = tiltInput.ReadValue<float>();

        float torqueAmount = -direction * tiltForce;

        rb.AddTorque(torqueAmount);
    }

}
