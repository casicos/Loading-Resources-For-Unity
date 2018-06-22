using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Actual position and rotation information for the bone node per frame.
/// </summary>
public abstract class BoneBase : MonoBehaviour
{
    #region <class>

    /// <summary>
    /// Bone position and rotation information.
    /// </summary>
    public class Bone
    {
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;

        public Bone(IList<string> boneFrameInfo)
        {
            LocalPosition = new Vector3(
                float.Parse(boneFrameInfo[1]),
                float.Parse(boneFrameInfo[2]),
                float.Parse(boneFrameInfo[3])
            );

            LocalRotation = new Quaternion(
                float.Parse(boneFrameInfo[4]),
                float.Parse(boneFrameInfo[5]),
                float.Parse(boneFrameInfo[6]),
                float.Parse(boneFrameInfo[7])
            );
        }
    };

    /// <summary>
    /// Bone information for node group.
    /// </summary>
    public class BoneFrame
    {
        public readonly Bone[] BoneNodeGroup;

        /// <summary>
        /// Constructor for the serialize from the loaded csv data.
        /// </summary>
        /// <param name="animationFrame">Used to serialize the <see cref="BoneNodeGroup"/></param>
        public BoneFrame(ICollection<string> animationFrame)
        {
            var boneNodeGroup = Enumerable.Range(0, animationFrame.Count / 8)
                .Select(startIndex =>
                    new Bone(animationFrame
                        .Where((n, index) => index >= startIndex * 8 && index < (startIndex + 1) * 8)
                        .ToList())
                ).ToList();
            
            // <Carey> Exception for the nullable elements in this list.
            boneNodeGroup.RemoveAll(boneNode => boneNode == null);
            
            BoneNodeGroup = boneNodeGroup.ToArray();
        }

        public BoneFrame(int size)
        {
            BoneNodeGroup = new Bone[size];
        }
    }

    /// <summary>
    /// Bone nodes' frame information.
    /// </summary>
    public class AnimationBone
    {
        // TODO: <Carey> Consts should be related with the parts index.
        private const int HipsIndex = 0;
		
        public readonly List<BoneFrame> BoneFrameGroup;

        public AnimationBone(List<BoneFrame> boneFrameGroup, Vector3 defaultEulerRotation)
        {
            // <Carey> Exception for the nullable elements in this list.
            boneFrameGroup.RemoveAll(boneFrame => boneFrame == null);
            
            BoneFrameGroup = boneFrameGroup;
                        
            // <Carey> Allocate the default euler rotation.
            foreach (var boneFrame in BoneFrameGroup)
            {
                var hipsLocalEulerAngles = boneFrame.BoneNodeGroup[HipsIndex].LocalRotation.eulerAngles;
                hipsLocalEulerAngles += defaultEulerRotation;
                boneFrame.BoneNodeGroup[HipsIndex].LocalRotation = Quaternion.Euler(hipsLocalEulerAngles);
            }					
        }
    }


    #endregion </class>
}