using UnityEngine;
using System.Collections;

public class GenerateLineScript : MonoBehaviour {

    public GameObject _indestructileBloc;
    public GameObject _destructileBloc;

    public int _time;
    private float time;

    private int nbLineCreated;
    private int probabilityBlock;
    private int probabilityIBlock;
    private GameObject[] line;
 
	// Use this for initialization
	void Start () {
        probabilityBlock = 40;
        probabilityIBlock = 0;
        nbLineCreated = 0;
        time = Time.time;
        line = new GameObject[10];
	}
	
	// Update is called once per frame
	void Update () {
        //Toutes les X secondes, une ligne apparait
        if (Time.time - time >= _time)
        {
            CreateLine();
            time = Time.time;
        }
	}

    //Fonction de création de ligne
    void CreateLine()
    {
        if (nbLineCreated > 0)
            MoveMap(ref line);

        //Dès que la probabilité de création d'un bloc est de 80% et de 30% pour les inde, on n'augmente plus...
        if (nbLineCreated < 40)
        {
            probabilityBlock++;
            probabilityIBlock++;
        }
        //...mais on diminue le temps entre chaque ligne
        else
            _time = _time > 1 ? _time-1 : _time;
        bool isBlock = false;
        bool isIBlock = false;
        for (int i = 0; i < StaticBoard.sizeZ; i += 1)
        {
            //Destroy(line[i]);
            if(nbLineCreated%2==0)
               isIBlock = Random.Range(0f, 1f) < probabilityIBlock / 200f;
            if (!isIBlock)
                isBlock = Random.Range(0f, 1f) < probabilityBlock / 100f;

            if (isIBlock)
                line[i] = Instantiate(_indestructileBloc, new Vector3((float)StaticBoard.sizeX, 0f, i), Quaternion.identity) as GameObject;
            if (isBlock)
                line[i] = Instantiate(_destructileBloc, new Vector3((float)StaticBoard.sizeX, 0f, i), Quaternion.identity) as GameObject;
            isIBlock = false;
            isBlock = false;
        }
        nbLineCreated += 1;
    }

    void MoveMap(ref GameObject[] newLine)
    {
        GameObject[] bombs;
        GameObject[] players;

        bombs = GameObject.FindGameObjectsWithTag("bomb");
        players = GameObject.FindGameObjectsWithTag("player");
        int i = 0;

        foreach (GameObject bomb in bombs)
        {
            bomb.transform.localPosition = new Vector3(bomb.transform.localPosition.x - 1, 0, bomb.transform.localPosition.z);
            if (bomb.transform.localPosition.x < 0)
                Destroy(bomb);
        }
        foreach (GameObject player in players)
            player.transform.localPosition = new Vector3(player.transform.localPosition.x - 1, 0.5f, player.transform.localPosition.z);

        //Suppression de la première ligne
        for ( i = 0; i < StaticBoard.sizeZ; i += 1)
        {
            if (StaticBoard.map[0][i] != null)
                Destroy(StaticBoard.map[0][i]);
            //if (StaticBoard.map[StaticBoard.sizeX - 1][i] != null)
              //  Destroy(StaticBoard.map[StaticBoard.sizeX - 1][i]);
        }

        //Décalage dans le tableau des lignes
        for ( i = 0; i < StaticBoard.sizeX-1; i += 1)
        {
            for (int j = 0; j < StaticBoard.sizeZ; j += 1)
            {
                if (StaticBoard.map[i + 1][j] != null && i + 2 == StaticBoard.sizeX)
                    StaticBoard.map[i][j] = Instantiate(StaticBoard.map[i + 1][j]) as GameObject;
                else
                    StaticBoard.map[i][j] = StaticBoard.map[i + 1][j];

                if (StaticBoard.bomb[i + 1][j] != null && i + 2 == StaticBoard.sizeX)
                    StaticBoard.bomb[i][j] = Instantiate(StaticBoard.bomb[i + 1][j]) as GameObject;
                else
                    StaticBoard.bomb[i][j] = StaticBoard.bomb[i + 1][j];
            }
        }
        for (int j = 0; j < StaticBoard.sizeZ; j += 1)
        {
            if (StaticBoard.map[i][j] != null)
                Destroy(StaticBoard.map[i][j]);
        }
        for (int j = 0; j < StaticBoard.sizeZ; j += 1)
        {
            if (newLine[j] != null)
            {
                if (newLine[j].tag == "destructible")
                    StaticBoard.map[i][j] = Instantiate(_destructileBloc, new Vector3((float)StaticBoard.sizeX, 0f, j - 4f), Quaternion.identity) as GameObject;
                else
                    StaticBoard.map[i][j] = Instantiate(_indestructileBloc, new Vector3((float)StaticBoard.sizeX, 0f, j - 4f), Quaternion.identity) as GameObject;
                Destroy(newLine[j]);            
            }
           // else if(StaticBoard.map[StaticBoard.sizeX - 1][i] != null)
             //   Destroy(StaticBoard.map[StaticBoard.sizeX - 1][i]);
        }
        if (StaticBoard.map[i][StaticBoard.sizeZ - 1] != null)
        {
           // Debug.Log(StaticBoard.map[i][StaticBoard.sizeZ - 1].transform.localPosition.x + " " + StaticBoard.map[i][StaticBoard.sizeZ - 1].transform.localPosition.z);
          //  Debug.Log(newLine[StaticBoard.sizeZ - 1].transform.localPosition.x + " " + newLine[StaticBoard.sizeZ - 1].transform.localPosition.z);
        }
        for (i = 0; i < StaticBoard.sizeX; i += 1)
        {
            for (int j = 0; j < StaticBoard.sizeZ; j += 1)
            {
                if (StaticBoard.map[i][j] != null)
                {
                    Vector3 newPos = new Vector3(
                        i,
                        0,
                        j);
                    StaticBoard.map[i][j].transform.localPosition = newPos;
                }
            }
        }
        //Debug.Log(StaticBoard.map[StaticBoard.sizeX - 1][StaticBoard.sizeZ - 1].transform.localPosition.x);

    }
}
