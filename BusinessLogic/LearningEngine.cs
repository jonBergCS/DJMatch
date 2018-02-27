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
    public class LearningEngine
    {
        private DecisionTree dtree;
        private DJMatchEntities db = new DJMatchEntities();

        public LearningEngine()
        {
            this.Learn();
        }

        private void Learn()
        {
            List<DecisionVariable> dvar = new List<DecisionVariable>();
            dvar.Add(new DecisionVariable("Budget", 4));
            dvar.Add(new DecisionVariable("Genre", DecisionVariableKind.Discrete));
            dvar.Add(new DecisionVariable("Area", 4));

            this.dtree = new DecisionTree(dvar, 100);
        }

        public List<DJ> GetDJsForClient(int userID)
        {
            // get user answers
            //var userAnswers = 

            return null;
        }
    }
}
