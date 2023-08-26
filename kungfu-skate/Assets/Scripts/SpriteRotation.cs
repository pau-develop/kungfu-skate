using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    private Transform[] childTransforms;
    private int rotationSpeed = 300;
    private bool frontRotation = false;
    private bool backRotation = false;
    private int currentRotationValue = 25;
    // Start is called before the first frame update
    void Start()
    {
        getChildTransforms();
    }

    private void getChildTransforms(){
        childTransforms = new Transform[transform.childCount];
        for(int i = 0; i < childTransforms.Length; i++){
            childTransforms[i] = transform.GetChild(i).transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<CharacterMovement>().isAlive){
            if(GetComponent<CharacterMovement>().rampedUp) rotateTransforms(1, 25);
            else if(GetComponent<CharacterMovement>().rampedDown) rotateTransforms(-1, 335);
            else {
                if(frontRotation) resetTransforms(-1);
                if(backRotation) resetTransforms(1);
            }   
        } else {
            hardResetTransforms();
        }
    }

    private void hardResetTransforms(){
        childTransforms[0].localEulerAngles = new Vector3(0, 0, 0);
    }

    private void resetTransforms(int direction){
        for(int i = 0; i < childTransforms.Length; i++){
            if(childTransforms[i].localEulerAngles.z > 0 && childTransforms[i].localEulerAngles.z < 350)
                childTransforms[i].Rotate(0, 0, (rotationSpeed * direction) * Time.deltaTime);
            else {
                childTransforms[i].localEulerAngles = new Vector3(0, 0, 0);
                frontRotation = false;
                backRotation = false;
            }
        }
    }

    

    private void rotateTransforms(int direction, int valueToAchieve){
        for(int i = 0; i < childTransforms.Length; i++){
            if(direction == 1){
                if(childTransforms[i].localEulerAngles.z < valueToAchieve) 
                    childTransforms[i].Rotate(0, 0, (rotationSpeed * direction) * Time.deltaTime);
                else {
                    childTransforms[i].localEulerAngles = new Vector3(0, 0, valueToAchieve);
                    frontRotation = true;
                }    
            } else {
                if(childTransforms[i].localEulerAngles.z > valueToAchieve || childTransforms[i].localEulerAngles.z == 0) 
                    childTransforms[i].Rotate(0, 0, (rotationSpeed * direction) * Time.deltaTime);
                else {
                    childTransforms[i].localEulerAngles = new Vector3(0, 0, valueToAchieve);
                    backRotation = true;
                }
            }
        }
    }
}
