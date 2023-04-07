using VertigoDemo.Data;
using VertigoDemo.Helpers.BaseMVC;

namespace VertigoDemo.UI.RewardMVC
{
    public class RewardModel : BaseModel
    {
        public int RewardAmount;
        public bool IsExplosive;
        public RewardType RewardType;
        public bool IsCurrency;
    }
}
