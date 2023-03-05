using UnityEngine;

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
    Silver,
    Super,
    GameOver
}
