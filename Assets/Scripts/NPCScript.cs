using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    private GameObject player;
    private PlayerController pC;
    private Rigidbody2D rb;
    private Slider slider;

    private GameObject[] resourcesOnDrop;
    public GameObject coin;

    private int speed = 3;
    private int health = 100;
    private int damagePlayer = 25; // isto é provisório, n vai ficar assim, é so para levar dano por agora para testar

    private int speedBump = 5;
    private Vector2 bumpVelocity;
    private bool isBumped;
    private float isBumpedMaxTime = 0.5f;
    private float bumpTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = NPCManager.Instance.player;
        pC = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        slider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (isBumped) {
            rb.MovePosition(rb.position + bumpVelocity * Time.fixedDeltaTime);

        } else {
            if (pC.isTargetable) {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);
            }

        }
    }

    private void Update() {
        if (isBumped) {
            bumpTimer -= Time.deltaTime;

            if (bumpTimer <= 0f) {
                isBumped = false;
            }
            return;

        }
    }

    public void OnCollisionStay2D(Collision2D collision) {
        // o motehrfucker só leva dano quando ja tiver acabado o bump time senão ele leva one shot
        if (collision.gameObject.CompareTag("Player") && isBumped == false) {
            TakeDamage();
        }
    }

    private void TakeDamage() {
        health = (int)Mathf.Clamp(health - damagePlayer, slider.minValue, slider.maxValue);
        slider.value = health;

        if (health == 0) {
            Instantiate(coin, transform.position, transform.rotation);
            Destroy(gameObject);
            return; // just in case
        }

        Vector2 bumpAwayDirection = -(player.transform.position - transform.position).normalized;
        bumpVelocity = bumpAwayDirection * speedBump;

        isBumped = true;
        bumpTimer = isBumpedMaxTime;
    }

}
