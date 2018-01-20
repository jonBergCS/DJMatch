using Accord.MachineLearning.DecisionTrees;
using Accord.Neuro;
using DJMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class LearningEngine
    {
        private static DecisionTree dtree;
        private static DJMatchEntities db = new DJMatchEntities();

        static LearningEngine()
        {
            Learn();
        }

        private static void Learn()
        {
            List<DecisionVariable> dvar = new List<DecisionVariable>();
            dvar.Add(new DecisionVariable("Budget", 4));
            dvar.Add(new DecisionVariable("Genre", DecisionVariableKind.Discrete));
            dvar.Add(new DecisionVariable("Area", 4));

            dtree = new DecisionTree(dvar, 100);
        }
    }
}
