using UnityEngine;
using Essentials.Variables;
using UnityEngine.Serialization;

namespace Essentials.Groups
{
    [CreateAssetMenu(fileName = "1D Float", menuName = "Variables/Groups/Directional Float")]
    public class DirectionalFloat : ScriptableObject
    {
        public FloatVariable Up;
        public FloatVariable Down;

        public float Y
        {
            get
            {
                return Up.Value - Down.Value;
            }
        }
    }
}
