using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugger : MonoBehaviour
{
    public int scrollX;
    private TextMeshPro fpsText;
    private TextMeshPro scrollXText;
    private TextMeshPro timeText;
    private TextMeshPro[] layersText;
    private GameObject stageObject;
    private StageScrolling[] stagePieces;
    public TMP_FontAsset font;
    private float timePassed = 0;
    // Start is called before the first frame update
    void Start()
    {
        fpsText =  transform.Find("fps").GetComponent<TextMeshPro>();
        scrollXText =  transform.Find("scrollX").GetComponent<TextMeshPro>();
        timeText =  transform.Find("time").GetComponent<TextMeshPro>();
        getHoldOfStageLayers();
    }

    void getHoldOfStageLayers(){
        if(GameObject.Find("Stage")!=null){
            stageObject = GameObject.Find("Stage");
            generateTextMesh();
        } 
    }

    void generateTextMesh(){
        int textYPos = 80;
        Vector2 textSize = new Vector2(40,5);
        Vector3 textPosition = new Vector3(120, textYPos, 0);
        stagePieces = new StageScrolling[stageObject.transform.childCount];
        layersText = new TextMeshPro[stageObject.transform.childCount];
        for(int i = 0; i < stageObject.transform.childCount; i++){
            stagePieces[i] = stageObject.transform.GetChild(i).GetComponent<StageScrolling>();
        }
        for(int i = 0; i < stagePieces.Length; i++){
            GameObject tempObject = new GameObject("layer"+i.ToString());
            tempObject.transform.parent = transform;
            textPosition.y = textYPos;
            layersText[i] = tempObject.AddComponent<TextMeshPro>();
            layersText[i].font = font;
            layersText[i].fontSize = 54;
            layersText[i].color = new Color32(0,255,27,255);
            layersText[i].text = "Layer" + i.ToString() + ": ";
            tempObject.GetComponent<RectTransform>().anchoredPosition = textPosition;
            tempObject.GetComponent<RectTransform>().sizeDelta = textSize;
            tempObject.GetComponent<RectTransform>().pivot = new Vector2(0,0);
            textYPos -= 10;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        countFrames();
        displayScrollX();
        if(stageObject != null) {
            displayTimeOnScene();
            updateLayerData();
        }   
    }

    private void updateLayerData(){
        for(int i = 0; i < stagePieces.Length; i++){
            string newText = "layer" + i.ToString() + ": " + stagePieces[i].spritesShifted;
            layersText[i].text = newText;
        }
    }

    private void countFrames(){
		float fps = (1 / Time.unscaledDeltaTime);
		fpsText.text = "FPS:  " + (int)fps;
	}

    private void displayScrollX(){
        scrollXText.text = "xPos: " + scrollX;
    }

    private void displayTimeOnScene(){
        timePassed = stageObject.GetComponent<BackgroundEvents>().timePassed;
        string secondsPassed = ((int)timePassed%60).ToString();
        if(secondsPassed.Length == 1) secondsPassed = "0" + secondsPassed;
        int minutesPassed = (int)Mathf.Round(timePassed/60);
        timeText.text = "Time: " + minutesPassed + ":" + secondsPassed;
    }
}
