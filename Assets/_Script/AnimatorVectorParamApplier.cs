using System;
using Obvious.Soap;
using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/AnimatorVectorParamApplier")]
    public class AnimatorVectorParamApplier : MonoBehaviour
    {
        [Tooltip("來源")]
        [SerializeField] private Vector3Variable fromValue;
        [Tooltip("動畫控制器")]
        [SerializeField] private Animator animator;
        [Tooltip("動畫參數名稱_x軸")]
        [SerializeField] private string paramName_x;
        [Tooltip("動畫參數名稱_y軸")]
        [SerializeField] private string paramName_y;
        [Tooltip("動畫參數名稱_z軸")]
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