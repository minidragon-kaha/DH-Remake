using System.Collections.Generic;
using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/TagTrigger")]
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class TagTrigger : MonoBehaviour
    {
        public event System.Action OnTriggered;

        [Tooltip("可以觸發的標籤")]
        [SerializeField] private List<string> targetTags = new List<string>();
        [Tooltip("觸發時要執行的事件")]
        [SerializeField] private UnityEngine.Events.UnityEvent onTriggeredUnityEvent;

        private Rigidbody rb;
        private Collider col;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();

            if (rb == null)
            {
                Debug.LogWarning("Rigidbody is not found. Rigidbody is added automatically. object name: " + gameObject.name);
                rb = gameObject.AddComponent<Rigidbody>();
            }
            if (col == null)
            {
                Debug.LogWarning("Collider is not found. Collider is added automatically. object name: " + gameObject.name);
                col = gameObject.AddComponent<BoxCollider>();
            }

            rb.useGravity = false;
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            TaggedObject otherTags = other.GetComponent<TaggedObject>();

            if (otherTags == null)
            {
                return;
            }

            for (int i = 0; i < targetTags.Count; i++)
            {
                if (otherTags.Tags.Contains(targetTags[i]))
                {
                    OnTriggered?.Invoke();
                    onTriggeredUnityEvent?.Invoke();
                    return;
                }
            }
        }
    }
}