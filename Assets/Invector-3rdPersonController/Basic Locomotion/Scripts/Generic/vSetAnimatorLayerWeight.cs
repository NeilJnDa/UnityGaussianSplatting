using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vSetAnimatorLayerWeight : MonoBehaviour
{   
    [Range(0,1)]
    public float value;
   
    public int animatorLayerIndex;
    private void Start()
    {
        GetComponent<Animator>().SetLayerWeight(animatorLayerIndex, value);
    }
}
