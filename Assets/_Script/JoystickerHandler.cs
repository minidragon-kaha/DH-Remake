using UnityEngine;
using Obvious.Soap;
using UnityEngine.Events;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/JoystickerHandler")]
    public class JoystickerHandler : MonoBehaviour
    {
        [Tooltip("虛擬搖桿")]
        [SerializeField] private DynamicJoystick dynamicJoystick;
        [Tooltip("移動動量")]
        [SerializeField] private Vector3Variable movingVector;
        [Tooltip("當移動動量更新時，會觸發此事件")]
        [SerializeField] private UnityEvent<Vector3> onMovingVectorChanged;

        private void Update()
        {
            movingVector.Value = new Vector3(dynamicJoystick.Horizontal, 0f, dynamicJoystick.Vertical);
            onMovingVectorChanged.Invoke(movingVector.Value);
        }
    }
}
