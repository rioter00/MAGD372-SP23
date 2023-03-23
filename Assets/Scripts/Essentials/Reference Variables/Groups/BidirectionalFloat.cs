using Essentials.Reference_Variables.Variables;
using UnityEngine;

namespace Essentials.Reference_Variables.Groups
{
    [CreateAssetMenu(fileName = "2D Float", menuName = "Variables/Groups/Bidirectional Float")]
    public class BidirectionalFloat : DirectionalFloat
    {
        public FloatVariable Right;
        public FloatVariable Left;

        public float X
        {
            get
            {
                return Right.Value - Left.Value;
            }
        }
    }
}
