using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UINavbar : MonoBehaviour {

    [SerializeField]
    private Text _golds;

    void Start() {
        HandleOnGoldChanged(null);
    }

    void OnEnable() {
        Events.Instance.AddListener<OnGoldChangedEvent>(HandleOnGoldChanged);
    }

    void OnDisable() {
        Events.Instance.RemoveListener<OnGoldChangedEvent>(HandleOnGoldChanged);
    }

    void HandleOnGoldChanged(OnGoldChangedEvent e) {
        _golds.text = InventoryManager.Instance.Golds.ToString();
    }
}
