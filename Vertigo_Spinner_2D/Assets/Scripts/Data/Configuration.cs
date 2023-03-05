using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Configuration : MonoBehaviour
{
    public static List<RewardDataTemplate> AllRewardData = Resources.LoadAll<RewardDataTemplate>("Data/Reward Datas").ToList();
    
    public static class SpinnerSprites
    {
        public static Sprite SilverSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_silver_base");
        public static Sprite SilverIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_silver_indicator");
        public static Sprite SuperSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_golden_base");
        public static Sprite SuperIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_golden_indicator");
        public static Sprite BronzeSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_bronze_base");
        public static Sprite BronzeIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_bronze_indicator");
    }

    public static class RewardData
    {
        public static List<RewardDataTemplate> AllRewardData =
            Resources.LoadAll<RewardDataTemplate>("Data/Reward Datas").ToList();
        
        public static List<RewardDataTemplate> AllSafeRewardData = AllRewardData.Where(c => c.IsBoom == false).ToList();
        
    }

}
