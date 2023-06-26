using UnityEngine;

public class ScaleTo : Reaction
{
    public float scaleTo;
    private bool _increase;
    private Vector3 _initialScale;

    //desactivar collider como que se va a otro plano

    protected override void OnInitBeforeReaction(Collider2D collider)
    {
        _initialScale = transform.parent.localScale;
        _increase = _initialScale.x < scaleTo && _initialScale.y < scaleTo;
    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
    {
        var scaleToCalculated = CalcScaleTo(executionData);
        Debug.Log("SCALETO" + scaleToCalculated);
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
        base.OnReactionStopped();
    }
}

