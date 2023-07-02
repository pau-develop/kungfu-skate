using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTries : MonoBehaviour
{
    // Start is called before the first frame update
    public void substactTry(){
        if(GlobalData.playerOneTries > 0) GlobalData.playerOneTries--;
    }
}
