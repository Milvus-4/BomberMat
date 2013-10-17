using UnityEngine;
using System.Collections;
using System;

public class StaticBoard : MonoBehaviour {

    static public GameObject[][] map;
    static public int sizeX;
    static public int sizeZ;
    static public GameObject[][] bomb;


    public static ArrayList players = new ArrayList();

    [Flags]
    public enum bombType
    {
        PAWN,
        QUEEN,
        KNIGHT,
        ROOK,
        BISHOP,
        KING
    };
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
