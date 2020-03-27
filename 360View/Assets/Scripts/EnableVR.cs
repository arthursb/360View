using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnableVR : MonoBehaviour {
    private void Awake() {
        Enable();
    }

    public void Enable() {
        StartCoroutine(SwitchToVR());
    }


    public void Disable() {
        StartCoroutine(SwitchTo2D());
    }

    private IEnumerator SwitchToVR() {
        string desiredDevice = "cardboard"; 

        if (string.Compare(XRSettings.loadedDeviceName, desiredDevice, true) != 0) {
            XRSettings.LoadDeviceByName(desiredDevice);

            yield return null;
        }

        XRSettings.enabled = true;
    }

    IEnumerator SwitchTo2D() {
        XRSettings.LoadDeviceByName("");
        XRSettings.enabled = false;

        yield return null;

        Screen.orientation = ScreenOrientation.Portrait;

        ResetCameras();
    }

    void ResetCameras() {
        foreach(Camera cam in Camera.allCameras) {
            if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None) {
                cam.transform.localPosition = Vector3.zero;
                cam.transform.localRotation = Quaternion.identity;
            }
        }
    }
}