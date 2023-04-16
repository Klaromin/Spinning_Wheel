using VertigoDemo.Helpers.BaseMVC;

namespace VertigoDemo.UI.RewardMVC
{
    public class RewardController : BaseController<RewardModel, RewardView>
    {
        /*  -Controller boş ama esas amacı model ile view'ı birbirine bağlamak base controllerdan görülebilir.
            -2. bir görev olarak da View'a eventler controller aracılığı ile de girebilirdi. */
        
        public RewardController(RewardModel model, RewardView view) : base(model, view)
        {
        }
    }
}
