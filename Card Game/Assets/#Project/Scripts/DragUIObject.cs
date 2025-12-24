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