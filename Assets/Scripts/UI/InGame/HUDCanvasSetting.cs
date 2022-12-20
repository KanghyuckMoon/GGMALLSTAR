using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Rendering.Universal;

public class HUDCanvasSetting : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("hudCanvas")] 
    private Canvas _hudCanvas;
    [SerializeField, FormerlySerializedAs("hudCamera")] 
    private Camera _hudCamera;

    // Start is called before the first frame update
    void Start()
    {
        //hudCanvas.worldCamera = Camera.main;
        _hudCanvas.planeDistance = 1f;
        _hudCanvas.sortingLayerName = "HUD";
        _hudCanvas.sortingOrder = 100;

        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(_hudCamera);
    }
}
