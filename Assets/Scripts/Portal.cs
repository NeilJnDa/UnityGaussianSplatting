using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

[SerializeField]
public enum PortalState
{
    Disabled,
    Enabled,
    JustEntered
}
[ExecuteInEditMode]
public class Portal : MonoBehaviour
{
    [Tooltip("By default, is this portal enabled")]
    [SerializeField] private bool enableByDefault = true;
    [Tooltip("Do not change from Inspector")]
    [SerializeField] private PortalState portalState = PortalState.Enabled; 
    [SerializeField] private Camera myCamera;
    public Camera MyCamera {get { return myCamera; } }

    //TODO: on this value change, change camera texture to target
    [SerializeField] private Portal pairPortal;
    public Portal PairPortal { get { return pairPortal; } }

    [SerializeField] private PortalCollider portalCollider;

    private void OnEnable()
    {
        Camera.onPreRender += UpdateCamera;
        portalCollider.MainCameraTriggerFromFront.AddListener(OnCameraEnterPortal);
        portalCollider.MainCameraLeave.AddListener(OnCameraLeavePortal);
        portalState = PortalState.Enabled;
    }
    private void OnDisable()
    {
        Camera.onPreRender -= UpdateCamera;
        portalCollider.MainCameraTriggerFromFront.RemoveListener(OnCameraEnterPortal);
        portalCollider.MainCameraLeave.RemoveListener(OnCameraLeavePortal);
        portalState = PortalState.Disabled;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

        }
        InCameraView();
    }

    private IEnumerator switchInst = null;
    private void SwitchPortal()
    {
        if (switchInst != null) {
            return;
        }

        switchInst = SwitchPortalCoroutine() as IEnumerator;
        StartCoroutine(switchInst);
    }
    IEnumerator SwitchPortalCoroutine()
    {
        yield return null;

    }
    private void InCameraView()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        bool visible =  GeometryUtility.TestPlanesAABB(planes, portalCollider.GetCollider.bounds);
        if (visible)
        {
            pairPortal.MyCamera.enabled = true;
        }
        else
        {
            pairPortal.MyCamera.enabled = false;
        }
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
            relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
            PairPortal.MyCamera.transform.position = PairPortal.transform.TransformPoint(relativePosition);

            var relativeDirection = transform.InverseTransformDirection(camera.transform.forward);
            relativeDirection = Vector3.Scale(relativeDirection, new Vector3(-1, 1, -1));
            PairPortal.MyCamera.transform.forward = PairPortal.transform.TransformDirection(relativeDirection);
        }
    }
    private void OnCameraEnterPortal(Collider collider)
    {
        if (portalState == PortalState.Disabled || portalState == PortalState.JustEntered) return;
        Debug.Log(collider.gameObject.name + " Enter " + this.gameObject.name);

        //Tell paired portal
        PairPortal.portalState = PortalState.JustEntered;

        collider.transform.position = PairPortal.MyCamera.transform.position;
        collider.transform.rotation = PairPortal.MyCamera.transform.rotation;

    }
    private void OnCameraLeavePortal(Collider collider)
    {
        //Debug.Log(collider.gameObject.name + " Leave " + this.gameObject.name);

        //Can teleport after leaving
        if (enableByDefault)
            this.portalState = PortalState.Enabled;
        else
            this.portalState = PortalState.Disabled;
    }
}
