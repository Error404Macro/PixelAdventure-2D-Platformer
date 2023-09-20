using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpGround; 

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, run, jump, falling }

    [SerializeField] private AudioSource jumpSoundEffect; 




   private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    
  private  void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); // ����� ������������� �������� �������� �� ��� �

        if (Input.GetButtonDown("Jump") && IsGrounded()) // ��������� ���� �� ������ ������� ������� (Jump � ����� -> space)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // GetComp... �������� ������, � ��� ������ veloacity ���������� ��� � ������������
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState() {

        MovementState state;

        if (dirX > 0f) // ->
        {
            state = MovementState.run;
            sprite.flipX = false;
        }
        else if (dirX < 0f) // <-
        {
            state = MovementState.run;
            sprite.flipX = true; // ���������� ��������� ��� ��������
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f) {
            state = MovementState.jump;
        } // ���� � > 0, �� �� �������
        else if (rb.velocity.y < -.1f) {
            state = MovementState.falling;
        } 

        anim.SetInteger("state", (int)state); // int ����������� ������������ � ����� 0, 1, 2, 3
    }

    private bool IsGrounded() { // ����� ��� ����, ����� ������ ���� �� ������������
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }


}

// �������� ��� dir� ����� -1 � 1. � > 0 = -> ; � < 0 = <-