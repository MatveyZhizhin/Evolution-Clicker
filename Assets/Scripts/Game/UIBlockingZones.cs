using UnityEngine;

namespace UI
{
    public class UIBlockingZones : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _uiBlockingZones;

        public RectTransform[] GetZones() => _uiBlockingZones;
    }
}

