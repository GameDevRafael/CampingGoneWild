using System.Collections;
using UnityEngine;

public class WaterPoolScript : MonoBehaviour
{
    public PlayerController player;
    private PlayerController pC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pC = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            pC.isTargetable = false;
            pC.canMove = false;
            player.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(PlayerLeavesFromWaterPool());
        }
    }

    private IEnumerator PlayerLeavesFromWaterPool() {
        yield return new WaitForSeconds(5);
        player.GetComponent<SpriteRenderer>().enabled = true;
        pC.isTargetable = true;
        pC.canMove = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StopCoroutine(PlayerLeavesFromWaterPool());
        }
    }
}
