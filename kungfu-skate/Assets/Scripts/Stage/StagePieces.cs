using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePieces : MonoBehaviour
{
    private Dictionary<string, int[]> stageOneLayerZeroPieces = new Dictionary<string, int[]>(){
        {"road", new[]{0}},
        {"alley", new[]{1, 2, 3, 4}},
    };
    private Dictionary<string, int[]> stageOneLayerOnePieces = new Dictionary<string, int[]>(){
        {"road", new[]{0}},
        {"alley-entrance", new[]{1}},
        {"alley-exit", new[]{2}},
        {"alley", new []{3, 4, 5, 6}},
        {"park-entrance", new[]{7}},
        {"park-exit", new[]{8}},
        {"park", new[]{9}}
    };
    private Dictionary<string, int[]> stageOneLayerTwoPieces = new Dictionary<string, int[]>(){
        {"road", new[]{0, 1, 2, 3, 4}},
        {"alley-entrance", new[]{5}},
        {"alley-exit", new[]{6}},
        {"alley", new []{7}},
        {"park-entrance", new[]{7}},
        {"park-exit", new[]{8}},
        {"park", new[]{9}}
    };

    public List<Dictionary<string,int[]>> dictionaries = new List<Dictionary<string,int[]>>();
    
    void Start(){
        dictionaries.Add(stageOneLayerZeroPieces);
        dictionaries.Add(stageOneLayerOnePieces);
        dictionaries.Add(stageOneLayerTwoPieces);
    }
}
