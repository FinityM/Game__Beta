using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float leftLimit = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveLeft();
        destroyObject();
    }

    // Method for the left limit
    void destroyObject()
    {
        if (transform.position.x < -leftLimit && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }  

    // Method for the objects to move left
    void moveLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
