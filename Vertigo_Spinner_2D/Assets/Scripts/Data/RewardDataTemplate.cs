using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/RewardDataTemplate", fileName = "New RewardView Data")]
public class RewardDataTemplate : ScriptableObject
{
    public string RewardID;
    public Sprite RewardImage;
    public int RewardAmount;
    public bool IsBoom;
    public RewardType RewardType;
}