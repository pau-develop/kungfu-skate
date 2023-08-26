using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UICredits : MonoBehaviour
{
    private TextMeshProUGUI creditsText;
    // Start is called before the first frame update
    void Start()
    {
        creditsText = transform.Find("credits").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        updateCredits();
    }

    private void updateCredits(){
        creditsText.text = "CREDITS 0" + GlobalData.playerOneCredits.ToString() ;
    }
}
