using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/SingletonGameObjectApplier")]
    public class SingletonGameObjectApplier : MonoBehaviour
    {
        [Tooltip("單例遊戲物件清單，用來確保此遊戲物件只有一個存在，因此此清單只能有一個元素")]
        [SerializeField] private ScriptableListGameObject singletonGameObjects;

        private void Awake()
        {
            if (singletonGameObjects == null || singletonGameObjects.Count > 0)
            {
                Debug.LogError("SingletonGameObjectApplier: singletonGameObjects is null or already has elements, name=" + gameObject.name);

                Destroy(gameObject);
                return;
            }

            singletonGameObjects.Add(gameObject);
        }
    }
}