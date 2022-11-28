using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HUDCanvasSetting : MonoBehaviour
{
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Camera hudCamera;

    // Start is called before the first frame update
    void Start()
    {
        //hudCanvas.worldCamera = Camera.main;
        hudCanvas.planeDistance = 1f;
        hudCanvas.sortingLayerName = "HUD";
        hudCanvas.sortingOrder = 100;

        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(hudCamera);
    }
}
