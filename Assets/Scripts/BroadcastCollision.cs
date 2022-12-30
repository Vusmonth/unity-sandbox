using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BroadcastCollision : MonoBehaviour
{
    private InputJson dataLocal;
    // Start is called before the first frame update
    void Start()
    {
        dataLocal = GameObject.FindObjectOfType<InputJson>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerData>().ID != dataLocal.id)
        {
            Camera.main.transform.DOShakePosition(.1f, 0.05f, 5, 90);
            transform.parent.parent.parent.GetComponent<PlayerData>().HitCollider(col.gameObject);
        }

    }
}
