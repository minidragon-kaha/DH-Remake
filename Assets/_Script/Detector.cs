using UnityEngine;
using Obvious.Soap;
using UnityEngine.Events;
using UnityEditor.EditorTools;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/Detector")]
    public class Detector : MonoBehaviour
    {
        public TaggedObject Target { get; private set; }

        [Tooltip("進入範圍內的物件有TaggedObject時，偵測目標標籤")]
        [SerializeField] private string targetTag;
        [Tooltip("偵測範圍")]
        [SerializeField] private IntVariable detectRangeVariable;
        [Tooltip("目標列表，將會從中找出最近的目標")]
        [SerializeField] private ScriptableListTaggedObject scriptableListTaggedObject;
        [Tooltip("當目標更新時，會觸發此事件")]
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