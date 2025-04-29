using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TouchCenterScaleUI : ScrollRect
{
    [SerializeField]
    private float minScale = 0.5f; // 最小缩放比例
    [SerializeField]
    private float maxScale = 2f; // 最大缩放比例
    [SerializeField]
    private float scaleSensitivity = .1f; // 缩放灵敏度
    [SerializeField] 
    private float distanceScale = 30f; // 缩放距离
    [SerializeField]
    private RectTransform rectTransform;
    private int _touchNum;
    private float _initialDistance;
    private float _targetScale = 1f;
    protected override void Start()
    {
        base.Start();
        if (rectTransform)
        {
            _targetScale = rectTransform.localScale.x;
        }
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1)
        {
            return;
        }
        base.OnBeginDrag(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1)
        {
            _touchNum = Input.touchCount;
            return;
        }
        if (Input.touchCount == 1 && _touchNum > 1)
        {
            _touchNum = Input.touchCount;
            base.OnBeginDrag(eventData);
            return;
        }
        base.OnDrag(eventData);
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
        _targetScale = rectTransform.localScale.x;
    }
    private void HandleTouchInput()
    {
        // 检查是否有两个手指触摸屏幕
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            // 至少有一个手指在移动
            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                // 计算当前两指距离
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                // 初始化初始距离（仅在开始时记录）
                if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                {
                    _initialDistance = currentDistance;
                }
                else
                {
                    float d = currentDistance - _initialDistance;
                    if (Math.Abs(d)<distanceScale)
                    {
                        return;
                    }
                    // 计算缩放比例
                    int sign = Math.Sign(d);
                    _targetScale += sign*scaleSensitivity;
                    ScaleUI(_targetScale);
                    // 更新初始距离为当前距离（用于下一次计算）
                    _initialDistance = currentDistance;
                }
            }
        }
    }
    private void HandleMouseInput()
    {
        // 使用鼠标滚轮缩放
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            _targetScale += scroll * scaleSensitivity * 10f; // 乘以10使滚轮更敏感
            ScaleUI(_targetScale);
        }
    }
    private void ScaleUI(float scaleFactor)
    {
        //就算缩放1下的位置
        Vector3 localScale = rectTransform.localScale;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition /= localScale.x;
        // 计算新的缩放比例
        float x = Mathf.Clamp(scaleFactor, minScale, maxScale);
        //保持中心点不变进行缩放
        rectTransform.localScale = new Vector3(x, x, x);
        //调整位置以保持中心点不变
        rectTransform.anchoredPosition = anchoredPosition*x;
    }
}