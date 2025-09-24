using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public int coins;
    public int wood;
    public int iron;
    public int rock;
    public int leather;
    public int sticks;

    public void AddCoins(int n) {
        coins += n;
    }

}
