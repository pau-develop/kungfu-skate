using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    private GameObject debugger;
    private TextMeshPro fpsText;
    // Start is called before the first frame update
    void Start()
    {
        debugger = transform.Find("debugger").gameObject;
        fpsText =  debugger.transform.Find("fps").GetComponent<TextMeshPro>();
        Debug.Log(fpsText);
    }

    // Update is called once per frame
    void Update()
    {
        countFrames();   
    }

    private void countFrames(){
		float fps = (1 / Time.unscaledDeltaTime);
		fpsText.text = "FPS: " + (int)fps;
	}
}
