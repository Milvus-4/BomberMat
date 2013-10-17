using UnityEngine;
using System.Collections;

public class GenerateScript : MonoBehaviour {

	public GameObject _indestructileBloc;
	public GameObject _destructileBloc;

    public int _probabilityBlock; //Bloquer entre 0 et 100
    public int _probabilityIBlock; //Bloquer entre 0 et 100
	
	private int sizeX;
	private int sizeZ;
	// Use this for initialization
    void Start()
    {
        GameObject[][] map;
        bool isBlock = false;
        bool isIBlock = false;
        //Gérer en fonction de la taille du plateau
        sizeX = 19;
        sizeZ = 10;

        StaticBoard.sizeX = sizeX;
        StaticBoard.sizeZ = sizeZ;

        map = new GameObject[sizeX][];
        StaticBoard.bomb = new GameObject[sizeX][];
        _probabilityBlock = _probabilityBlock > 100 ? 100 : _probabilityBlock < 0 ? 0 : _probabilityBlock;
        _probabilityIBlock = _probabilityIBlock > 100 ? 100 : _probabilityIBlock < 0 ? 0 : _probabilityIBlock;

        //Génération de la map
        for (int i = 0; i < sizeX; i += 1)
        {
            map[i] = new GameObject[sizeZ];
            StaticBoard.bomb[i] = new GameObject[sizeZ];
            for (int j = 0; j < sizeZ; j += 1)
            {

                //Pas de bloc indestructible dans les lignes pairs
                if (i % 2 == 1)
                {
                    isIBlock = Random.Range(0f, 1f) < _probabilityIBlock / 100f;
                }
                if (!isIBlock)
                    isBlock = Random.Range(0f, 1f) < _probabilityBlock / 100f;

                if (isIBlock)
                    map[i][j] = Instantiate(_indestructileBloc, new Vector3((float)i , 0f, (float)j ), Quaternion.identity) as GameObject;
                if (isBlock)
                    map[i][j] = Instantiate(_destructileBloc, new Vector3((float)i, 0f, (float)j), Quaternion.identity) as GameObject;
                isIBlock = false;
                isBlock = false;

            }
        }



        //On stock la map générer dans la map static
        StaticBoard.map = map;
        

        //Les contours sont différents en fonction du mode : 0 = classic, 1 = tetris
        int mode = 1;
        int start = -1;
        if (mode == 1)
            start = 0;
        //Création des contours
        for (int i = start; i <= sizeX; i += 1)
        {
            Instantiate(_indestructileBloc, new Vector3((float)i, 0f, -1f), Quaternion.identity);
            Instantiate(_indestructileBloc, new Vector3((float)i, 0f, sizeZ), Quaternion.identity);
        }
        if (mode == 0)
        {
            for (int i = 0; i < sizeZ; i += 1)
            {
                Instantiate(_indestructileBloc, new Vector3(sizeX, 0f, i), Quaternion.identity);

                Instantiate(_indestructileBloc, new Vector3(-1f, 0f, i), Quaternion.identity);
            }
            destroyCorner();
        }
        else
            destroyCenter();

    }

    // Update is called once per frame
    void Update()
    {
	
	}

    //Détruit les blocs dans les coin pour laisser la place au joueur
    void destroyCorner()
    {
        GameObject[][] map = StaticBoard.map;
        Destroy(map[0][0]);
        Destroy(map[1][0]);
        Destroy(map[0][1]);
        Destroy(map[sizeX-1][sizeZ-1]);
        Destroy(map[sizeX-1][sizeZ-2]);
        Destroy(map[sizeX-2][sizeZ-1]);
    }

    //Détruit les blocs au centre pour laisser la place au joueur
    void destroyCenter()
    {
        GameObject[][] map = StaticBoard.map;
        int centerX = StaticBoard.sizeX/2;
        int centerZ = StaticBoard.sizeZ/2;
        Destroy(map[centerX][centerZ]);
        Destroy(map[centerX-1][centerZ]);
        Destroy(map[centerX+1][centerZ]);
        Destroy(map[centerX][centerZ-1]);
        Destroy(map[centerX][centerZ+1]);
    }
}
