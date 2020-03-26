using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class PopupPanel : MonoBehaviour{
    [SerializeField]
    private EventTrigger eventTrigger;

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    public void SetClickTrigger(UnityEvent uEvent) {
        EventTrigger.Entry entry = new EventTrigger.Entry {
            eventID = EventTriggerType.PointerClick
        };

        entry.callback.AddListener((eventData) => { uEvent.Invoke(); });
        eventTrigger.triggers.Add(entry);
    }

    public void SetTitle(string title) {
        titleText.text = title;
    }

    public void SetDescription(string description) {
        descriptionText.text = description;
    }

    public void SelfDestruct() {
        Destroy(gameObject);
    }
}
