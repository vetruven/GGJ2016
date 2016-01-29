using UnityEngine;

public class MonoRectWrapper : MonoBehaviour
{
    private RectTransform _rectTransform;
    protected RectTransform rectTransform
    {
        get { return _rectTransform ?? (_rectTransform = transform as RectTransform); }
    }
}
