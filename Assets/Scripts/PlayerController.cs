using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public bool isMoving = false;
    public float accelaration = 0f;
    public float accelarationRate = 0.1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    float moveX;
    float moveY;

    
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool isTargetable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        isTargetable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove == false) {
            moveX = 0;
            moveY = 0;
            accelaration = 0;
            return;
        }

        Keybinds();
        
        Movement();
    }

    void FixedUpdate()
    {
        ApplyingMovement();
    }

    void Keybinds()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }
    
    void Movement()
    {
        moveInput = new Vector2(moveX, moveY).normalized;

        if (moveInput != Vector2.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            if (accelaration < 1f)
            {
                accelaration += accelarationRate * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (accelaration > 0f)
            {
                accelaration -= accelarationRate * Time.fixedDeltaTime;
            }
        }


    }

    void ApplyingMovement()
    {
        if(isMoving)
        {
            rb.linearVelocity = moveInput * moveSpeed * accelaration;
        } else
        {
            rb.linearVelocity = rb.linearVelocity * accelaration;
        }
    }

    void DebugLogs()
    {
        Debug.Log(accelaration);
        Debug.Log(rb.linearVelocity.magnitude);
    }
}
