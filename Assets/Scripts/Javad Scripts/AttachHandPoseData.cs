using UnityEngine;

namespace MJUtilities
{
    public class AttachHandPoseData : MonoBehaviour
    {
        public enum HandModelType 
        {
            Left,
            Right
        }
        public HandModelType handType;
    }
}