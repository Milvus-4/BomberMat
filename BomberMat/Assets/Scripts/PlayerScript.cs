using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public GameObject _selector;
    public float player_speed=3; //vitesse du joueur
    
    private Vector3 lastBombPosition; //position de la dernière bombe posée
    private Vector3 roundedPlayerPosition; //position arroudie du joueur
    private int playerID;
    public GameObject winText;

    private SelectBombScript selectorScript;


	void Start () {
		selectorScript = _selector.GetComponent<SelectBombScript>();
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rigidbody.AddForce(Vector3.forward * Time.deltaTime * player_speed * 100);
            transform.localEulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rigidbody.AddForce(Vector3.back * Time.deltaTime * player_speed * 100);
            transform.localEulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rigidbody.AddForce(Vector3.right * Time.deltaTime * player_speed * 100);
            transform.localEulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rigidbody.AddForce(Vector3.left * Time.deltaTime * player_speed * 100);
            transform.localEulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
        }
        
       

        //mort du joueur
        if (transform.position.x <= -0.5)
        {
            rigidbody.useGravity = true;
            transform.rigidbody.constraints = RigidbodyConstraints.None;
            transform.rigidbody.AddForce(Vector3.left * Time.deltaTime* 200);
            if (transform.position.y < -10)
                Die();
        }


    }
    public void setID(int id)
    {
        playerID = id;
    }
    void DropBomb()
    {
        roundedPlayerPosition = new Vector3((float)((int)(transform.position.x + 0.5)),
                                        0,
                                        (float)((int)(transform.position.z + 0.5)));
        
       if (StaticBoard.bomb[(int)roundedPlayerPosition.x][(int)roundedPlayerPosition.z] == null)//ne pose pas une bombe sur une autre
       {
           GameObject bomb = null;
            //charge le prefab Bomb du type correspondant
           switch (selectorScript.GetNextBomb())
           {
               case (int)StaticBoard.bombType.BISHOP:
                   bomb = (GameObject)Instantiate(Resources.Load("Prefab/Bomb/Bishop"));
                   bomb.transform.position = roundedPlayerPosition;
                   break;
               case (int)StaticBoard.bombType.KING:
                   bomb = (GameObject)Instantiate(Resources.Load("Prefab/Bomb/King"));
                   bomb.transform.position = roundedPlayerPosition;
                   break;
               case (int)StaticBoard.bombType.KNIGHT:
                   bomb = (GameObject)Instantiate(Resources.Load("Prefab/Bomb/Knight"));
                   bomb.transform.position = roundedPlayerPosition;
                   break;
               case (int)StaticBoard.bombType.QUEEN:
                   bomb = (GameObject)Instantiate(Resources.Load("Prefab/Bomb/Queen"));
                   bomb.transform.position = roundedPlayerPosition;
                   break;
               case (int)StaticBoard.bombType.PAWN:
                   bomb = (GameObject)Instantiate(Resources.Load("Prefab/Bomb/Pawn"));
                   bomb.transform.position = roundedPlayerPosition;
                   break;
               case (int)StaticBoard.bombType.ROOK:
                   bomb = (GameObject)Instantiate(Resources.Load("Prefab/Bomb/Rook"));
                   bomb.transform.position = roundedPlayerPosition;
                   break;
           }
           
           if(bomb !=null)
               StaticBoard.bomb[(int)roundedPlayerPosition.x][(int)roundedPlayerPosition.z] = bomb;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "fire")
        {
            Die();
        }
    }

    
    void Die()
    {
        networkView.RPC("IAmDying", RPCMode.Server, Network.player.ToString());

        Destroy(gameObject);
    }

    [RPC]
    void IAmDying(string id)
    {
        if (Network.isServer)
        {
            StaticBoard.players.RemoveAt(StaticBoard.players.IndexOf(id));
            if (StaticBoard.players.Count < 2)
            {
                networkView.RPC("IWon", RPCMode.Others);
            }
        }

    }

    [RPC]
    void IWon()
    {
        winText.GetComponent<MeshRenderer>().enabled=true;
    }

	
	void Update () {
        //poser une bombe
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }
	}

    void OnGUI()
    {
        /*GUI.Box(new Rect(200, 0, 100, 50), "count : " + StaticBoard.players.Count);
        for (int i = 0; i < StaticBoard.players.Count; i += 1)
            GUI.Box(new Rect(0, 50 * i, 100, 50), "players connected : " + StaticBoard.players[i]);*/
    }
}
