using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private GameObject glowEffect;

    [HideInInspector] public HandManager ownerHandManager;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;
    }

    void Update()
    {

        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                if (!Input.GetMouseButton(0))
                {
                    TransitionToState0();
                }
                break;
        }
    }

    private void TransitionToState0()
    {
        bool wasPlaying = (currentState == 2);

        currentState = 0;
        rectTransform.localScale = originalScale;
        rectTransform.localRotation = originalRotation;
        rectTransform.localPosition = originalPosition;
        glowEffect.SetActive(false);

        if (wasPlaying)
        {
            OnCardPlayed();
        }
    }

    private void OnCardPlayed()
    {
        Debug.Log("OnCardPlayed CALLED!");

        HandManager handManager = ownerHandManager;
        Transform handPosition = handManager != null ? handManager.handTransform : null;

        Debug.Log($"HandManager: {handManager}, HandPosition: {handPosition}");

        if (handManager != null && handPosition != null)
        {
            Debug.Log("Triggering CustomEvent OnCardPlayed");
            CustomEvent.Trigger(gameObject, "OnCardPlayed", handManager, handPosition);
        }
        else
        {
            Debug.LogError("HandManager or HandPosition is NULL!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
        }
    }

    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }
}

