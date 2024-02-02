using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    
    private Vector2 movement;
    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update() 
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    
    void FixedUpdate() 
    {
        rb.velocity = movement * speed * Time.fixedDeltaTime;
    }
}
