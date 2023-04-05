using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSolver
{
    PatternManager patternManager;
    OutputGrid outputGrid;
    CoreHelper coreHelper;
    PropagationHelper propagationHelper;

    public CoreSolver(OutputGrid outputGrid, PatternManager patternManager)
    {
        this.outputGrid = outputGrid;
        this.patternManager = patternManager;
        this.coreHelper = new CoreHelper(this.patternManager);
        this.propagationHelper = new PropagationHelper(this.outputGrid, this.coreHelper);
    }

    public void Propagate()
    {
        while(propagationHelper.PairsToPropagate.Count > 0)
        {
            var propagatePair = propagationHelper.PairsToPropagate.Dequeue();
            if(propagationHelper.CheckIfPairShouldBeProcessed(propagatePair))
            {
                ProcessCell(propagatePair);
            }
            if(propagationHelper.CheckForConflicts() || outputGrid.CheckIfGridIsSolved())
            {
                return;
            }
        }
        if(propagationHelper.CheckForConflicts() && propagationHelper.PairsToPropagate.Count == 0 && propagationHelper.LowEntropySet.Count == 0)
        {
            propagationHelper.SetConflictFlag();
        }
    }

    private void ProcessCell(VectorPair propagatePair)
    {
        throw new NotImplementedException();
    }
}
