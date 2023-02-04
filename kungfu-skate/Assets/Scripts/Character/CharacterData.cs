using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public string bodySpriteName;
    public string armSpriteName;
    public string folder;
    public int hitPoints;
    public int explodeThreshold;
    public AudioClip shoot;
    public AudioClip swing;
    public AudioClip[] die;
    public AudioClip hit;
    public AudioClip explode;
    public AudioClip melee;
}
