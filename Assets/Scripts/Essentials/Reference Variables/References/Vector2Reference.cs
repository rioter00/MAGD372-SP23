using System;
using Essentials.Reference_Variables.Variables;
using UnityEngine;

namespace Essentials.Reference_Variables.References
{
    [Serializable]
    public class Vector2Reference
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private Vector2 constantValue;
        [SerializeField] private Vector2Variable variable;

        public Vector2 Value
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
