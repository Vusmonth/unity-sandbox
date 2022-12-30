using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SocketIO;

public class StartScene : MonoBehaviour
{
    private SocketIOComponent socket;

    public Button startGame;

    void Start()
    {
        socket = GameObject.FindObjectOfType<SocketIOComponent>();
        InvokeRepeating("send", 0.1f, 3f);
        socket.On("boop", ServerOn);
        GameObject.Find("InputName").GetComponent<InputField>().text = PlayerPrefs.GetString("Username");
    }

    void send()
    {
        startGame.interactable = false;
        socket.Emit("beep");
        print("Trying to connect to server");

    }

    void ServerOn(SocketIOEvent e)
    {
        startGame.interactable = true;
        print("Server connected");
    }

    public void SetName(InputField inputName)
    {
        string name = inputName.text;
        PlayerPrefs.SetString("Username", name);
        print("Set nickname to: " + name);
    }

    public void GameScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
