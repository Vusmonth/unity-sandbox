using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class InputJson : MonoBehaviour
{
    public Dictionary<string, string> inputData = new Dictionary<string, string>();
    private SocketIOComponent socket;
    public PlayerData Jogador;

    //[HideInInspector]
    public int id;

    void Start()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        Jogador.globalAxis.x = Input.GetAxis("Horizontal");
        Jogador.globalAxis.y = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Mouse1)){
        Jogador.actionBtt = 1;
        }
        if(Input.GetKeyUp(KeyCode.Mouse1)){
            Jogador.actionBtt = 0;
        }


        if(Input.GetKeyDown(KeyCode.Space)){
        Jogador.jumpBtt = 1;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            Jogador.jumpBtt = 0;
        }

    }
}
