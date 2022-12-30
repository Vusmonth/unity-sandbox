using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Freeze : MonoBehaviour
{
    public float charge;
    public GameObject Fairy;

    GameObject fairy;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.tag == "Player")
        {
            Vector3 instPos = transform.parent.position;

            charge = 100;
            fairy = Instantiate(Fairy, instPos, new Quaternion(0, 0, 0, 0));
            fairy.GetComponent<Fairy>().player = transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
        if (rb.velocity.x != 0)
        {
            if (charge < 100)
            {
                charge += 0.15f;
            }
        }

        Transform canvas = transform.parent.GetChild(2);
        canvas.GetChild(2).GetComponent<Image>().fillAmount = charge / 100;
    }

    public Color FrozeColor;
    Transform target;
    public GameObject freezeEffect;

    public void ListenHit(Transform a)
    {
        if (a.tag == "Player")
        {
            if (a.GetComponent<PlayerData>().enabled == false)
            {
                unFreeze();
            }
        }

        if (charge >= 100)
        {

            print(a.name + " listened");
            if (a.tag == "Player")
            {
                target = a;
                charge = 0;
                Instantiate(freezeEffect, a);
                a.GetComponent<SpriteRenderer>().color = FrozeColor;
                a.GetComponent<PlayerData>().enabled = false;
                a.GetComponent<Animator>().enabled = false;
                Invoke("unFreeze", 3);
            }
        }
    }

    void unFreeze()
    {
        target.GetComponent<SpriteRenderer>().color = Color.white;
        target.GetComponent<PlayerData>().enabled = true;
        target.GetComponent<Animator>().enabled = true;
    }

    void OnDestroy()
    {
        Destroy(fairy);
    }

}
