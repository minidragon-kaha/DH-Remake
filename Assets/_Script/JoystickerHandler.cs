using UnityEngine;
using Obvious.Soap;
using UnityEngine.Events;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/JoystickerHandler")]
    public class JoystickerHandler : MonoBehaviour
    {
        [SerializeField] private DynamicJoystick dynamicJoystick;
        [SerializeField] private Vector3Variable movingVector;
        [SerializeField] private UnityEvent<Vector3> onMovingVectorChanged;

        private void Update()
        {
            movingVector.Value = new Vector3(dynamicJoystick.Horizontal, 0f, dynamicJoystick.Vertical);
            onMovingVectorChanged.Invoke(movingVector.Value);
        }
    }
}
