namespace VertigoDemo.Helpers.BaseMVC
{
    public class BaseController<M, V>
        where M : BaseModel
        where V : BaseView<M>

    {

        private V _view;
        public V View => _view;
    
        private M _model; 
        public M Model => _model;

        public BaseController(M model, V view)
        {
            _model = model;
            _view = view;
            _view.Model = model;
        }
        public void Init() 
        {
            OnInit();
            _view.Init();
        }

        public void DeInit()
        {
            OnDeInit();
            _view.DeInit();
        }

        protected virtual void OnDeInit()
        {
            
        }

        protected virtual void OnInit()
        {
        }
    }
}