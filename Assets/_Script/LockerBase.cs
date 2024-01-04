using System.Collections.Generic;
using UnityEngine;

namespace DigiHero
{
    public class LockerBase : MonoBehaviour
    {
        public bool IsLocked { get { return lockers.Count > 0; } }

        private List<object> lockers = new List<object>();

        public void Lock()
        {
            lockers.Add(new object());
        }

        public void Unlock()
        {
            if (lockers.Count > 0)
                lockers.RemoveAt(0);
        }
    }
}