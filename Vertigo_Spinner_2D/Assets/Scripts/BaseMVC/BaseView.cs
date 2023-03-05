using UnityEngine;
public abstract class BaseView<M> : MonoBehaviour 
    where M:BaseModel
{
    private M _model;
    public M Model { get; set; }

    public abstract void Init();
    public abstract void DeInit();

}