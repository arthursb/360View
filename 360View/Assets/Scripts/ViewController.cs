using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewController : MonoBehaviour {
    public bool isVR;
    public GameObject sphere;
    private GameObject menuContainer;

    private void Start() {
        menuContainer = GameObject.FindGameObjectWithTag("Container");

        if(menuContainer != null) {
            menuContainer.SetActive(false);
        }

        if (GlobalData.sprite == null) {
            return;
        }

        Texture2D tex = GlobalData.sprite.texture;
        sphere.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
    }

    private void Update() {

#if UNITY_ANDROID
        if (!isVR) {
            if (Input.touchCount > 0) {
                RaycastHit hit;

                Touch t = Input.touches[0];

                if (t.phase != TouchPhase.Began) {
                    return;
                }

                Ray ray = Camera.main.ScreenPointToRay(t.position);

                if (Physics.Raycast(ray.origin, ray.direction * 1000, out hit)) {
                    if (hit.collider.CompareTag("Finish")) {
                        BackToMenu();
                        return;
                    }
                }
            }
        }
#endif

        if (Input.GetKeyDown(KeyCode.Escape)) {
            BackToMenu();
        }
    }

    public void BackToMenu() {
        menuContainer.SetActive(true);

        EnableVR vr = FindObjectOfType<EnableVR>();

        if(vr != null) {
            vr.Disable();
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
    }

}