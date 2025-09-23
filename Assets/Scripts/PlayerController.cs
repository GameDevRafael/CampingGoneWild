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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Keybinds();
        
        Movement();

        DebugLogs();
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
