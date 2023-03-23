using System;
using Essentials.Reference_Variables.Variables;
using UnityEngine;

namespace Essentials.Reference_Variables.References
{
    [Serializable]
    public class BoolReference
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private bool constantValue;
        [SerializeField] private BoolVariable variable;

        public bool Value
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
