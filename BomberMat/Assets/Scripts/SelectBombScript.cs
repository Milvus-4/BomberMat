using UnityEngine;
using System.Collections;

public class SelectBombScript : MonoBehaviour {

    public int _size = 5;

    private int[] bombs;
    private GameObject[] bombsObject;
    private int time;

    private int roque=0;

	// Use this for initialization
	void Start () {
        time = (int) Time.time; 
        bombs = new int[_size];
        bombsObject = new GameObject[_size];
        for (int i = 0; i < _size; i += 1)
        {

            //Probabilité d'avoir une pièce
           int piece = (int)Random.Range(0, 100);
           if (piece < 25)
               bombs[i] = (int)StaticBoard.bombType.PAWN;
           else if(piece < 45)
               bombs[i] = (int)StaticBoard.bombType.KING;
           else if (piece < 60)
           {
               if (roque > 0)
               {
                   bombs[i] = (int)StaticBoard.bombType.PAWN;
                   roque-=1;
               }
               else
                   bombs[i] = (int)StaticBoard.bombType.ROOK;
           }
           else if (piece < 75)
               bombs[i] = (int)StaticBoard.bombType.KNIGHT;
           else if (piece < 90)
               bombs[i] = (int)StaticBoard.bombType.BISHOP;
           else
               bombs[i] = (int)StaticBoard.bombType.QUEEN;

            switch (bombs[i])
            {
                case (int)StaticBoard.bombType.BISHOP:
                    bombsObject[i] = (GameObject) Instantiate(Resources.Load("Prefab/selector/Bishop"));
                    break;
                case (int)StaticBoard.bombType.KING:
                    bombsObject[i] = (GameObject) Instantiate(Resources.Load("Prefab/selector/King"));
                    break;
                case (int)StaticBoard.bombType.KNIGHT:
                    bombsObject[i] = (GameObject) Instantiate(Resources.Load("Prefab/selector/Knight"));
                    break;
                case (int)StaticBoard.bombType.QUEEN:
                    bombsObject[i] = (GameObject) Instantiate(Resources.Load("Prefab/selector/Queen"));
                    break;
                case (int)StaticBoard.bombType.PAWN:
                    bombsObject[i] = (GameObject) Instantiate(Resources.Load("Prefab/selector/Pawn"));
                    break;
                case (int)StaticBoard.bombType.ROOK:
                    bombsObject[i] = (GameObject) Instantiate(Resources.Load("Prefab/selector/Rook"));
                    break;
            }
            bombsObject[i].transform.localPosition = new Vector3(i * 4f, -4f, -4f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setRoque(int value)
    {
        roque = value;
    }

    public int GetNextBomb()
    {
        //On récupére la bombe qui arrive
        int bomb = bombs[0];
        Destroy(bombsObject[0]);
        //On en crée une nouvelle
        newBomb();
        //On envoie la bombe
        return bomb;
    }

    void newBomb()
    {
        int i = 0;
        for (; i < _size-1; i += 1)
        {
            bombs[i] = bombs[i+1];
            bombsObject[i] = bombsObject[i+1];
            bombsObject[i].transform.Translate(new Vector3(-4f, 0,0));
        }
        //Plus la partie dure, plus les probabilité d'avoir une piece se valent
        int piece =  (int)Random.Range(0, 100 + ((Time.time - time)/10)*6 );
        if (piece < 25 + ((Time.time - time) / 10))
            bombs[i] = (int)StaticBoard.bombType.PAWN;
        else if (piece < 45 + ((Time.time - time) / 10))
            bombs[i] = (int)StaticBoard.bombType.KING;
        else if (piece < 60 + ((Time.time - time) / 10))
            bombs[i] = (int)StaticBoard.bombType.ROOK;
        else if (piece < 75 + ((Time.time - time) / 10))
            bombs[i] = (int)StaticBoard.bombType.KNIGHT;
        else if (piece < 90 + ((Time.time - time) / 10))
            bombs[i] = (int)StaticBoard.bombType.BISHOP;
        else
            bombs[i] = (int)StaticBoard.bombType.QUEEN;


        switch (bombs[i])
        {
            case (int)StaticBoard.bombType.BISHOP:
                bombsObject[i] = (GameObject)Instantiate(Resources.Load("Prefab/selector/Bishop"));
                break;
            case (int)StaticBoard.bombType.KING:
                bombsObject[i] = (GameObject)Instantiate(Resources.Load("Prefab/selector/King"));
                break;
            case (int)StaticBoard.bombType.KNIGHT:
                bombsObject[i] = (GameObject)Instantiate(Resources.Load("Prefab/selector/Knight"));
                break;
            case (int)StaticBoard.bombType.QUEEN:
                bombsObject[i] = (GameObject)Instantiate(Resources.Load("Prefab/selector/Queen"));
                break;
            case (int)StaticBoard.bombType.PAWN:
                bombsObject[i] = (GameObject)Instantiate(Resources.Load("Prefab/selector/Pawn"));
                break;
            case (int)StaticBoard.bombType.ROOK:
                bombsObject[i] = (GameObject)Instantiate(Resources.Load("Prefab/selector/Rook"));
                break;
        }
        bombsObject[i].transform.localPosition = new Vector3(i * 4f, -4f, -4f);
    }

}
