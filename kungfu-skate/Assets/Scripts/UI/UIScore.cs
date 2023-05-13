using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = transform.Find("score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GlobalData.playerOneScore.ToString() + "$";
    }
}
