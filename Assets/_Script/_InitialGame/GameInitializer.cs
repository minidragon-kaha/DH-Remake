using UnityEngine;

namespace DigiHero.Initial
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private InitialStepBase[] initialSteps;

        private int currentStepIndex;

        private void Start()
        {
            currentStepIndex = 0;
            ProcessCurrentStep();
        }

        private void ProcessCurrentStep()
        {
            if (currentStepIndex >= initialSteps.Length)
            {
                Debug.Log("Initialization ended");
                return;
            }

            Debug.Log("Initialization step " + currentStepIndex);
            initialSteps[currentStepIndex].Process(OnStepEnded);
        }

        private void OnStepEnded()
        {
            currentStepIndex++;
            ProcessCurrentStep();
        }
    }
}