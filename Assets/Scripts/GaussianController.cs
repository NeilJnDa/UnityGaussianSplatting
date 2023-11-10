using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianController : MonoBehaviour
{
    GaussianSplatRenderer splatRenderer;
    // Start is called before the first frame update
    void Start()
    {
        splatRenderer = GetComponent<GaussianSplatRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            if (splatRenderer.m_RenderMode == GaussianSplatRenderer.RenderMode.DebugPoints)
                splatRenderer.m_RenderMode = GaussianSplatRenderer.RenderMode.Splats;
            else if (splatRenderer.m_RenderMode == GaussianSplatRenderer.RenderMode.Splats)
                splatRenderer.m_RenderMode = GaussianSplatRenderer.RenderMode.DebugPoints;
        }
    }
}
