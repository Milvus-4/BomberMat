using UnityEngine;
using System.Collections;
using System;

public class StaticBoard : MonoBehaviour {

    static public GameObject[][] map;
    static public int sizeX;
    static public int sizeZ;
    static public GameObject[][] bomb;


    public static ArrayList players = new ArrayList();

    public enum bombType
    {
        PAWN,
        QUEEN,
        KNIGHT,
        ROOK,
        BISHOP,
        KING
    };

    static public bool AmIThisGuy(string ID)
    {
        return (Network.player.ToString() == ID);
    }
}
