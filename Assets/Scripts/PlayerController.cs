using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public bool isMoving = false;
    public float accelaration = 0f;
    public float accelarationRate = 0.1f;

    [Header("Character Status")]
    public int health = 100;
    public int baseDamage = 25;
    public float afterDamageInvuln = 5f;
    public float debugInvulnTimer = 0f;
    public bool isInvulnerable = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private Vector2 moveInput;
    public Slider healthSlider;
    public Slider oxygenSlider;

    private NPCScript npcScript;
    
    //Internal Variables
    float moveX;
    float moveY;
    float invulnTimer = 0f;
    float oxygenTime = 5f;
    


    
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool isTargetable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        isTargetable = true;
        spriteRend = GetComponent<SpriteRenderer>();
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

        if (isInvulnerable && invulnTimer < afterDamageInvuln)
        {
            invulnTimer += Time.fixedDeltaTime;
            debugInvulnTimer = invulnTimer;
        }
        else
        {
            isInvulnerable = false;
            invulnTimer = 0f;
        }

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

    public void OnCollisionStay2D(Collision2D collision)
    {
        // o motehrfucker só leva dano quando ja tiver acabado o bump time senão ele leva one shot
        if (collision.gameObject.CompareTag("NPC") && !isInvulnerable)
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            npcScript = collision.gameObject.GetComponent<NPCScript>();
            TakeDamage(npcScript.baseDamage);

            
        }
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

    private void TakeDamage(int damage)
    {
        health = (int)Mathf.Clamp(health - damage, healthSlider.minValue, healthSlider.maxValue);
        healthSlider.value = health;
        isInvulnerable = true;

        if (health == 0) {
            spriteRend.enabled = false;
            canMove = false;
            UIController.Instance.deathInterface.SetActive(true);
        }
    }


    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("WaterPool") && Input.GetKeyDown(KeyCode.Space)) {
            isTargetable = false;
            canMove = false;
            spriteRend.enabled = false;
            StartCoroutine(PlayerLeavesFromWaterPool());
        }
    }

    private IEnumerator PlayerLeavesFromWaterPool() {
        yield return new WaitForSeconds(oxygenTime / 5);
        oxygenSlider.value = oxygenSlider.maxValue / 5;
        yield return new WaitForSeconds(oxygenTime / 5);
        oxygenSlider.value = oxygenSlider.maxValue * 2 / 5;
        yield return new WaitForSeconds(oxygenTime / 5);
        oxygenSlider.value = oxygenSlider.maxValue * 3 / 5;
        yield return new WaitForSeconds(oxygenTime / 5);
        oxygenSlider.value = oxygenSlider.maxValue * 4 / 5;
        yield return new WaitForSeconds(oxygenTime / 5);
        oxygenSlider.value = oxygenSlider.maxValue;
        spriteRend.enabled = true;
        isTargetable = true;
        canMove = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("WaterPool")) {
            StopCoroutine(PlayerLeavesFromWaterPool());
            oxygenSlider.value = oxygenSlider.minValue;
        }
    }

}
