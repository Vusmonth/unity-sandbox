using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(burningTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    PlayerData player;
    IEnumerator burningTime()
    {
        player = transform.GetComponentInParent<PlayerData>();

        player.DamageTake(1);
        yield return new WaitForSeconds(1);
        player.DamageTake(2);
        yield return new WaitForSeconds(1);
        player.DamageTake(2);
        yield return new WaitForSeconds(1);
        player.DamageTake(2);
        yield return new WaitForSeconds(1);
        player.DamageTake(3);
        Destroy(gameObject);
    }
}
