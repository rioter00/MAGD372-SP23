using System;
using Essentials.Reference_Variables.Variables;
using UnityEngine;

namespace Essentials.Reference_Variables.References
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
