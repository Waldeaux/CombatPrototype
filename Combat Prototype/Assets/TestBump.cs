using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBump : MonoBehaviour
{
    Rigidbody rb;
    public float magnitude;
    public Vector3 moveVector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            rb.AddForce((moveVector).normalized * magnitude, ForceMode.Impulse);
        }
    }
}
