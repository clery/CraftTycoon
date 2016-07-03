using UnityEngine;
using System.Collections.Generic;

public class TriggerVisible : MonoBehaviour {

    public List<string> TriggerHideName = null;
    public List<string> TriggerShowName = null;
    CanvasGroup _canvasGroup;

    // TODO : Tuner le script pour avoir différents triggers pour show et pour hide

    void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable() {
        Events.Instance.AddListener<OnTriggerValueChangedEvent>(HandleOnTriggerValueChanged);
    }

    void OnDisable() {
        Events.Instance.RemoveListener<OnTriggerValueChangedEvent>(HandleOnTriggerValueChanged);
    }

    void HandleOnTriggerValueChanged(OnTriggerValueChangedEvent e) {
        if (TriggerHideName.Contains(e.TriggerName) && _canvasGroup.alpha == 1) 
            Hide();
        else if (TriggerShowName.Contains(e.TriggerName) && _canvasGroup.alpha == 0)
            Show();
    }

    public void Hide() {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void Show() {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
}
