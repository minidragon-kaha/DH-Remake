using UnityEngine;

namespace DigiHero
{
    public abstract class StepBase : MonoBehaviour
    {
        public abstract void Process(System.Action onEnded);
    }
}