using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    public class MotionApplier : MonoBehaviour
    {
        [SerializeField] private Vector3Variable movingVector;
        [SerializeField] private StatsContainer statsContainer;

        private void Update()
        {
            float moveSpeed = 1f;

            if (statsContainer != null)
                moveSpeed = statsContainer.MoveSpeed;

            transform.position += moveSpeed * Time.deltaTime * movingVector.Value;
        }
    }
}