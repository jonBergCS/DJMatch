using Accord.MachineLearning.DecisionTrees;
using Accord.Neuro;
using DJMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;

namespace BusinessLogic
{
    public class LearningEngine
    {
        //private DecisionTree dtree;
        private DJMatchEntities db = new DJMatchEntities();

        private DecisionNode dTree;

        public LearningEngine()
        {
            this.BuildDecisionTree();
            
            //this.Learn();
        }

        //private void Learn()
        //{
        //    List<DecisionVariable> dvar = new List<DecisionVariable>();
        //    dvar.Add(new DecisionVariable("Budget", 4));
        //    dvar.Add(new DecisionVariable("Genre", DecisionVariableKind.Discrete));
        //    dvar.Add(new DecisionVariable("Area", 4));

        //    this.dtree = new DecisionTree(dvar, 100);
        //}

        private void BuildDecisionTree()
        {
            this.dTree = new DecisionNode()
            {
                Result = db.DJs.ToList()
            };


        }

        public List<DJ> GetDJsForClient(int userID)
        {
            // get user answers
            //var userAnswers = 

            return null;
        }

    }

    class DecisionNode
    {
        public List<DecisionNode> Branches { get; set; }
        public List<DJ> Result { get; set; }

        public DecisionNode()
        {
            this.Branches = new List<DecisionNode>();
            this.Result = new List<DJ>();
        }

        public List<DJ> Evaluate()
        {


            return null;
        }
        /*
         
        1. Budget
        2. eventType
        3. genres
        4. experience years

        */
    }
}
