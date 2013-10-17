using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

    

    enum direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST,
        SOUTH_EAST,
        SOUTH_WEST,
        NORTH_EAST,
        NORTH_WEST
    }



    public float timeBeforeExplosion = 5f;
    public GameObject _fire;

    private float time;
    public int _type;

    private bool show = false;


	// Use this for initialization
	void Start () {
        if(!show)
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - time >= timeBeforeExplosion && !show)
        {
            explosion(_type);

            Destroy(gameObject);
        }
	}

    void OnTriggerExit()
    {
        transform.collider.isTrigger = false;
    }

    public void setShow(bool value)
    {
        show = value;
    }

    //Cette fonction sert à créer les patterns des bombes
    void explosion(int type)
    {
        
        int x = (int) transform.localPosition.x;
        int z = (int) transform.localPosition.z;
        switch (type)
        {
            case (int)StaticBoard.bombType.PAWN:
                //explosion dans la direction de l'autre joueur ou vers le haut
                longFire(x, z, (int)direction.NORTH, 2);
                break;

            case (int)StaticBoard.bombType.KING:
                //petit +
                createFire(x, z);
                createFire(x - 1, z);
                createFire(x + 1, z);
                createFire(x, z - 1);
                createFire(x, z + 1);
                createFire(x - 1, z - 1);
                createFire(x + 1, z - 1);
                createFire(x - 1, z + 1);
                createFire(x + 1, z + 1);
                break;

            case (int)StaticBoard.bombType.KNIGHT:
                //Huit cases des huit possibilités d'un cavalier
                createFire(x, z);
                createFire(x - 1, z - 2);
                createFire(x - 1, z + 2);
                createFire(x - 2, z - 1);
                createFire(x - 2, z + 1);
                createFire(x + 1, z + 2);
                createFire(x + 1, z - 2);
                createFire(x + 2, z - 1);
                createFire(x + 2, z + 1);
                break;

            case (int)StaticBoard.bombType.QUEEN:
                //Gros + et x en même temps
                longFire(x, z, (int)direction.NORTH, 7);
                longFire(x, z, (int)direction.SOUTH, 7);
                longFire(x, z, (int)direction.EAST, 7);
                longFire(x, z, (int)direction.WEST, 7);
                longFire(x, z, (int)direction.NORTH_WEST, 7);
                longFire(x, z, (int)direction.NORTH_EAST, 7);
                longFire(x, z, (int)direction.SOUTH_EAST, 7);
                longFire(x, z, (int)direction.SOUTH_WEST, 7);
                break;

            case (int)StaticBoard.bombType.ROOK:
                //Gros +
                longFire(x, z, (int)direction.NORTH, 7);
                longFire(x, z, (int)direction.SOUTH, 7);
                longFire(x, z, (int)direction.EAST, 7);
                longFire(x, z, (int)direction.WEST, 7);
                break;

            case (int)StaticBoard.bombType.BISHOP:
                //Gros x
                longFire(x, z, (int)direction.NORTH_WEST, 7);
                longFire(x, z, (int)direction.NORTH_EAST, 7);
                longFire(x, z, (int)direction.SOUTH_EAST, 7);
                longFire(x, z, (int)direction.SOUTH_WEST, 7);
                break;

            default :
                break;

        }
    }

    void createFire(int x, int y)
    {
        if (!testCollision(x, y))
        {
            Instantiate(_fire, new Vector3(x, 0, y), Quaternion.identity);
        }   
    }

    void longFire(int x, int y, int dir, int range)
    {

        
        if (!testCollision(x, y) && range > 0)
        {
           
            Instantiate(_fire, new Vector3(x, 0, y), Quaternion.identity);
            switch (dir)
            {
                case (int)direction.NORTH :
                    longFire(x + 1, y, dir, range - 1);
                    break;
                case (int)direction.SOUTH :
                    longFire(x - 1, y, dir, range - 1);
                    break;
                case (int)direction.EAST:
                    longFire(x, y+1, dir, range - 1);
                    break;
                case (int)direction.WEST:
                    longFire(x, y-1, dir, range - 1);
                    break;
                case (int)direction.NORTH_EAST:
                    longFire(x + 1, y+1, dir, range - 1);
                    break;
                case (int)direction.NORTH_WEST:
                    longFire(x + 1, y-1, dir, range - 1);
                    break;
                case (int)direction.SOUTH_EAST:
                    longFire(x - 1, y+1, dir, range - 1);
                    break;
                case (int)direction.SOUTH_WEST:
                    longFire(x - 1, y-1, dir, range - 1);
                    break;
            }
        }  
    }

    bool testCollision(int x, int y)
    {
        //Debug.Log("stop les bugs là !!");
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
