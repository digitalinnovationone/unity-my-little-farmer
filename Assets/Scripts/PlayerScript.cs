using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed = 10;

    private Rigidbody thisRigidbody;

    void Awake() {
        thisRigidbody = GetComponent<Rigidbody>();
    }
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        // Create input vector
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        Vector2 movementVector = new Vector2(inputX, inputY);

        // Get forward
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        Quaternion forward = Quaternion.Euler(0, eulerY, 0);

       // Create vector
        Vector3 walkVector = new Vector3(movementVector.x, 0, movementVector.y);
        walkVector = forward * walkVector;
        walkVector *= movementSpeed;

        // Apply input to character
        thisRigidbody.AddForce(walkVector, ForceMode.Force);
    }

}
