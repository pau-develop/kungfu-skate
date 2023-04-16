using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ColorLister : MonoBehaviour
{
    public Color32[] originalColors;
    public Color32[] transitionColors;
    public Color32[] afterTransitionColors;
    public ColorList orginalColorCycles = new ColorList();
    public ColorList transitionColorCycles = new ColorList();
    public ColorList afterTransitionColorCycles = new ColorList();
}
 