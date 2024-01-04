using UnityEngine;

namespace DigiHero
{
    public class ControllerLocker : LockerBase
    {
        public static ControllerLocker Instance { get; private set; }

        [SerializeField] private GameObject controllerRoot;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            controllerRoot.SetActive(!IsLocked);
        }
    }
}
