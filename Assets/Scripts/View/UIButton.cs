using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

public class OnTriggerValueChangedEvent : GameEvent {
    public string TriggerName;
}

public class OnButtonClickedEvent : GameEvent
{
    public UIButton Button;
}

public enum ButtonType {
    OTHER,
    SHOP,
    DUNGEON,
    CAMP
}

[RequireComponent (typeof(Animator))]
public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler {

    protected Animator _animator;
    public bool Selected
    {
        get; private set;
    }
    public bool InvokeOnDeselect = false;
    public ButtonType Type;
    public UnityEvent OnClick;
    public bool TriggerButton = false;
    private bool _enabled = true;
    public bool Enabled { get { return (_enabled); } set { _enabled = value; if (_animator) _animator.SetBool("Enabled", value); } }
    [SerializeField] private AudioClip _onHoveredSound;
    [SerializeField] private AudioClip _onPressedSound;
    [SerializeField] private AudioClip _onReleasedSound;
    [SerializeField] List<string> _triggerEnableNames = new List<string>();
    [SerializeField] List<string> _triggerDisableNames = new List<string>();

	// Use this for initialization
	void Awake () {
        _animator = GetComponent<Animator>();
        if (_animator) {
            _animator.SetBool("Selected", Selected);
            _animator.SetBool("Enabled", Enabled);
        }
    }

    void OnEnable() {
        if (_animator) {
            _animator.SetBool("Selected", Selected);
            _animator.SetBool("Enabled", Enabled);
        }
        Events.Instance.AddListener<OnTriggerValueChangedEvent>(HandleOnTriggerValueChanged);
    }

    void OnDisable() {
        Events.Instance.RemoveListener<OnTriggerValueChangedEvent>(HandleOnTriggerValueChanged);
    }

    void HandleOnTriggerValueChanged(OnTriggerValueChangedEvent e) {
        if (_triggerDisableNames.Contains(e.TriggerName) && Enabled == true)
            Enabled = false;
        else if (_triggerEnableNames.Contains(e.TriggerName) && Enabled == false)
            Enabled = true;
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (_animator)
            _animator.SetBool("Highlighted", true);
        if (_onHoveredSound && Enabled)
            AudioManager.Instance.PlaySFX(_onHoveredSound);
        if (Enabled)
            SendMessage("OnPointerEnteredButton", e, SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (_animator)
            _animator.SetBool("Highlighted", false);
        if (Enabled)
            SendMessage("OnPointerExitedButton", e, SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerDown(PointerEventData e) {
        if (_animator) {
            _animator.SetBool("Pressed", true);
            _animator.SetBool("Highlighted", true);
        }
        if (_onPressedSound && Enabled)
            AudioManager.Instance.PlaySFX(_onPressedSound);
    }

    public void OnPointerUp(PointerEventData e) {
        if (_animator)
            _animator.SetBool("Pressed", false);
        if (_onReleasedSound && Enabled)
            AudioManager.Instance.PlaySFX(_onReleasedSound);
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (Enabled == true) {
            OnClick.Invoke();
            if (Selected)
                Deselect();
            else {
                Select();
            }
        }
    }

    public void Deselect()
    {
        if (!TriggerButton && Selected)
        {
            Selected = false;
            if (_animator)
                _animator.SetBool("Selected", false);
        }
        Events.Instance.Raise(new OnButtonClickedEvent() { Button = this });
    }

    public void Select()
    {
        if (!TriggerButton)
        {
            Selected = true;
            if (_animator)
                _animator.SetBool("Selected", true);
        }
        Events.Instance.Raise(new OnButtonClickedEvent() { Button = this });
    }

    public void OnSelect(BaseEventData e) {
        if (_onHoveredSound && Enabled)
            AudioManager.Instance.PlaySFX(_onHoveredSound);
    }

    public void Trigger(string triggerName) {
        Events.Instance.Raise(new OnTriggerValueChangedEvent() { TriggerName = triggerName });
    }
}
