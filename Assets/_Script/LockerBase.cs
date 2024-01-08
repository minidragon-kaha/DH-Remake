using System.Collections.Generic;
using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("DigiHero/LockerBase")]
    public class LockerBase : MonoBehaviour
    {
        public bool IsLocked { get { return lockers.Count > 0; } }

        private List<object> lockers = new List<object>();

        public void Lock(object locker)
        {
            lockers.Add(locker);
        }

        public void Unlock(object locker)
        {
            if (lockers.Contains(locker))
                lockers.Remove(locker);
        }
    }
}