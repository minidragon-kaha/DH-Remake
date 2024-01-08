using System;

namespace DigiHero.FailFlow
{
    [UnityEngine.AddComponentMenu("_DigiHero/FailFlow/FailAnimationStep")]
    public class FailAnimationStep : StepBase
    {
        public override void Process(Action onEnded)
        {
            UnityEngine.Debug.Log("Fail animation played");
            onEnded?.Invoke();
        }
    }
}