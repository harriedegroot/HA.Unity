using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ActivationBehaviour : MonoBehaviour, IPointerUpHandler
{
    private const float DoubleClickDelay = 0.4f;
    private const float DoubleClickTimeout = 1f;

    public enum Activation
    {
        OnClick,
        OnUIClick,
        OnEnabled,
        OnDisabled,
        OnMouseDown,
        OnMouseUp,
        OnDoubleClick,
        OnSingleClick,   // NOTE: Waits before double click timeout
        None = 99       // Should be default?
    }

    private int _clicks = 0;
    private float _initialClick;

    public Activation activation;
    //public bool AllowInteractionDuringCameraMovement = false;

    public bool IsUIElement { get; private set; }

    public abstract void OnActivate();

    private void Start()
    {
        IsUIElement = gameObject.layer == LayerMask.NameToLayer("UI");
    }

    private void OnEnable()
    {
        if (activation == Activation.OnEnabled) OnActivate();
    }
    private void OnDisable()
    {
        if (activation == Activation.OnDisabled) OnActivate();
    }

    // From UI Button
    public void OnPointerUp(PointerEventData eventData)
    {
        if (activation == Activation.OnUIClick) OnActivate();
    }


    private void OnMouseDown()
    {
        if (activation == Activation.OnMouseDown)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            //if (!AllowInteractionDuringCameraMovement && CameraController.IsMoving)
            //    return;

            OnActivate();
        }
    }

    private void OnMouseUp() // NOTE: Do most of the work in the MouseUp handler for an up-to-date CameraController.IsMoving value
    {
        if (activation == Activation.OnMouseUp || activation == Activation.OnClick || activation == Activation.OnSingleClick || activation == Activation.OnDoubleClick)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            //if (!AllowInteractionDuringCameraMovement && CameraController.IsMoving)
            //    return;
        }

        if (activation == Activation.OnMouseUp || activation == Activation.OnClick)
        {
            OnActivate();
        }
        else if (activation == Activation.OnSingleClick)
        {
            if (_initialClick == 0)
            {
                _initialClick = Time.time;
            }
            else if (IsDoubleClick)
            {
                _initialClick = 0;
            }
        }
        else if (activation == Activation.OnDoubleClick)
        {
            _clicks++;
            if (_clicks == 1)
            {
                _initialClick = Time.time;
            }
            else if (IsDoubleClick)
            {
                _clicks = 0;
                _initialClick = 0;
                OnActivate();
            }
            else 
            {
                _clicks = 1;
                _initialClick = Time.time;
            }
        }
    }

    private void Update()
    {
        switch(activation)
        {
            case Activation.OnSingleClick:
                if (_initialClick != 0 && !IsDoubleClick)
                {
                    _initialClick = 0;
                    OnActivate();
                }
                break;
            case Activation.OnDoubleClick:
                if (_clicks > 0 && IsDoubleClickTimeout)
                {
                    _clicks = 0;
                    _initialClick = 0;
                }
                break;
        }
    }

    private bool IsDoubleClick => Time.time < _initialClick + DoubleClickDelay;
    private bool IsDoubleClickTimeout => Time.time > _initialClick + DoubleClickTimeout;
}
