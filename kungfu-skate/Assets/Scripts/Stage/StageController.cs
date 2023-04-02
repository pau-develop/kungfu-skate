using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public int[] stagePieces;
    // Start is called before the first frame update
    void Start()
    {
        stagePieces = getPieces();
    }

    private int[] getPieces(){
        int[] tempArray = new int[transform.childCount];
        for(int i = 0; i < tempArray.Length; i++){
            tempArray[i] = transform.GetChild(i).GetComponent<StageScrolling>().sprites.Length;
        }
        return tempArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
