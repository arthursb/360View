using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ImageItem : MonoBehaviour {
    private ScrollController controller;
    public Image image;
    public TMP_Text text;

    public GameObject popupPrefab;

    [SerializeField]
    private Image imagePreview;

    [SerializeField]
    private EventTrigger viewGyro;

    [SerializeField]
    private EventTrigger viewVR;

    [SerializeField]
    private EventTrigger viewMouse;

    private void Awake() {
        controller = FindObjectOfType<ScrollController>();
    }

    private void Start() {
        SetButtonVisibility();
    }

    private void SetButtonVisibility() {
#if UNITY_ANDROID || UNITY_IOS
        viewGyro.gameObject.SetActive(true);
        viewVR.gameObject.SetActive(true);
        viewMouse.gameObject.SetActive(false);
#elif UNITY_STANDALONE
        viewGyro.gameObject.SetActive(false);
        viewVR.gameObject.SetActive(false
);
        viewMouse.gameObject.SetActive(true);
#endif
    }

    public void OpenConfirmCancel() {
        Transform canvasObj = FindObjectOfType<Canvas>().transform;
        GameObject popup = Instantiate(popupPrefab, canvasObj);
        PopupPanel script = popup.GetComponent<PopupPanel>();

        script.SetTitle("Deletar");
        script.SetDescription(
            "Você tem certeza que gostaria de remover a imagem selecionada?\n" +
            "Essa ação não poderá ser desfeita."
            );

        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(DestroyThenUpdate);
        myEvent.AddListener(script.SelfDestruct);
        script.SetClickTrigger(myEvent);

    }

    public void SetTitle(string title) {
        text.text = title;
    }

    public void SetSprite(Sprite sprite) {
        image.sprite = sprite;
    }

    private void DestroyThenUpdate() {
        controller.DestroyThenUpdate(this.gameObject);
    }

    public void GoToViewScene() {
        GlobalData.sprite = image.sprite;
        SceneManager.LoadScene("3D View", LoadSceneMode.Additive);
    }
}
