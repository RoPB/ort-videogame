using UnityEngine;

public class ScaleTo : Reaction
{
    public GameObject objectToScale;
    private Vector3 _initialScale;
    public float scaleTo;
    public bool goToOtherLayerWhenScaling;
    private bool _increase;
    

    public ScaleTo() : base("ScaleTo")
    {

    }


    //desactivar collider como que se va a otro plano

    protected override void OnInitBeforeReaction(Collider2D collider, Collision2D collision)
    {
        _initialScale = objectToScale.transform.localScale;
        _increase = _initialScale.x < scaleTo && _initialScale.y < scaleTo;
        if (goToOtherLayerWhenScaling)
        {
            //TODO IMPLEMENT THIS
        }
    }

    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        var scaleToCalculated = CalcScaleTo(executionData);
        //Debug.Log("SCALETO" + scaleToCalculated);
        transform.parent.localScale = scaleToCalculated;
    }

    private Vector3 CalcScaleTo(ExecutionData executionData)
    {
        var scaleToCalc = transform.parent.localScale;

        if (_increase)
        {
            scaleToCalc.x = scaleToCalc.x >= scaleTo ? scaleTo : _initialScale.x + (scaleTo - _initialScale.x) * executionData.progress;
            scaleToCalc.y = scaleToCalc.y >= scaleTo ? scaleTo : _initialScale.y + (scaleTo - _initialScale.y) * executionData.progress;
        }
        else
        {
            scaleToCalc.x = scaleToCalc.x <= scaleTo ? scaleTo : _initialScale.x - (_initialScale.x - scaleTo) * executionData.progress;
            scaleToCalc.y = scaleToCalc.y <= scaleTo ? scaleTo : _initialScale.y - (_initialScale.y - scaleTo) * executionData.progress;
        }

        return scaleToCalc;

    }


    protected override void OnReactionStopped()
    {
        if (goToOtherLayerWhenScaling) {

            //TODO IMPLEMENT THIS
        }

        //ESTO ROMPE TODO
        //if (goToOtherLayerWhenScaling)
        //{
        //    if(_currentCollider!=null)
        //        _currentCollider.enabled = true;
        //}
        
        base.OnReactionStopped();
    }

}