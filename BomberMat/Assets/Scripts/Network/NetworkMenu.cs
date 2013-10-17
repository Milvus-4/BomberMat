using UnityEngine;
using System.Collections;

public class NetworkMenu : MonoBehaviour {

    public GameObject networkMaster;

    private GameObject instantiatedMaster;
    private StartNetwork startNetworkScript;
    private string serverIP = "127.0.0.1";
    private int serverPort = 25000;


	void Start () {
	
	}


	void Update () {
	
	}

    void OnGUI()
    {
        int menuSizeX = 460;
        int menuSizeY = 115;
        int menuPosX = 20;
        int menuPosY = Screen.height/2 -menuSizeY/2;
        Rect networkMenu = new Rect(menuPosX, menuPosY, menuSizeX, menuSizeY);
        int sizeButtonX = 250;
        int sizeButtonY = 30;

        //le menu
        GUI.BeginGroup(networkMenu, "");
        GUI.Box(new Rect(0, 0, menuSizeX, menuSizeY), "");

        //champ pour l'adresse IP
        serverIP = GUI.TextField(new Rect(sizeButtonX + 30, 60, 120, 30), serverIP, 40);

        if (GUI.Button(new Rect(10, 20, sizeButtonX, sizeButtonY), "Créer serveur"))
        {
            //Création du serveur
            instantiatedMaster = Instantiate(networkMaster, Vector3.zero, Quaternion.identity) as GameObject;
            startNetworkScript = instantiatedMaster.GetComponent("StartNetwork") as StartNetwork;
            startNetworkScript.isServer = true;
            startNetworkScript.listenPort = serverPort;
        }
        if (GUI.Button(new Rect(10, 60, sizeButtonX, sizeButtonY), "Rejoindre serveur"))
        {
            //Rejoindre serveur
            instantiatedMaster = Instantiate(networkMaster, Vector3.zero, Quaternion.identity) as GameObject;
            startNetworkScript = instantiatedMaster.GetComponent("StartNetwork")as StartNetwork;
            startNetworkScript.isServer = false;
            startNetworkScript.remoteIP = serverIP;
            startNetworkScript.listenPort = serverPort;
        }

        GUI.EndGroup();
    }
}
