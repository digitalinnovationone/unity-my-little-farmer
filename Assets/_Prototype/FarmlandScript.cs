using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmlandScript : MonoBehaviour
{
    public int stage = 0;
    public bool isWet = false;
    public List<GameObject> cropPrefabs;
    public Material materialDry;
    public Material materialWet;

    private GameObject crop;
    private int lastStage;
    private bool wasWet;
    private int maxStage;

    private MeshRenderer thisMeshRenderer;
    
    void Awake() {
        thisMeshRenderer = GetComponent<MeshRenderer>();
    }

    void Start() {
        maxStage = cropPrefabs.Count;
        lastStage=stage;
        wasWet=isWet;
    }

    // Update is called once per frame
    void Update() {
        if(lastStage != stage) {
            lastStage = stage;

            // Update prefab
            Destroy(crop);

            // Create prefab
            if(stage > 0) {
                var prefab = cropPrefabs[stage - 1];
                var rotation = prefab.transform.rotation * Quaternion.Euler(Vector3.up * Random.Range(0, 360));
                crop = Instantiate(prefab, transform.position, rotation);
            }
        }

        if(wasWet != isWet) {
            wasWet = isWet;
            thisMeshRenderer.material = isWet ? materialWet : materialDry;
        }
    }

    public bool IsFullyGrown() {
        return stage > 0 && stage == maxStage;
    }

}
