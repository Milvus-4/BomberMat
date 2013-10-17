using UnityEngine;
using System.Collections;

public class StartNetwork : MonoBehaviour {

    public bool isServer;
    public int listenPort = 25000; //port d'écoute du serveur
    public string remoteIP; //adresse IP du serveur

    void Awake()
    {
        //ne pas détruire le script et le gameObject au chargement
        DontDestroyOnLoad(this);
    }

	void Start () {
        
        // si c'est le serveur, l'initialiser
        if (isServer)
        {
            Network.InitializeSecurity();
            Network.InitializeServer(8, listenPort, false); //TODO limiter le nb de joueur selon le monde

            // prévenir tous les objets que le réseau est lancé
            foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
                go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
        }
        // sinon connecter les clients au server
        else
        {
            Network.Connect(remoteIP, listenPort);
        }

        Application.LoadLevel("waitingRoom");
	}
	
	
    void OnPlayerConnected(NetworkPlayer player)
    {
        if(isServer)
        {
            Debug.Log("Connecté !");
        }
    }


    
    void OnLevelWasLoaded()
    {
        if ( Application.loadedLevelName == "Diapo")
        Destroy(this.gameObject);
        // prévenir tous les objets que le réseau et le jeu sont lancés
        foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
    }


	void Update () {
	
	}
}
