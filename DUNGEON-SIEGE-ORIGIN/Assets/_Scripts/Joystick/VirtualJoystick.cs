using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public enum VirtualJoystickType { Fixed, Floating }

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    [SerializeField] private RectTransform centerArea = null;
    [SerializeField] private RectTransform handle = null;
    [InputControl(layout = "Vector2")]
    [SerializeField] private string stickControlPath;
    [SerializeField] private float movementRange = 100f;

    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _playerDeathChannel;
    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _exitLevelChannel;

    protected bool _hideOnPointerUp = false;
    protected bool _centralizeOnPointerUp = true;
    private Canvas canvas;
    protected RectTransform baseRect = null;
    protected OnScreenStick handleStickController = null;
    protected CanvasGroup bgCanvasGroup = null;
    protected Vector2 initialPosition = Vector2.zero;

    protected virtual void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        baseRect = GetComponent<RectTransform>();
        bgCanvasGroup = centerArea.GetComponent<CanvasGroup>();
        handleStickController = handle.gameObject.AddComponent<OnScreenStick>();
        handleStickController.movementRange = movementRange;
        handleStickController.controlPath = stickControlPath;

        Vector2 center = new Vector2(0.5f, 0.5f);
        centerArea.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;

        initialPosition = centerArea.anchoredPosition;

        if (_hideOnPointerUp) bgCanvasGroup.alpha = 0;
        else bgCanvasGroup.alpha = 1;
    }

    private void OnEnable()
    {
        _playerDeathChannel.OnEventTrigger += DisableJoystick;
        _exitLevelChannel.OnEventTrigger += DisableJoystick;
    }

    private void OnDisable()
    {
        _playerDeathChannel.OnEventTrigger -= DisableJoystick;
        _exitLevelChannel.OnEventTrigger -= DisableJoystick;
    }

    private void DisableJoystick()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerEventData constructedEventData = new PointerEventData(EventSystem.current);
        constructedEventData.position = handle.position;
        handleStickController.OnPointerDown(constructedEventData);

        centerArea.anchoredPosition = GetAnchoredPosition(eventData.position);

        if (_hideOnPointerUp) { bgCanvasGroup.alpha = 1; }
    }

    public void OnDrag(PointerEventData eventData)
    {
        handleStickController.OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (_centralizeOnPointerUp)
            centerArea.anchoredPosition = initialPosition;

        if (_hideOnPointerUp) bgCanvasGroup.alpha = 0;
        else bgCanvasGroup.alpha = 1;

        PointerEventData constructedEventData = new PointerEventData(EventSystem.current);
        constructedEventData.position = Vector2.zero;

        handleStickController.OnPointerUp(constructedEventData);
    }

    protected Vector2 GetAnchoredPosition(Vector2 screenPosition)
    {
        Camera cam = (canvas.renderMode == RenderMode.ScreenSpaceCamera) ? canvas.worldCamera : null;
        Vector2 localPoint = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (centerArea.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }

        return Vector2.zero;
    }

}
