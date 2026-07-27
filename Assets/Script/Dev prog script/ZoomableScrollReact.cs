using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ZoomableScrollReact : MonoBehaviour
{
[Header("Components")]
    [SerializeField] private ScrollRect scrollRect;
    
    [Header("Zoom Settings")]
    [SerializeField] private float minZoom = 0.5f;
    [SerializeField] private float maxZoom = 3.0f;
    [SerializeField] private float mouseSensitivity = 0.2f;
    [SerializeField] private float touchSensitivity = 0.01f;
    [SerializeField] private float zoomSpeed = 10f;

    private RectTransform content;
    private float currentZoom = 1f;
    private bool isPinching = false;

    // Mobile tracking
    private float startTouchDist;
    private float startZoom;

    void Start()
    {
        if (scrollRect == null) scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        currentZoom = content.localScale.x;
        
        // Ensure multi-touch works
        Input.multiTouchEnabled = true; 
    }

    void Update()
    {
        HandleMouseZoom();
        HandleTouchZoom();
        
        // Smoothly interpolate to target zoom
        if (Mathf.Abs(content.localScale.x - currentZoom) > 0.001f)
        {
            content.localScale = Vector3.Lerp(content.localScale, Vector3.one * currentZoom, zoomSpeed * Time.deltaTime);
        }
    }

    private void HandleMouseZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollInput) > float.Epsilon)
        {
            Vector2 mousePos = Input.mousePosition;
            UpdatePivotToInputPosition(mousePos);
            
            currentZoom += scrollInput * mouseSensitivity * currentZoom;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }
    }

    private void HandleTouchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (!isPinching)
            {
                isPinching = true;
                // Calculate initial distance and zoom anchor
                startTouchDist = Vector2.Distance(touch0.position, touch1.position);
                startZoom = currentZoom;
                
                Vector2 touchCenter = (touch0.position + touch1.position) / 2f;
                UpdatePivotToInputPosition(touchCenter);
            }
            else
            {
                // Dynamic pinch tracking
                float currentTouchDist = Vector2.Distance(touch0.position, touch1.position);
                float deltaDist = currentTouchDist - startTouchDist;
                
                currentZoom = startZoom + (deltaDist * touchSensitivity);
                currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            }
        }
        else
        {
            isPinching = false;
        }
    }

    private void UpdatePivotToInputPosition(Vector2 screenPosition)
    {
        // Convert screen position to local space of the content rect
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(content, screenPosition, null, out Vector2 localPoint))
        {
            Vector2 pivotPosition = new Vector2(content.pivot.x * content.rect.width, content.pivot.y * content.rect.height);
            Vector2 offsetFromBottomLeft = pivotPosition + localPoint;
            
            Vector2 newPivot = new Vector2(offsetFromBottomLeft.x / content.rect.width, offsetFromBottomLeft.y / content.rect.height);
            
            // Adjust pivot position dynamically without moving the graphic visually
            Vector2 deltaPivot = content.pivot - newPivot;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * content.rect.width, deltaPivot.y * content.rect.height) * content.localScale.x;
            
            content.pivot = newPivot;
            content.localPosition -= deltaPosition;
        }
    }
}
