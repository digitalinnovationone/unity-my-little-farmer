using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public int coins;
    public int coinsPerFruit = 5;

    public Transform depositBoxTransform;

    public List<string> items;
    public List<GameObject> itemObjects;

    public List<GameObject> beetPrefabs;
    public List<GameObject> pumpkinPrefabs;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
