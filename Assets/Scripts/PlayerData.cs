using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public float life; 
    public string nickname;
    public int ID;
    public Vector2 globalAxis;
    public int actionBtt, jumpBtt;

    [Space]
    [Range(0, 3)] public float mvSpeed = 1;
    [Range(0, 3)] public float jumpForce = 2.1f;
    public Vector2 externForces;

    float gravity;
    public bool grounded;
    public Animator animator;

    private Transform canvas;
    Rigidbody2D rb;

    void Update()
    {
        //usa coroutine pra resovler esse lixo
        print("Grounded = " + grounded);

        canvas = transform.GetChild(2);
        canvas.GetChild(1).GetComponent<Image>().fillAmount = life / 100;
        canvas.GetChild(3).GetComponent<Text>().text = nickname;

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetFloat("Air", rb.velocity.y);
        isGrounded();
        animator.SetFloat("Life", life);

        if (grounded == false)
        {
            gravity -= 0.1f;
            animator.SetBool("Grounded", false);
        }
        else
        {
            animator.SetBool("Grounded", true);
            gravity = 0;
            if (jumpBtt == 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (life > 0)
        {
            movement();
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }

        SpriteRenderer lifebar = transform.GetChild(1).GetComponent<SpriteRenderer>();

        InputJson jEvent = GameObject.FindObjectOfType<InputJson>();

        if (ID == jEvent.id)
        {
            Camera.main.GetComponent<CameraController>().player = gameObject;
        }

    }

    void movement()
    {
        if (globalAxis.x < 0)
        {
            transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            canvas.localScale = new Vector2(0.003f, 0.003f);
            animator.SetBool("Walk", true);
            // transform.GetChild(2).localScale = new Vector3(-1, 1, 1);
        }
        if (globalAxis.x > 0)
        {
            transform.localScale = new Vector3(-0.06f, 0.06f, 1);
            canvas.localScale = new Vector2(-0.003f, 0.003f);
            animator.SetBool("Walk", true);
            // transform.GetChild(2).localScale = new Vector3(1, 1, 1);
        }
        if(globalAxis.x == 0){
            animator.SetBool("Walk", false);
        }

        rb = GetComponent<Rigidbody2D>();
        Vector2 teste = new Vector2(globalAxis.x * mvSpeed, rb.velocity.y);
        rb.velocity = (teste + externForces);

        if (actionBtt == 1)
        {
            mvSpeed = 0;
            animator.SetBool("Atack", true);
        }
        else
        {
            mvSpeed = 1;
        }

        Dictionary<string, string> currentPos = new Dictionary<string, string>();
        currentPos["posX"] = transform.position.x.ToString();
        currentPos["posY"] = transform.position.y.ToString();
        currentPos["id"] = ID.ToString();

        EventManager EM = GameObject.FindObjectOfType<EventManager>();
        EM.EmitData(currentPos, "setPos");
    }

    public Vector2 offSet;
    public float radius;
    Vector2 overlapPosition;
    void isGrounded()
    {

        overlapPosition.x = transform.position.x + offSet.x;
        overlapPosition.y = transform.position.y + offSet.y;

        Collider2D[] overlap = Physics2D.OverlapCircleAll(overlapPosition, radius);

        for (int i = 0; i < overlap.Length; i++)
        {
            if (overlap[i].gameObject.layer == 9)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
        if (overlap.Length < 1)
        {
            grounded = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(overlapPosition, radius);
    }

    public void atackBool()
    {
        animator.SetBool("Atack", false);
    }

    public void HitCollider(GameObject a)
    {
        BroadcastMessage("ListenHit", a.transform);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Damage")
        {
            InputJson jEvent = GameObject.FindObjectOfType<InputJson>();

            animator.SetTrigger("Damage");
            if (life > 0)
            {
                life -= 12;
            }
            if (life <= 0)
            {
                life = 0;
                animator.SetTrigger("Die");
            }
        }
    }

    public void DamageTake(int q)
    {
        animator.SetTrigger("Damage");
        if (life > 0)
        {
            life -= q;
        }

        if (life <= 0)
        {
            life = 0;
        }
    }
}
