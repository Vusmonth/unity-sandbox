using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    Rigidbody2D rb;

    void Update()
    {
        rb = GetComponent<Rigidbody2D>();

        if (Vector2.Distance(transform.position, player.transform.position) > 0.15f)
        {
            rb.velocity = new Vector2 (-(transform.position.x - player.transform.position.x)*1.8f, -(transform.position.y - player.transform.position.y)*0.5f);
        }
        else{
           rb.velocity = Vector2.zero;
        }
    }
}
