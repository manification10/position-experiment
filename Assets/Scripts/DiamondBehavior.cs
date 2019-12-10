using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondBehavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Store diamond rotation speed
    public float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the trophy at given angles and speed
        transform.Rotate(new Vector3(0, 45, 0) * speed * Time.deltaTime);
    }
}
