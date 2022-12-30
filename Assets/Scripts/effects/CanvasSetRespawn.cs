using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void SetRespawn()
    {
        GameObject.FindObjectOfType<CharSet>().Respawn();
        GetComponent<Animator>().SetBool("Victory", false);
    }
}
