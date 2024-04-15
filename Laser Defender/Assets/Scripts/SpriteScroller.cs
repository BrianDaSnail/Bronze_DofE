using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] UnityEngine.Vector2 moveSpeed;

    void Start() //Scroll the background at a given speed.
    {
        Renderer renderer = GetComponent<SpriteRenderer>();
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propBlock);
 
        propBlock.SetFloat("_XSpeed", moveSpeed.x);
        propBlock.SetFloat("_YSpeed", moveSpeed.y);
 
        renderer.SetPropertyBlock(propBlock);
    }
}
