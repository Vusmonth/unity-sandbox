using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuff : MonoBehaviour
{
    public int random;
    public List<GameObject> Buffs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            int colChild = col.transform.childCount;
            for(int i = 0; i < colChild; i++){
                if(col.transform.GetChild(i).tag == "Bless"){
                    Destroy(col.transform.GetChild(i).gameObject);
                }
                else{
                    continue;
                }
            }
            Instantiate(Buffs[random], col.transform);
            Destroy(gameObject);

        }
    }
}
