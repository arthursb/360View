using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewController : MonoBehaviour {
    public GameObject sphere;
    private GameObject menuContainer;

    void Start() {
        menuContainer = GameObject.FindGameObjectWithTag("Container");
        menuContainer.SetActive(false);

        if (GlobalData.sprite == null) {
            return;
        }

        Texture2D tex = GlobalData.sprite.texture;
        sphere.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
    }

    private void Update() {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape)) {
            BackToMenu();
        }
#endif
    }

    private void BackToMenu() {
        menuContainer.SetActive(true);
        SceneManager.UnloadSceneAsync("3D View");
    }

}
