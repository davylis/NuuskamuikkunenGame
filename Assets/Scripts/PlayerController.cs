using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float movementSpeed;
    [SerializeField] Animator _animator;
    [SerializeField]SpriteRenderer _characterBody;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        HandlePalyerMovement();
    }

    private void HandlePalyerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement = Vector2.ClampMagnitude(movement, 1.0f);
        _rb.velocity = movement * movementSpeed;

        bool characterIsWalking = movement.magnitude > 0f;
        _animator.SetBool("IsWalking", characterIsWalking);

        bool flipSprite = movement.x < 0f;
        _characterBody.flipX = flipSprite;
    }
}
