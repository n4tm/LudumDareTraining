using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMarker : MonoBehaviour
{
    // Start is called before the first frame update
    private CircleCollider2D _circleCollider2D;
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private GameObject parent;
    private float timer;
    private bool pressed;
    void Start()
    {
      
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.3f)
        {
            timer = 0;
            _rigidbody2D.velocity = Vector2.zero;
            pressed = false;
            _rigidbody2D.simulated = false;
            parent.transform.position = transform.position;
            transform.position = parent.transform.position;
        }
    }

    public void buttonPressed()
    {
        pressed = true;
    }
    
}
