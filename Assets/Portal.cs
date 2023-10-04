using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class Portal : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    public Camera MyCamera {get { return myCamera; } }

    //TODO: on this value change, change camera texture to target
    [SerializeField] private Portal pairPortal;
    public Portal PairPortal { get { return pairPortal; } }
    

    private void OnEnable()
    {
        Camera.onPreRender += UpdateCamera;
    }
    private void OnDisable()
    {
        Camera.onPreRender -= UpdateCamera;
    }
    /// <summary>
    /// Update camera position of paired portal
    /// </summary>
    /// <param name="camera">Current Camera</param>
    private void UpdateCamera(Camera camera)
    {
        if (PairPortal == null) return;
        if((camera.cameraType == CameraType.Game || camera.cameraType == CameraType.SceneView) && camera.tag != "PortalCamera"){
            PairPortal.MyCamera.projectionMatrix = camera.projectionMatrix;

            var relativePosition = transform.InverseTransformPoint(camera.transform.position);
            PairPortal.MyCamera.transform.position = PairPortal.transform.TransformPoint(relativePosition);

            var relativeRotation = transform.InverseTransformPoint(camera.transform.forward);
            PairPortal.MyCamera.transform.forward = PairPortal.transform.TransformDirection(relativeRotation);
        }
    }

}
