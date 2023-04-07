using UnityEngine;

namespace VertigoDemo.Data
{
    public class Enums : MonoBehaviour
    {
    }

    public enum RewardType
    {
        EMP,
        HealthShot,
        MedKit,
        Grenade,
        Money,
        Gold,
        C4
    }

    public enum GameState
    {
        Start,
        Spin,
        Decision,
        Reward,
        GameOver
    }
}