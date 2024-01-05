using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigiHero.TestScene
{
    public class _TestScene : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Text text;

        public void OnTargetUpdated(TaggedObject taggedObject)
        {
            if (taggedObject == null)
            {
                text.text = "No target";
            }
            else
            {
                text.text = "Target: " + taggedObject.name;
            }
        }
    }
}
