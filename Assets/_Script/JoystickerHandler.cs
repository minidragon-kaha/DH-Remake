using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    public class JoystickerHandler : MonoBehaviour
    {
        [SerializeField] private DynamicJoystick dynamicJoystick;
        [SerializeField] private Vector3Variable movingVector;

        private void Update()
        {
            movingVector.Value = new Vector3(dynamicJoystick.Horizontal, 0f, dynamicJoystick.Vertical);
        }
    }
}
