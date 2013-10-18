using UnityEngine;
using System.Collections;

public class MalusManager : MonoBehaviour {

    public GameObject _generate;
    public GameObject _selector;

    private GenerateLineScript generateLineScript;
    private SelectBombScript selectorScript;

    private int nbRoque = 5;
    private static MalusManager instance;
    public static MalusManager Instance
    {
        get { return instance; }
    }


    public GameObject malusText;
    private float timeMalusAnimDurtion = 3f;
    private float time;


	// Use this for initialization
	void Start () {
        generateLineScript = _generate.GetComponent<GenerateLineScript>();
        selectorScript = _selector.GetComponent<SelectBombScript>();
        nbRoque = 6;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (malusText.GetComponent<MeshRenderer>().enabled)
            if (Time.time - time > timeMalusAnimDurtion)
                malusText.GetComponent<MeshRenderer>().enabled = false;
	}

    public void sendMalusToServer()
    {
        networkView.RPC("sendMalus", RPCMode.Server, Network.player.ToString());
    }

    [RPC]
    void sendMalus(string ID)
    {
        int IdSender = StaticBoard.players.IndexOf(ID);
        int IdReceiver = 0;

        if (IdSender == StaticBoard.players.Count-1)
            IdReceiver = 0;
        else
            IdReceiver = IdSender + 1;
        networkView.RPC("chooseMalus", RPCMode.Others, StaticBoard.players[IdReceiver]);

    }

    [RPC]
    void chooseMalus(string ID)
    {
        if (StaticBoard.AmIThisGuy(ID))
        {
            switch ((int)Random.Range(0, 4))
            {
                case 0:
                    addBlocks();
                    break;
                case 1:
                    roquer();
                    break;
                case 2:
                    newLine();
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            malusAnim();
        }
    }

    void malusAnim()
    {
        if (!malusText.GetComponent<MeshRenderer>().enabled)
        {
            malusText.GetComponent<MeshRenderer>().enabled = true;
            time = Time.time;
        }


    }

    void addBlocks()
    {
        int nbBlocks = (int)Random.Range(2, 5);
        int x=(int)Random.Range(0, StaticBoard.sizeX), y=(int)Random.Range(0, StaticBoard.sizeZ);
        for (int i = 0; i < nbBlocks; i += 1) 
            while (StaticBoard.map[x][y] != null)
            {
                x = (int)Random.Range(0, StaticBoard.sizeX);
                y = (int)Random.Range(0, StaticBoard.sizeZ);
            }

        StaticBoard.map[x][y] = Instantiate(Resources.Load("Prefab/IndestructibleBloc"), new Vector3((float)x, 0f, (float)y), Quaternion.identity) as GameObject;
    }

    void roquer()
    {
        selectorScript.setRoque(nbRoque);
    }

    void newLine()
    {
        generateLineScript.CreateLine();
    }


}
