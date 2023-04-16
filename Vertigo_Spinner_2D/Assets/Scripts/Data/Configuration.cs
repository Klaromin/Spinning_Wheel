using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VertigoDemo.Data
{
    public class Configuration : MonoBehaviour
    {
        public static List<RewardDataTemplate> AllSafeRewardData;
        public Sprite SilverSpinSprite ;
        public Sprite SilverIndicatorSprite;
        public Sprite SuperSpinSprite;
        public Sprite SuperIndicatorSprite ;
        public Sprite BronzeSpinSprite;
        public Sprite BronzeIndicatorSprite ;
        private void Awake()
        {
            AllSafeRewardData = Resources.LoadAll<RewardDataTemplate>("Data/Reward Datas").Where(c => c.IsExplosive == false).ToList();
            SilverSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_silver_base");
            SilverIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_silver_indicator");
            SuperSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_golden_base");
            SuperIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_golden_indicator");
            BronzeSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_bronze_base");
            BronzeIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_bronze_indicator");
        }
    }
}