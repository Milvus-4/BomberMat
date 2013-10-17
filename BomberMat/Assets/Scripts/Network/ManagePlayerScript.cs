using UnityEngine;
using System.Collections;

public class ManagePlayerScript : MonoBehaviour
{

    public float waitingTime = 30f;
    private float time;

    public GameObject cmpt;
    private TextMesh tm;

    void Start()
    {
        tm = (TextMesh)cmpt.GetComponent<TextMesh>();
    }
    void OnGUI()
    {
        /*GUI.Box(new Rect(0, 0, 100, 50),"id player : "+ Network.player.ToString() );
        GUI.Box(new Rect(0, 50, 100, 50), "temps : " + (Time.time - time));
        GUI.Box(new Rect(200, 0, 100, 50), "count : " + StaticBoard.players.Count);
        for (int i = 0; i < StaticBoard.players.Count; i += 1)
            GUI.Box(new Rect(100, 50 * i, 100, 50), "players connected : " + StaticBoard.players[i]);*/

    }

    void Update()
    {
        if (Time.time - time > waitingTime && Network.connections.Length >= 2)
        {
            networkView.RPC("LaunchGame", RPCMode.All);
        }
        //compteur de secondes d'attentes
        int sec = (int)(waitingTime - Time.time - time);
        tm.text = Mathf.Clamp(sec, 0, waitingTime).ToString();
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        if (Network.connections.Length > 1)
        {
            time = Time.time;
        }
        StaticBoard.players.Add(player.ToString());
    }

    [RPC]
    void LaunchGame()
    {
        Application.LoadLevel("scene2");
    }
}
