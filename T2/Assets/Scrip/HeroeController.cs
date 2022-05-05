using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroeController : MonoBehaviour
{
    public float Jump = 10;
    public float velocity = 10;
    public GameObject BulletPrefab;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    public PuntajeController puntajeManager;

    private static readonly string ANIMATOR_STATE = "Estado";
    private static readonly int ANIMATOR_JUMP = 2;
    private static readonly int ANIMATOR_IDLE = 0;
    private static readonly int ANIMATOR_RUN = 1;
    private static readonly int ANIMATOR_SLICE = 3;
    private static readonly int ANIMATOR_DEAD = 6;
    private static readonly int ANIMATOR_SHOOT = 4;
    private static readonly int ANIMATOR_RUNSHOOT = 5;

    private static readonly int RIGHT = 1;
    private static readonly int LEFT = -1;

    private PuntajeController _controller;
    public void SetController(PuntajeController controller)
    {
        _controller = controller;
    }

    void Start()
    {
        _controller = GameObject.Find("PuntajeManager").GetComponent<PuntajeController>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        ChangeAnimation(ANIMATOR_IDLE);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Desplazarse(RIGHT);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Desplazarse(LEFT);
        }

        if (Input.GetKey(KeyCode.C))
        {
            Deslizarse();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
            ChangeAnimation(ANIMATOR_JUMP);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            Disparar();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            Disparar_Correr();
        }
    }

    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE, animation);
    }
    private void Saltar()
    {
        _rb.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
        ChangeAnimation(ANIMATOR_JUMP);
    }
    private void Deslizarse()
    {
        ChangeAnimation(ANIMATOR_SLICE);
    }
    private void Desplazarse(int position)
    {
        _rb.velocity = new Vector2(velocity * position, _rb.velocity.y);
        _sr.flipX = position == LEFT;
        ChangeAnimation(ANIMATOR_RUN);
    }
    private void Disparar()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        int a = 1;
        ChangeAnimation(ANIMATOR_SHOOT);
        if (_sr.flipX) a = -1;
        var bulletGO = Instantiate(BulletPrefab, new Vector3(x + a, y), Quaternion.identity) as GameObject;
        var controller = bulletGO.GetComponent<BullerController>();
        if (_sr.flipX) controller.velocidad *= -1;
    }
    private void Disparar_Correr()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        int a = 1;
        ChangeAnimation(ANIMATOR_RUNSHOOT);
        if (_sr.flipX) a = -1;
        var bulletGO = Instantiate(BulletPrefab, new Vector3(x + a, y), Quaternion.identity) as GameObject;
        var controller = bulletGO.GetComponent<BullerController>();
        if (_sr.flipX) controller.velocidad *= -1;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Enemy")
        {
            ChangeAnimation(ANIMATOR_DEAD);
            Destroy(this.gameObject,2);
            _controller.DisminuirVida(1);
        }
        //SceneManager.LoadScene(1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Llave")
        {
            Destroy(this.gameObject,1);
            SceneManager.LoadScene(1);
        }
    }
}
