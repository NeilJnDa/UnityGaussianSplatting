using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vEnableRandomObject : MonoBehaviour
{
    public GameObject[] objects;
    public bool enableOnStart;
    // Start is called before the first frame update
    protected void Awake()
    {
        if (enableOnStart)
            EnableObject();
    }

    public virtual void EnableObject()
    {
        int indexToEnable = Random.Range(0, objects.Length *10) & objects.Length - 1;
        for (int i=0;i<objects.Length;i++)
        {
            objects[i].SetActive(i == indexToEnable);
        }
    }
   
}
