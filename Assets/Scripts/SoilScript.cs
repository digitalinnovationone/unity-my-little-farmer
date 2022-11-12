using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilScript : MonoBehaviour
{
    public bool isWet;
    public float timeToDry = 120;
    public Material materialDry;
    public Material materialWet;

    public int seedIndex;
    private int oldCropStage;
    public int cropStage;
    public GameObject cropObject;

    private MeshRenderer thisMeshRenderer;
    private float dryCooldown = 0;

    private float growInterval = 1;
    private float growCooldown = 0;
    private float growChance = 0.025f;

    void Awake() {
        thisMeshRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dryCooldown = timeToDry;
    }

    // Update is called once per frame
    void Update() {
        // Update material
        thisMeshRenderer.material = isWet ? materialWet : materialDry;

        // Dry soil
        if(isWet) {
            dryCooldown -= Time.deltaTime;
            if(dryCooldown <= 0) {
                isWet = false;
            }
        }

        // Update crops
        if(oldCropStage != cropStage) {

            // Remove crops
            if(cropObject != null) {
                Destroy(cropObject);
            }

            // Plant crops
            if(cropStage > 0) {
                var gm = GameManager.Instance;
                var prefabs = seedIndex == 1 ? gm.beetPrefabs : gm.pumpkinPrefabs;
                var cropPrefab = prefabs[cropStage - 1];

                var position = transform.position;
                var rotation = cropPrefab.transform.rotation
                 * Quaternion.Euler(Vector3.up * Random.Range(0, 360));
                cropObject = Instantiate(cropPrefab, position, rotation);
            }
        }
        oldCropStage = cropStage;

        // Grow crops
        if(!IsEmpty() && !IsFinished()) {
            if((growCooldown -= Time.deltaTime) <= 0) {
                growCooldown = growInterval;
                var realChance = growChance;
                if(isWet) {
                    realChance *= 2f;
                }
                if(Random.Range(0f, 1f) < growChance){
                    cropStage++;   
                }
            }
        }
    }

    public void Water() {
        isWet = true;
        dryCooldown = timeToDry;
    }

    public bool IsEmpty() {
        return cropStage == 0;
    }

    public bool IsFinished() {
        return cropStage == 5;
    }

    public void Seed(int index) {
        if(!IsEmpty()) return;

        // Set vars
        seedIndex = index;
        cropStage = 1;
    }

    public void RemoveCrop() {
        cropStage = 0;
    }
}
