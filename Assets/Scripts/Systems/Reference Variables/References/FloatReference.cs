using System;
using Essentials.Variables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Essentials.References
{
    [Serializable]
    public class FloatReference
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private float constantValue;
        [SerializeField] private FloatVariable variable;

        public float Value
        {
            get
            {
                return useConstant ? constantValue : variable.Value;
            }
            set
            {
                if (useConstant)
                {
                    constantValue = value;
                    return;
                }
                variable.Value = value;

            }
        }
    }
}
