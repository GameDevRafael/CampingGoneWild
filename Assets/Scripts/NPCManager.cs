using System.Collections;
using UnityEngine;

public class NPCManager : MonoBehaviour {
    public static NPCManager Instance;

    public GameObject[] spawners;
    public GameObject[] typesNPC;

    public GameObject player;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(SpawnNPC(Random.Range(0, typesNPC.Length)));

    }



    private IEnumerator SpawnNPC(int type) {
        while (true) {
            Transform currentSpawner = spawners[Random.Range(0, spawners.Length)].transform;
            Instantiate(typesNPC[type], currentSpawner.position, currentSpawner.rotation);
            yield return new WaitForSeconds(3);
        }
    }

}
