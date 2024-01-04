using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    public class SingletonGameObjectApplier : MonoBehaviour
    {
        [SerializeField] private ScriptableListGameObject singletonGameObjects;

        private void Awake()
        {
            singletonGameObjects.Add(gameObject);
        }
    }
}