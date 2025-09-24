using UnityEngine;

public class ResourcesScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            switch (gameObject.tag) {
                case "Coin":
                    collision.gameObject.GetComponent<PlayerInventory>().AddCoins(5);
                    Destroy(gameObject);
                    break;
            }

        }
    }

    private void Start() {
        Destroy(gameObject, 5f);
    }
}
