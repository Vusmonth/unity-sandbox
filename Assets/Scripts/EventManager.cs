using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    private SocketIOComponent socket;
    public string nickname;
    public List<int> allPlayers;
    public List<string> allNames;
    public List<int> allChar;

    public GameObject charPrefb;

    void Start()
    {
        nickname = PlayerPrefs.GetString("Username");
        socket = GetComponent<SocketIOComponent>();
        socket.On("setId", SetId);
        socket.On("QueweFind", QueweFind);
        socket.On("SetPlayerPanel", updateChar);

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Invoke("NUC", 0.1f);
        }
    }

    private Dictionary<string, string> playerData = new Dictionary<string, string>();
    void NUC()
    {
        playerData["name"] = nickname;
        socket.Emit("newPlayer", new JSONObject(playerData));
    }

    void SetId(SocketIOEvent e)
    {
        if (GetComponent<InputJson>().id == 0)
        {
            GetComponent<InputJson>().id = int.Parse(e.data.GetField("id").ToString().Replace("\"", ""));
            PlayerPrefs.SetInt("id", int.Parse(e.data.GetField("id").ToString().Replace("\"", "")));
        }
    }

    void QueweFind(SocketIOEvent e)
    {
        allPlayers[0] = int.Parse(e.data.GetField("player0").ToString().Replace("\"", ""));
        allPlayers[1] = int.Parse(e.data.GetField("player1").ToString().Replace("\"", ""));

        allNames[0] = e.data.GetField("name").ToString().Replace("\"", "");
        allNames[1] = e.data.GetField("name1").ToString().Replace("\"", "");

    }

    void updateChar(SocketIOEvent e)
    {
        allChar[0] = int.Parse(e.data.GetField("charset").ToString().Replace("\"", ""));
        allChar[1] = int.Parse(e.data.GetField("charset1").ToString().Replace("\"", ""));
    }

    public void EmitData(Dictionary<string, string> data, string name)
    {
        socket.Emit(name, new JSONObject(data));
    }

    public void Instance()
    {
        for (int i = 0; i < 4; i++)
        {
            if (allPlayers[i] != 0)
            {
                GameObject newChar = Instantiate(charPrefb);
                newChar.GetComponent<PlayerData>().ID = allPlayers[i];
                newChar.GetComponent<PlayerData>().nickname = allNames[i];
            }
        }
    }

}
