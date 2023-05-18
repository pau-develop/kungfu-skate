using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFloatingPoints : MonoBehaviour
{
    private bool isFlashing = false;
    private float blinkTimer = 0;
    private int pointsMoveSpeed = 10;
    private RectTransform rect;
    private Vector2 pointsPos;
    private TextMeshProUGUI text;
    private Color32 originalScoreFontColor = new Color32(0, 108, 250, 255);
    private Color32 altScoreFontColor = new Color32(255, 255, 255, 255);
    private UIText ui;
    private int alpha = 255;
    // Start is called before the first frame update
    void Start(){
        rect = GetComponent<RectTransform>();
        pointsPos = rect.position;
        text = GetComponent<TextMeshProUGUI>();
        ui = GameObject.Find("UI").GetComponent<UIText>();
    }
    // Update is called once per frame
    void Update()
    {
        movePoints();
        textBlinkEffect(text);
        fadeText();
    }

    public void textBlinkEffect(TextMeshProUGUI text){
        blinkTimer += Time.unscaledDeltaTime;
        if(blinkTimer >= 0.05f){
            isFlashing = !isFlashing;
            blinkTimer = 0;
        }
        if(!isFlashing) text.color = originalScoreFontColor;
        else text.color = altScoreFontColor;
    }

    private void fadeText(){
        alpha -= 2;
        if(alpha <= 0) Destroy(this.gameObject);     
        originalScoreFontColor.a = (byte)alpha;
        altScoreFontColor.a = (byte)alpha;
    }

    private void movePoints(){
        pointsPos.y += pointsMoveSpeed * Time.deltaTime;
        rect.position = pointsPos;
    }
}
