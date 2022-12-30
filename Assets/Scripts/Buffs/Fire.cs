using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
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
        else
        {
            charge = 100;
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
                charge += 0.2f;
            }
        }

        Transform canvas = transform.parent.GetChild(2);
        canvas.GetChild(2).GetComponent<Image>().fillAmount = charge / 100;
    }

    public void ListenHit(Transform a)
    {
        if (charge >= 100)
        {
            print(a.name + " listened");
            if (a.tag == "Player")
            {
                Instantiate(burn, a);
                charge = 0;
            }
        }
    }

    public GameObject burn;

    void OnDestroy()
    {
        Destroy(fairy);
    }

}
