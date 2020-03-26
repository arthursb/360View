using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour{
    public GameObject itemPrefab;

    private RectTransform myRectT;
    private VerticalLayoutGroup myLayoutGroup;

    private void Awake() {
        myRectT = GetComponent<RectTransform>();
        myLayoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    private void Start() {
        ResizeArea();
    }

    #region SIZE
    public void ResizeArea() {
        float size = GetItemsSize();
        float spacing = myLayoutGroup.spacing * (GetItemsCount() - 1);
        size += spacing;

        myRectT.sizeDelta = new Vector2(myRectT.sizeDelta.x, size);
    }

    public float GetItemsSize() {
        float result = 0;

        foreach (Transform child in transform) {
            if (child.CompareTag("Item")) {
                RectTransform rectT = child.GetComponent<RectTransform>();
                float height = rectT.rect.height;
                result += height;
            }
        }

        return result;
    }

    public float GetItemsCount() {
        float result = 0;

        foreach (Transform child in transform) {
            if (child.CompareTag("Item")) {
                result++;
            }
        }

        return result;
    }
    #endregion

    #region DATA

    public void DestroyThenUpdate(GameObject obj) {
        Destroy(obj);
        ResizeArea();
    }

    public void SpawnItem(string title, Sprite preview) {
        GameObject itemObj = Instantiate(itemPrefab, this.transform);
        ImageItem item = itemObj.GetComponent<ImageItem>();

        item.SetTitle("EXEMPLO");
        item.SetSprite(preview);

        ResizeArea();
    }

    #endregion
}
