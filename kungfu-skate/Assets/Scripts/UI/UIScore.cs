using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int latestScore;
    private Color32 originalScoreFontColor = new Color32(0, 108, 250, 255);
    private Color32 altScoreFontColor = new Color32(255, 255, 255, 255);
    [SerializeField] private TMP_FontAsset font;
    // Start is called before the first frame update
    void Start()
    {
        latestScore = GlobalData.playerOneScore;
        scoreText = transform.Find("score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
        checkForScoreChanges();
    }

    private void updateScore(){
        scoreText.text = GlobalData.playerOneScore.ToString() + "$";
    }

    private void checkForScoreChanges(){
        if(latestScore < GlobalData.playerOneScore) StartCoroutine(changeScoreFontColorRoutine());
        latestScore = GlobalData.playerOneScore;
    }

    private IEnumerator changeScoreFontColorRoutine(){
        scoreText.color = altScoreFontColor;
        yield return new WaitForSeconds(0.04f);
        scoreText.color = originalScoreFontColor;
    }
    // Update is called once per frame,
    public void InstantiateFloatingPoints(int points, Vector2 spawnLocation)
    {
        GameObject floatingPoints = new GameObject();
        floatingPoints.AddComponent<TextMeshProUGUI>();
        TextMeshProUGUI floatingPointsText = floatingPoints.GetComponent<TextMeshProUGUI>();
        floatingPointsText.text = points.ToString() + "$";
        floatingPointsText.color = originalScoreFontColor;
        floatingPointsText.fontSize = 8;
        floatingPointsText.font = font;
        RectTransform floatingPointsRect = floatingPoints.GetComponent<RectTransform>();
        floatingPointsRect.position = new Vector2(spawnLocation.x + 100, spawnLocation.y);
        floatingPointsRect.SetParent(GameObject.Find("screen-info").gameObject.transform);
        floatingPoints.AddComponent<UIFloatingPoints>();
    }
}
