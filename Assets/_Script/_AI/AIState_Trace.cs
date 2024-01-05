using Obvious.Soap;
using UnityEngine;

namespace DigiHero
{
    public class AIState_Trace : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Events.UnityEvent<Vector3> onMovingVectorUpdated;

        private Vector3 movingVector;
        private TaggedObject traceTarget;

        public void UpdateTraceTarget(TaggedObject taggedObject)
        {
            traceTarget = taggedObject;
        }

        private void Update()
        {
            if (traceTarget == null)
            {
                this.movingVector = Vector3.zero;
                onMovingVectorUpdated?.Invoke(this.movingVector);
                return;
            }

            Vector3 targetPosition = traceTarget.transform.position;
            Vector3 movingVector = targetPosition - transform.position;
            movingVector.Normalize();
            this.movingVector = movingVector;
            onMovingVectorUpdated?.Invoke(this.movingVector);
        }
    }
}