using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    private float toTheLeft = 50;
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveLeft();
        destroyObject();
    }

    void destroyObject()
    {
        if (transform.position.x < -toTheLeft && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }  

    void moveLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
