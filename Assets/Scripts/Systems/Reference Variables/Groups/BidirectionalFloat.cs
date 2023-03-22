using UnityEngine;
using Essentials.Variables;
using UnityEngine.Serialization;

namespace Essentials.Groups
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
