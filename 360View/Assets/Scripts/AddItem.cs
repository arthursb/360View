using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddItem : MonoBehaviour {
    public Image debugImg;

    private Sprite sprite;
    private ScrollController controller;

    private void Awake() {
        controller = FindObjectOfType<ScrollController>();
    }

    public void OpenExplorer() {
#if UNITY_ANDROID
        PickImage();
#endif
    }

    private void PickImage() {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) => {
            if (path != null) {
                Texture2D texture = NativeGallery.LoadImageAtPath(path);

                if (texture == null) {
                    Debug.LogError("Não encontrou a imagem em: " + path);
                    return;
                }

                float width = texture.width;
                float height = texture.height;

                if (width != 2 * height) {
                    Debug.LogError("Imagem está com dimensões inadequadas");
                    return;
                }

                sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

                controller.SpawnItem("Exemplo", sprite);
            }
        }, "Selecionar imagem 360");
    }
}

