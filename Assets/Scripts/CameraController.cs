using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    [Range(0, 4)]public float velocity = 0.5f;
    public Vector2 offSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        Vector2 target;
        target.x = player.transform.position.x - offSet.x;
        target.y = player.transform.position.y - offSet.y;
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (Vector2.Distance(transform.position, player.transform.position) > 0.10f)
        {
            rb.velocity = new Vector2 (-(transform.position.x - target.x)*velocity, -(transform.position.y - target.y)*velocity/1.2f);
        }
        else{
           rb.velocity = Vector2.zero;
        }
    }
}
