namespace DigiHero.FailFlow
{
    [UnityEngine.AddComponentMenu("_DigiHero/FailFlow/FailSFXStep")]
    public class FailSFXStep : StepBase
    {
        public override void Process(System.Action onEnded)
        {
            UnityEngine.Debug.Log("Fail SFX played");
            onEnded?.Invoke();
        }
    }
}