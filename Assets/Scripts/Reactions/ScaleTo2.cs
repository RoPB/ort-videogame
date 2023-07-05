using UnityEngine;

public class ScaleTo2 : Reaction
{
    public float scaleTo;
    public bool goToOtherLayerWhenScaling;
    private bool _increase;
    private Vector3 _initialScale;
    private Collider2D _currentCollider;

    public ScaleTo2() : base("ScaleTo2")
    {

    }

    private void Start()
    {
        _currentCollider = this.gameObject.GetComponentInParent<Collider2D>();
    }

    //desactivar collider como que se va a otro plano

    protected override void OnInitBeforeReaction(Collider2D collider)
    {
        _initialScale = transform.parent.localScale;
        _increase = _initialScale.x < scaleTo && _initialScale.y < scaleTo;
        if (goToOtherLayerWhenScaling)
        {
            _currentCollider.enabled = false;
        }
    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
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
        if (goToOtherLayerWhenScaling)
        {
            if(_currentCollider!=null)
                _currentCollider.enabled = true;
        }
        
        base.OnReactionStopped();
    }

}