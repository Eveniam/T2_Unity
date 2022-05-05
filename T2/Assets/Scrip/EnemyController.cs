using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigi;
    public float vida;
    public float velocidad;
    private int a = 0;

    private PuntajeController _controller;
    public void SetController(PuntajeController controller)
    {
        _controller = controller;
    }
    void Start()
    {
        _rigi = GetComponent<Rigidbody2D>();
        _controller = GameObject.Find("PuntajeManager").GetComponent<PuntajeController>();
    }

    
    void Update()
    {
        _rigi.velocity = new Vector2(velocidad, _rigi.velocity.y);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "plataforma")
        {
            velocidad *= -1;
            this.transform.localScale = new Vector2(this.transform.localScale.x * -1,this.transform.localScale.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        a += 1;
        if (collision.gameObject.tag == "Bala" && a == 5)
        {
            Destroy(this.gameObject);
            _controller.IncrementarPuntaje(10);
        }
    }
}
