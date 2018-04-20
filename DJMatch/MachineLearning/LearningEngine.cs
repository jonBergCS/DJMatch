using Accord.MachineLearning.DecisionTrees;
using DJMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;

namespace DJMatch
{
    public class LearningEngine
    {
        //private DecisionTree dtree;
        private DJMatchEntities db = new DJMatchEntities();
        private DecisionTree dtree;

        public LearningEngine()
        {            
            // Do asynchrously
            //this.Learn();
        }

        private void Learn()
        {
            List<DecisionVariable> dvar = new List<DecisionVariable>();
            dvar.Add(new DecisionVariable("Budget", DecisionVariableKind.Discrete));
            dvar.Add(new DecisionVariable("Eventype", DecisionVariableKind.Discrete));
            dvar.Add(new DecisionVariable("Genre", DecisionVariableKind.Discrete));
            dvar.Add(new DecisionVariable("Area", DecisionVariableKind.Discrete));
            dvar.Add(new DecisionVariable("Experience", DecisionVariableKind.Discrete));

            this.dtree = new DecisionTree(dvar, 100);



        }

        public List<DJ> GetDJsForClient(int userID)
        {
            // get user answers
            //var userAnswers = 

            return new List<DJ>();
        }

    }
}
