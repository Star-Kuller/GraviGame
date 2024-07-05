using System;
using UnityEngine;
using DG.Tweening;

namespace View.Animations
{
    public class PowerOnAnimation : MonoBehaviour
    {
        [SerializeField] private float animationDuration = 0.45f;
        private RectTransform _uiElement;
        private float _originalSizeY;
        
        private void Awake()
        {
            _uiElement = GetComponent<RectTransform>();
            _originalSizeY = _uiElement.localScale.y;
        }

        private void OnEnable()
        {
            var size = _uiElement.localScale;

            size = new Vector3(size.x, 0, size.z);
            _uiElement.localScale = size;
            
            _uiElement.DOScaleY(_originalSizeY, animationDuration)
                .SetEase(Ease.InOutQuart)
                .SetUpdate(UpdateType.Normal, true);
        }
    }
}
