using System;
using Obvious.Soap;
using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("DigiHero/AnimatorVectorParamApplier")]
    public class AnimatorVectorParamApplier : MonoBehaviour
    {
        [SerializeField] private Vector3Variable fromValue;
        [SerializeField] private Animator animator;
        [SerializeField] private string paramName_x;
        [SerializeField] private string paramName_y;
        [SerializeField] private string paramName_z;

        private void Awake()
        {
            fromValue.OnValueChanged += OnValueChanged;
        }

        private void OnValueChanged(Vector3 vector)
        {
            if (!string.IsNullOrEmpty(paramName_x))
                animator.SetFloat(paramName_x, vector.x);

            if (!string.IsNullOrEmpty(paramName_y))
                animator.SetFloat(paramName_y, vector.y);

            if (!string.IsNullOrEmpty(paramName_z))
                animator.SetFloat(paramName_z, vector.z);
        }
    }
}