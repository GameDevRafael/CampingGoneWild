using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public int coins;
    public int wood;
    public int iron;
    public int rock;
    public int leather;
    public int sticks;

    public GameObject[] slots;


    public void AddItemToSlot(GameObject item) {
        foreach(GameObject slot in slots) {
            if(slot.transform.Find("item") == false) {
                Destroy(item);
                Debug.Log("adiciona ao inventario");
            } else {
                Debug.Log("não há espaço");
            }
        }
    }

    public void AddCoins(int n) {
        coins += n;
    }

}
