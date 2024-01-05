using UnityEngine;

namespace DigiHero
{
    public class StepProcessor : MonoBehaviour
    {
        [SerializeField] private StepBase[] steps;

        private int currentStepIndex;

        public void StartProcess()
        {
            currentStepIndex = 0;
            ProcessCurrentStep();
        }

        private void ProcessCurrentStep()
        {
            if (currentStepIndex >= steps.Length)
            {
                Debug.Log("process ended");
                return;
            }

            steps[currentStepIndex].Process(OnStepEnded);
        }

        private void OnStepEnded()
        {
            currentStepIndex++;
            ProcessCurrentStep();
        }
    }
}