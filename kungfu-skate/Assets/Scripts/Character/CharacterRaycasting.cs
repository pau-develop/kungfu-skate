using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRaycasting : MonoBehaviour
{
    [SerializeField] private int leftRayXPos;
    [SerializeField] private int rightRayXPos;
    private int rayLength = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doTheRayCasting(leftRayXPos);
        doTheRayCasting(rightRayXPos);
    }

    private void doTheRayCasting(int rayOrigin){
        Vector2 rayPos = new Vector2(transform.position.x + rayOrigin, transform.position.y + 5);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayLength);
        Debug.DrawRay(rayPos, Vector2.down * rayLength, Color.green);
        if(hit.collider != null) Debug.Log(hit.collider);
    }
}
