using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TouchCenterScaleUI : MonoBehaviour
{
    [SerializeField]
    private float minScale = 0.5f; // 最小缩放比例
    [SerializeField]
    private float maxScale = 2f; // 最大缩放比例
    [SerializeField]
    private float scaleSensitivity = 1f; // 缩放灵敏度
    private RectTransform _rectTransform;
    // 用于双指缩放
    private float _prevTouchDistance;
    private bool _isScaling;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        // 确保初始pivot是0.5,0.5（中心）
        if (_rectTransform.pivot != new Vector2(0.5f, 0.5f))
        {
            Debug.LogWarning("UI元素的pivot不是中心点(0.5,0.5)，缩放可能不如预期", this);
        }
    }
    private void Update()
    {
        #if UNITY_EDITOR
        // 在编辑器中使用鼠标模拟触摸
        HandleMouseInput();
        #else
        // 在设备上使用触摸输入
        HandleTouchInput();
        #endif
    }
    private void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            // 单指触摸 - 可以在这里添加拖动逻辑
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                // 可以在这里添加单指操作逻辑
            }
        }
        else if (Input.touchCount == 2)
        {
            // 双指缩放
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);
            // 获取当前触摸点位置
            Vector2 firstTouchCurrentPos = firstTouch.position;
            Vector2 secondTouchCurrentPos = secondTouch.position;
            // 计算当前触摸距离
            float currentTouchDistance = Vector2.Distance(firstTouchCurrentPos, secondTouchCurrentPos);
            if (firstTouch.phase == TouchPhase.Began || secondTouch.phase == TouchPhase.Began)
            {
                // 记录初始触摸位置和距离
                _prevTouchDistance = currentTouchDistance;
                _isScaling = true;
            }
            else if (firstTouch.phase == TouchPhase.Moved || secondTouch.phase == TouchPhase.Moved)
            {
                if (_isScaling)
                {
                    // 计算缩放比例
                    float scaleFactor = currentTouchDistance / _prevTouchDistance;
                    ScaleUI(scaleFactor);
                    // 更新前一个位置和距离
                    _prevTouchDistance = currentTouchDistance;
                }
            }
            else if (firstTouch.phase == TouchPhase.Ended || secondTouch.phase == TouchPhase.Ended)
            {
                _isScaling = false;
            }
        }
        else
        {
            _isScaling = false;
        }
    }
    private void HandleMouseInput()
    {
        // 使用鼠标滚轮缩放
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float scaleFactor = 1f + scroll * scaleSensitivity; // 乘以10使滚轮更敏感
            ScaleUI(scaleFactor);
        }
    }
    private void ScaleUI(float scaleFactor)
    {
        //就算缩放1下的位置
        Vector3 localScale = _rectTransform.localScale;
        Vector2 anchoredPosition = _rectTransform.anchoredPosition;
        anchoredPosition /= localScale.x;
        // 计算新的缩放比例
        Vector3 newScale = localScale * scaleFactor;
        float x = Mathf.Clamp(newScale.x, minScale, maxScale);
        newScale = new Vector3(x, x, x);
        //保持中心点不变进行缩放
        _rectTransform.localScale = newScale;
        //调整位置以保持中心点不变
        _rectTransform.anchoredPosition = anchoredPosition*newScale.x;
    }
}