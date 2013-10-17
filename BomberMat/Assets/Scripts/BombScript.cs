using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

    enum bombType
    {
        PAWN,
        QUEEN,
        KNIGHT,
        ROOK,
        BISHOP,
        KING
    };


    public float timeBeforeExplosion = 5f;
    public GameObject _fire;

    private float time;


	// Use this for initialization
	void Start () {
        StaticBoard.bomb[(int)transform.localPosition.x][(int)transform.localPosition.z] = gameObject;
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - time >= timeBeforeExplosion)
        {
            Debug.Log("explosion");
            explosion((int)bombType.KING);
            
        }
	}

    void OnTriggerExit()
    {
        transform.collider.isTrigger = false;
    }

    //Cette fonction sert à créer les patterns des bombes
    void explosion(int type)
    {
        
        int x = (int) transform.localPosition.x;
        int z = (int) transform.localPosition.z;
        switch (type)
        {
            case (int)bombType.PAWN:
                //explosion dans la direction de l'autre joueur ou vers le haut
                break;

            case (int)bombType.KING:
                //petit +
                createFire(x, z);
                createFire(x - 1, z);
                createFire(x + 1, z);
                createFire(x, z - 1);
                createFire(x, z + 1);
                break;

            case (int)bombType.KNIGHT:
                //Huit cases des huit possibilités d'un cavalier
                break;

            case (int)bombType.QUEEN:
                //Gros + et x en même temps
                break;

            case (int)bombType.ROOK:
                //Gros +
                break;

            case (int)bombType.BISHOP:
                //Gros x
                break;

            default :
                break;

        }
        Destroy(gameObject);
    }

    void createFire(int x, int y)
    {
        if (!testCollision(x, y))
        {
            Instantiate(_fire, new Vector3(x, 0, y), Quaternion.identity);
        }
        
    }

    bool testCollision(int x, int y)
    {
        //Il y a collision si on tape contre un loc
        if (x < 0 || y < 0 || x >= StaticBoard.sizeX || y >= StaticBoard.sizeZ)
            return true;
        if (StaticBoard.map[x][y] != null)
        {
            //S'il est destructible... on le détruit
            if (StaticBoard.map[x][y].tag == "destructible")
            {
                Destroy(StaticBoard.map[x][y]);
            }
            return true;
        }
        //Si on touche une autre bombe, explosion en chaine
        if (StaticBoard.bomb[x][y] != null)
        {
            StaticBoard.bomb[x][y].GetComponent<BombScript>().timeBeforeExplosion = 0;
        }
        //Et il y a aussi collision si on est contre un mur
        return false ;

    }
}
