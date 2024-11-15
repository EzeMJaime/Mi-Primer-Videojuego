using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad = 3f;

    private Rigidbody2D jugadorRb;
    private Vector2 moveInput;
    private Animator jugadorAnimator;

    void Start()
    {
        jugadorRb = GetComponent<Rigidbody2D>();
        jugadorAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        jugadorAnimator.SetFloat("Horizontal", moveX);
        jugadorAnimator.SetFloat("Vertical", moveY);
        jugadorAnimator.SetFloat("Velocidad", moveInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        jugadorRb.MovePosition(jugadorRb.position + moveInput * velocidad * Time.fixedDeltaTime);
    }
}
