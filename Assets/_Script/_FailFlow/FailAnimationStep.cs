using System;

namespace DigiHero.FailFlow
{
    public class FailAnimationStep : StepBase
    {
        public override void Process(Action onEnded)
        {
            UnityEngine.Debug.Log("Fail animation played");
            onEnded?.Invoke();
        }
    }
}