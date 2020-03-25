using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private const float turnSpeedMouse = 150f;

    public Transform container;

    private void Awake() {
        if (!SystemInfo.supportsGyroscope) {
            Debug.LogWarning("Gyro not supported");
            return;
        }

        Input.gyro.enabled = true;
    }


    private void LateUpdate() {
#if UNITY_ANDROID
        if (Input.gyro.enabled) {
            RotateByGyro();
        }
#elif UNITY_IOS
#elif UNITY_STANDALONE || UNITY_WEBGL
        RotateByMouse();
#endif

    }

    private void RotateByMouse() {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        container.Rotate(new Vector3(0, horizontal * (-1), 0f) * Time.deltaTime * turnSpeedMouse);
        transform.Rotate(new Vector3(vertical, 0, 0) * Time.deltaTime * turnSpeedMouse);
    }

    private void RotateByGyro() {
        transform.rotation = Input.gyro.attitude;

        transform.Rotate(0f, 0f, 180f, Space.Self); 
        transform.Rotate(90f, 180f, 0f, Space.World);
    }

    private Quaternion GyroToUnity(Quaternion q) {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}