using UnityEngine;
using Obvious.Soap;
using UnityEngine.Events;

namespace DigiHero
{
    public class Detector : MonoBehaviour
    {
        public TaggedObject Target { get; private set; }

        [SerializeField] private string targetTag;
        [SerializeField] private IntVariable detectRangeVariable;
        [SerializeField] private ScriptableListTaggedObject scriptableListTaggedObject;
        [SerializeField] private UnityEvent<TaggedObject> onTargetUpdated;

        private void Update()
        {
            TaggedObject tempTarget = null;
            for (int i = 0; i < scriptableListTaggedObject.Count; i++)
            {
                if (scriptableListTaggedObject[i].Tags.Contains(targetTag))
                {
                    if (Vector3.Distance(transform.position, scriptableListTaggedObject[i].transform.position) > detectRangeVariable.Value)
                    {
                        continue;
                    }

                    if (tempTarget == null)
                    {
                        tempTarget = scriptableListTaggedObject[i];
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, scriptableListTaggedObject[i].transform.position) < Vector3.Distance(transform.position, tempTarget.transform.position))
                        {
                            tempTarget = scriptableListTaggedObject[i];
                        }
                    }
                }
            }

            if (tempTarget != Target)
            {
                Target = tempTarget;
                onTargetUpdated?.Invoke(Target);
            }
        }
    }
}