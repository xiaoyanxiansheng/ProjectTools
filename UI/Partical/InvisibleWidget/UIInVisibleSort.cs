using UnityEngine;

[RequireComponent(typeof(UIInvisibleWidget))]
public class UIInVisibleSort : MonoBehaviour
{
    public bool isTop = true;
    public int renderQueue_ReadOnly;

    private Renderer[] _renderers;
    private UIInvisibleWidget _widget;
    private bool _isSort = false;

    public void SortTop()
    {
        int maxDepth = _widget.depth;
        if (isTop)
        {
            UIPanel parentPanel = transform.GetComponentInParent<UIPanel>();
            if (parentPanel)
                maxDepth = parentPanel.GetMaxUIWightDepth() + 1;
        }
        Sort(maxDepth);
    }

    public void Sort(int depth)
    {
        _widget.depth = depth;
        _isSort = true;
    }

    void Awake()
    {
        _widget = gameObject.GetComponent<UIInvisibleWidget>();
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
    }

    void Start()
    {
        SortTop();
    }

    void Update()
    {
        if (!_isSort)
            return;

        if (_widget == null || _widget.drawCall == null || _renderers == null)
        {
            return;
        }

        _isSort = false;

        renderQueue_ReadOnly = _widget.drawCall.renderQueue;
        foreach(Renderer render in _renderers)
        {
            //render.sortingOrder = 0;
            render.material.renderQueue = renderQueue_ReadOnly;
        }
    }

}
