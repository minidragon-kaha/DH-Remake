using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.AI;

namespace DigiHero.UnityNavMesh
{
    [AddComponentMenu("_DigiHero/UnityNavMesh/UnityNavMeshMotionApplier")]
    public class UnityNavMeshMotionApplier : MonoBehaviour
    {
        [Tooltip("移動動量")]
        [SerializeField] private Vector3Reference movingVector;
        [Tooltip("數值容器")]
        [SerializeField] private StatsContainer statsContainer;

        private List<object> pauseLockers = new List<object>();

        public void UpdateMovingVector(Vector3 movingVector)
        {
            this.movingVector.Value = movingVector;
        }

        public void Pause(object locker)
        {
            pauseLockers.Add(locker);
        }

        public void Resume(object locker)
        {
            if (pauseLockers.Contains(locker))
                pauseLockers.Remove(locker);
        }

        private void Update()
        {
            if (pauseLockers.Count > 0)
                return;

            transform.position += movingVector.Value * statsContainer.MoveSpeed * Time.deltaTime;
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 100, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
        }
    }
}
