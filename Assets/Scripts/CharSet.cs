using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSet : MonoBehaviour
{
    public GameObject charPrefb;

    public void Instance(int id, string name, Vector2 pos)
    {
        GameObject newChar = Instantiate(charPrefb);
        newChar.GetComponent<PlayerData>().ID = id;
        newChar.GetComponent<PlayerData>().nickname = name;
        newChar.transform.position = pos;
    }

    GameObject[] allActives;
    public List<int> alive;
    void Update()
    {
        allActives = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < allActives.Length; i++)
        {
            if (allActives[i].GetComponent<PlayerData>().life > 0)
            {
                if (!alive.Contains(allActives[i].GetComponent<PlayerData>().ID))
                {
                    alive.Add(allActives[i].GetComponent<PlayerData>().ID);
                }
            }

            if (allActives[i].GetComponent<PlayerData>().life <= 0)
            {
                if (alive.Contains(allActives[i].GetComponent<PlayerData>().ID))
                {
                    alive.Remove(allActives[i].GetComponent<PlayerData>().ID);
                }
            }
        }

        if (alive.Count == 1 && allActives.Length > 1)
        {
            Animator canvasAnim = GameObject.Find("Canvas").GetComponent<Animator>();
            canvasAnim.SetBool("Victory", true);
            Text victoryName = GameObject.Find("Victory name").GetComponent<Text>();

            for (int i = 0; i < allActives.Length; i++)
            {
                if (allActives[i].GetComponent<PlayerData>().ID == alive[0])
                {
                    victoryName.text = allActives[i].GetComponent<PlayerData>().nickname;
                }
            }
        }
    }

    public void Respawn()
    {
        for (int i = 0; i < allActives.Length; i++)
        {
            allActives[i].GetComponent<PlayerData>().life = 100;
        }

    }

}
