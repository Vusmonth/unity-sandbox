using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerPanel : MonoBehaviour
{
    private SocketIOComponent socket;
    private InputJson inputData;
    public int id;
    public string nickname;
    public int charset;

    void Start()
    {
        socket = GameObject.FindObjectOfType<SocketIOComponent>();
        inputData = GameObject.FindObjectOfType<InputJson>();
        InvokeRepeating("UptoDate", 0, 1.5f);
        socket.On("SetPlayerPanel", SetupPlayerPanel);
        inst = true;
    }

    bool inst;
    void Update()
    {
        if (id == inputData.id)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(nickname);
    }

    void UptoDate()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = id.ToString();
        data["charset"] = charset.ToString();
        socket.Emit("setPanel", new JSONObject(data));
    }

    Vector2 pos;
    void SetupPlayerPanel(SocketIOEvent e)
    {
        int netId = int.Parse(e.data.GetField("id").ToString().Replace("\"", ""));

        if (netId == id)
        {
            nickname = e.data.GetField("name").ToString().Replace("\"", "");

            pos = new Vector2(
            float.Parse(e.data.GetField("posX").ToString().Replace("\"", "")),
            float.Parse(e.data.GetField("posY").ToString().Replace("\"", ""))
        );
        }
    }
}
