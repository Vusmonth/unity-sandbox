using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private SocketIOComponent socket;
    private InputJson inputData;
    public GameObject charPrefb;

    void Start()
    {
        socket = GameObject.FindObjectOfType<SocketIOComponent>();
        inputData = GameObject.FindObjectOfType<InputJson>();

        socket.On("QueweFind", QueweFind);
    }

    public void StartQuewe()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = inputData.id.ToString();
        socket.Emit("onQuewe", new JSONObject(data));
        StartCoroutine(countTime());
    }

    public void ExitQuewe()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = inputData.id.ToString();
        socket.Emit("offQuewe", new JSONObject(data));
        StopCoroutine(countTime());
    }

    public GameObject QueuePanel;
    public GameObject LobbyPanel;

    void QueweFind(SocketIOEvent e)
    {
        print("Quewe finded " + e.data.GetField("player0") + " / " + e.data.GetField("player1"));
        QueuePanel.SetActive(false);
        LobbyPanel.SetActive(true);

        Transform anchor = LobbyPanel.transform.GetChild(0);
        anchor.GetChild(0).GetComponent<PlayerPanel>().id = int.Parse(e.data.GetField("player0").ToString().Replace("\"", ""));
        anchor.GetChild(1).GetComponent<PlayerPanel>().id = int.Parse(e.data.GetField("player1").ToString().Replace("\"", ""));
        StopCoroutine(countTime());
    }

    public void GameScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        Invoke("RemoveScene", 0.1f);
    }

    void RemoveScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        GameObject.FindObjectOfType<EventManager>().Instance();
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public int timeSec;
    public int timeMin;

    IEnumerator countTime()
    {
        while (true)
        {
            string timerStr = (timeMin + ":" + timeSec.ToString("D2"));
            QueuePanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(timerStr);
            if (timeSec < 60)
            {
                yield return new WaitForSeconds(1);
                timeSec++;
            }
            else
            {
                timeMin++;
                timeSec = 0;
            }
        }

    }

}
