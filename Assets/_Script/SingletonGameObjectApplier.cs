using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/SingletonGameObjectApplier")]
    public class SingletonGameObjectApplier : MonoBehaviour
    {
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