using System.Collections.Generic;
using Essentials.Reference_Variables.Variables;
using UnityEngine;

namespace Essentials.Reference_Variables.Groups
{
    [CreateAssetMenu(fileName = "Variable List", menuName = "Variables/Groups/List")]
    public class VariableList : ScriptableObject
    {
        [SerializeField] private List<GenericVariable> variables;

        public List<GenericVariable> Variables
        {
            get
            {
                return variables;
            }
        }
    }
}
