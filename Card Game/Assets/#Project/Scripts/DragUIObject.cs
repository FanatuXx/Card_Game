/*using UnityEngine;
using UnityEngine.EventSystems; //This allows us to use Unity's event system to detect our mouse inputs

public class DragUIObject : MonoBehaviour, IDragHandler, IPointerDownHandler //These classes hold the methods required to handle UI interactions that we need
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;
    public float movementSensitivity = 1.0f; // Adjustable sensitivity if needed

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); //Get the RectTransform component of the attached GameObject
        canvas = GetComponentInParent<Canvas>(); //Get the Canvas component of the attached GameObject
    }

    public void OnPointerDown(PointerEventData eventData) //This is inherited from the IPointerDownHandler class referenced above
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition); //Using the event system to detect what is clicked on
        originalPanelLocalPosition = rectTransform.localPosition;
    }

    public void OnDrag(PointerEventData eventData) //This is inherited from the IDragHandler class referenced above
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition))
        {
            localPointerPosition /= canvas.scaleFactor;

            // Adjusting the movement based on sensitivity
            Vector3 offsetToOriginal = (localPointerPosition - originalLocalPointerPosition) * movementSensitivity;
            rectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;

            // Debug output
            Debug.Log($"Drag - LocalPointerPosition: {localPointerPosition}, Offset: {offsetToOriginal}, New Position: {rectTransform.localPosition}"); //Comment out this line if not debugging an issue, otherwise it will flood the console unnecessarily
        }
    }
}
*/

using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUGUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Config")]
    [SerializeField] private bool m_IsBlockMouse0Operate;

    [SerializeField] private bool m_IsBlockMouse1Operate;
    [SerializeField] private bool m_IsBlockOver1TouchesOperate;

    [Header("Smooth Movement")]
    [SerializeField] private bool m_IsLerpPosition;

    [SerializeField] private float m_LerpT = 500f;

    public bool isDragging => m_IsDragging;
    private bool m_IsDragging;
    private Vector2 m_PivotPosOffsetXY;
    private Vector3 m_TargetPos;

    private RectTransform m_ParentRTCache;
    private RectTransform m_ParentRT => !m_ParentRTCache ? (RectTransform)transform.parent : m_ParentRTCache;
    private RectTransform m_SelfRTCache;
    private RectTransform m_SelfRT => !m_SelfRTCache ? (RectTransform)transform : m_SelfRTCache;

    private Canvas m_RenderCanvasCache;
    private Canvas m_RenderCanvas => !m_RenderCanvasCache ? GetComponentInParent<Canvas>() : m_RenderCanvasCache;

    private void Update()
    {
        if (ShouldBlockDragCalculation()) return;

        if (!m_IsDragging) return;

        var t = m_LerpT * Time.deltaTime * Time.deltaTime;
        if (IsOverlayRenderMode()) m_SelfRT.position = GetPos(m_SelfRT.position);
        else m_SelfRT.localPosition = GetPos(m_SelfRT.localPosition);

        Vector2 GetPos(Vector2 originalPos) => m_IsLerpPosition ? Vector2.Lerp(originalPos, m_TargetPos, t) : m_TargetPos;

        bool ShouldBlockDragCalculation()
        {
            var touchCount = Input.touchCount;
            bool isTouching = touchCount != 0;
            return m_IsBlockMouse0Operate && Input.GetKey(KeyCode.Mouse0) && !isTouching ||
                   m_IsBlockMouse1Operate && Input.GetKey(KeyCode.Mouse1) && !isTouching ||
                   m_IsBlockOver1TouchesOperate && touchCount > 1;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CachePressingOffset(eventData);
        m_IsDragging = true;

        void CachePressingOffset(PointerEventData eventData)
        {
            if (IsOverlayRenderMode())
            {
                Vector2 nowPosXY = m_SelfRT.position;
                m_PivotPosOffsetXY = nowPosXY - eventData.position;
            }
            else
            {
                Vector2 nowPosXY = m_SelfRT.localPosition;
                m_PivotPosOffsetXY = nowPosXY - GetPointerAnchorPosInRectTransform(m_ParentRT);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateTargetPos(eventData);

        void UpdateTargetPos(PointerEventData eventData)
        {
            if (IsOverlayRenderMode())
            {
                var newPosXY = eventData.position + m_PivotPosOffsetXY;
                var originalPosZ = m_SelfRT.position.z;
                m_TargetPos = new Vector3(newPosXY.x, newPosXY.y, originalPosZ);
            }
            else
            {
                var newPosXY = GetPointerAnchorPosInRectTransform(m_ParentRT) + m_PivotPosOffsetXY;
                var originalPosZ = m_SelfRT.localPosition.z;
                m_TargetPos = new Vector3(newPosXY.x, newPosXY.y, originalPosZ);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_IsDragging = false;
    }

    private bool IsOverlayRenderMode()
    {
        return m_RenderCanvas.renderMode == RenderMode.ScreenSpaceOverlay;
    }

    private Vector2 GetPointerAnchorPosInRectTransform(RectTransform parent)
    {
        var cam = Camera.main;
        var mousePos = Input.mousePosition; // Also present touches avg position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, mousePos, cam, out var relativeLocalPos);
        return relativeLocalPos;
    }
}