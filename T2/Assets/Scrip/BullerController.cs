using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BullerController : MonoBehaviour
{

    private Rigidbody2D _rb;

    public float velocidad = 10;
    private PuntajeController _controller;

    public void SetController(PuntajeController controller)
    {
        _controller = controller;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controller = GameObject.Find("PuntajeManager").GetComponent<PuntajeController>();

        Destroy(this.gameObject, 2);
    }

    void Update()
    {
        _rb.velocity = new Vector2(velocidad, _rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var tag = col.gameObject.tag;
        if (tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }


}
