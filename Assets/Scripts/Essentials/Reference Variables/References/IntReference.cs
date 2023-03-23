using System;
using Essentials.Reference_Variables.Variables;
using UnityEngine;

namespace Essentials.Reference_Variables.References
{
    [Serializable]
    public class IntReference
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private int constantValue;
        [SerializeField] private IntVariable variable;

        public int Value
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
