using UnityEngine;

namespace VertigoDemo.Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/RewardDataTemplate", fileName = "New RewardView Data")]
    public class RewardDataTemplate : ScriptableObject
    {
        public string RewardID;
        public Sprite RewardImage;
        public int RewardAmount;
        public bool IsExplosive;
        public bool IsCurrency;
        public RewardType RewardType;
    }
}