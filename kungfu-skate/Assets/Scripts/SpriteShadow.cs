using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    [SerializeField] private GameObject shadowPrefab;
    private GameObject shadow;
    private Vector2 shadowScale;
    public bool shouldDestroyShadow = false;
    private int[] raysPosition = new int[]{10, -10};
    private int[] botPositions = new int[2];
    private Transform[] shadows = new Transform[2];
    private SpriteMask[] masks = new SpriteMask[2];
    private Vector2[] shadowScales = new Vector2[]{
        new Vector2(1,1),
        new Vector2(1,1)
    };
    // Start is called before the first frame update
    void Start()
    {
        shadows[0] = Instantiate(shadowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity).transform;
        shadows[1] = Instantiate(shadowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity).transform;
    }

    // Update is called once per frame
    void Update()
    {
        castRays(0);
        castRays(1); 
        updateShadowPosition(0);
        updateShadowPosition(1);
        if(shouldDestroyShadow){
            if(shadows[0] != null) destroyShadow(0);
            if(shadows[1] != null) destroyShadow(1);
        } 
    }

    private void castRays(int index){
        int rayLength = 180;
        Vector2 rayPos = new Vector2(transform.position.x+raysPosition[index], transform.position.y + 5);
        Debug.DrawRay(rayPos, Vector2.down * rayLength, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayLength);
        if(hit.collider != null)
            if(hit.collider.tag == "Obstacle"||hit.collider.tag == "Grindable")
                if(hit.transform.Find("shadow-mask") != null) {
                    masks[index] = hit.transform.Find("shadow-mask").GetComponent<SpriteMask>();
                    getBottomPosition(index, hit.collider);
                } 
    }

    private void getBottomPosition(int index, Collider2D collider){
        float screenBottom = -90;
        BoxCollider2D boxCollider = collider.GetComponent<BoxCollider2D>();
        botPositions[index] = (int)(screenBottom + boxCollider.size.y + 3);
        shadows[index].GetComponent<SpriteRenderer>().sortingOrder = masks[index].frontSortingOrder;
    }

    

    private void updateShadowPosition(int index){
        shadows[index].position = new Vector2(transform.position.x, botPositions[index]);
    }

    public void destroyShadow(int index){
        if(shadowScales[index].x > 0){
            shadowScales[index].x -= 5 * Time.deltaTime;
            shadowScales[index].y -= 5 * Time.deltaTime;
        } else {
            shadowScales[index].x = 0;
            shadowScales[index].y = 0;
            Destroy(shadows[index]);
            // Destroy(GetComponent<SpriteShadow>());
        }
        shadows[index].localScale = shadowScale; 
    }
}
