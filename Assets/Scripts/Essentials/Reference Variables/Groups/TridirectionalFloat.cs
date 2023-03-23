using Essentials.Reference_Variables.Variables;
using UnityEngine;

namespace Essentials.Reference_Variables.Groups
{
    [CreateAssetMenu(fileName = "3D Float", menuName = "Variables/Groups/Tridirectional Float")]
    public class TridirectionalFloat : BidirectionalFloat
    {
        public FloatVariable Forward;
        public FloatVariable Backward;

        public float Z
        {
            get
            {
                return Forward.Value - Backward.Value;
            }
        }
    }
}
