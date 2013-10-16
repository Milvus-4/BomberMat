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


        map = new GameObject[sizeX][];

        _probabilityBlock = _probabilityBlock > 100 ? 100 : _probabilityBlock < 0 ? 0 : _probabilityBlock;
        _probabilityIBlock = _probabilityIBlock > 100 ? 100 : _probabilityIBlock < 0 ? 0 : _probabilityIBlock;

        //Génération de la map
        for (int i = 0; i < sizeX; i += 1)
        {
            map[i] = new GameObject[sizeZ];
            for (int j = 0; j < sizeZ; j += 1)
            {

                //Pas de bloc indestructible dans les lignes pairs
                if (i % 2 == 1)
                {
                    isIBlock = Random.Range(0f, 1f) < _probabilityIBlock / 100f;
                    Debug.Log(Random.Range(0f, 1f));
                    Debug.Log(1 - _probabilityBlock / 100f);
                }
                if (!isIBlock)
                    isBlock = Random.Range(0f, 1f) < _probabilityBlock / 100f;

                if (isIBlock)
                    map[i][j] = Instantiate(_indestructileBloc, new Vector3(((float)i) - 4f, 0f, ((float)j) - 4f), Quaternion.identity) as GameObject;
                if (isBlock)
                    map[i][j] = Instantiate(_destructileBloc, new Vector3(((float)i) - 4f, 0f, ((float)j) - 4f), Quaternion.identity) as GameObject;
                isIBlock = false;
                isBlock = false;

            }
        }
        //On stock la map générer dans la map static
        StaticBoard.map = map;

        destroyCorner();

        //Les contours sont différents en fonction du mode : 0 = classic, 1 = tetris
        int mode = 0;
        int start = -1;
        if (mode == 1)
            start = 0;
        //Création des contours
        for (int i = start; i <= sizeX; i += 1)
        {
            Instantiate(_indestructileBloc, new Vector3(((float)i) - 4f, 0f, (-5f)), Quaternion.identity);
            Instantiate(_indestructileBloc, new Vector3(((float)i) - 4f, 0f, sizeZ - 4f), Quaternion.identity);
        }
        for (int i = 0; i < sizeZ; i += 1)
        {
            Instantiate(_indestructileBloc, new Vector3(sizeX - 4f, 0f, i - 4f), Quaternion.identity);
            if(mode == 0)
                Instantiate(_indestructileBloc, new Vector3(- 5f, 0f, i - 4f), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
	
	}

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
}
