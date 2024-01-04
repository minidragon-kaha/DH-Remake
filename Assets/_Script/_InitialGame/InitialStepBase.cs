using UnityEngine;

namespace DigiHero.Initial
{
    public abstract class InitialStepBase : MonoBehaviour
    {
        public abstract void Process(System.Action onEnded);
    }
}