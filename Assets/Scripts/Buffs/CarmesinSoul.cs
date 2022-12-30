using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmesinSoul : MonoBehaviour
{
    public GameObject skull;

    GameObject skullEffect;
    void Start()
    {
        skullEffect = Instantiate(skull);

        //SpriteRenderer energyBar = transform.parent.GetChild(2).GetComponent<SpriteRenderer>();
        //energyBar.size = new Vector2(0 , 0.3f);

    }

    void Update()
    {
        skullEffect.transform.position = transform.parent.position;

    }

    public void ListenHit(Transform a)
    {
        print(a.name + " listened");
        if (a.tag == "Player")
        {
            if (a.GetComponent<PlayerData>().life >= 12)
            {
                if (transform.parent.GetComponent<PlayerData>().life <= 94){
                    transform.parent.GetComponent<PlayerData>().life += 6;
                }
            }
        }
    }

    void OnDestroy(){
        Destroy(skullEffect);
    }
}
