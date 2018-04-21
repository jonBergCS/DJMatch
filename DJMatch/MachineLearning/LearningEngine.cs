using Accord.MachineLearning.DecisionTrees;
using DJMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Filters;

namespace DJMatch
{
    public class LearningEngine
    {
        //private DecisionTree dtree;
        private DJMatchEntities db = new DJMatchEntities();
        private DecisionTree dtree;
        private Codification code = new Codification();

        public LearningEngine()
        {
            // Do asynchrously
            //this.Learn();
        }

        private void Learn()
        {
            //List<DecisionVariable> dvar = new List<DecisionVariable>();
            //dvar.Add(new DecisionVariable("Budget", DecisionVariableKind.Discrete));
            //dvar.Add(new DecisionVariable("Eventype", DecisionVariableKind.Discrete));
            //dvar.Add(new DecisionVariable("Genre", DecisionVariableKind.Discrete));
            //dvar.Add(new DecisionVariable("Area", DecisionVariableKind.Discrete));
            //dvar.Add(new DecisionVariable("Experience", DecisionVariableKind.Discrete));
            // Attractions

            //this.dtree = new DecisionTree(dvar, 100);

            
            int[][] inputs =
                (from user in db.Users
                 where user.Events.Count > 0
                 select new int[] {
                     user.UserAnswers.FirstOrDefault(ans => ans.QuestionID == 1).AnswerID,
                     user.UserAnswers.FirstOrDefault(ans => ans.QuestionID == 4).AnswerID,
#pragma warning disable CS0618 // Type or member is obsolete
                     code.Translate(string.Join("; ", user.UserAnswers.Where(ans => ans.QuestionID == 2)))[0],
#pragma warning restore CS0618 // Type or member is obsolete
                     user.UserAnswers.FirstOrDefault(ans => ans.QuestionID == 3).AnswerID,
                     user.UserAnswers.FirstOrDefault(ans => ans.QuestionID == 6).AnswerID,
                     user.ID
                 }).ToArray();

            List<int> outputList = new List<int>();

            foreach (int[] currVect in inputs)
            {
                outputList.Add(db.Users.Find(currVect.Last()).Events.GroupBy(ev => ev.DJId).
                  OrderByDescending(grp => grp.Count()).
                  First().Key);
            }

            int[] outputs = outputList.ToArray();

            // Create an ID3 learning algorithm
            ID3Learning teacher = new ID3Learning();
            // Learn a decision tree for the XOR problem
            dtree = teacher.Learn(inputs, outputs, new double[] {6,5,4,3,2,1,0});

            // Compute the error in the learning
            double error = new ZeroOneLoss(outputs).Loss(dtree.Decide(inputs));
            Console.WriteLine("ERROR - " + error);
        }

        public DJ GetRecommendedDJ(int userID)
        {
            try
            {
                return db.DJs.Find(this.dtree.Decide(this.UserToVector(userID)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private double[] UserToVector(int userID)
        {
            return (from user in db.Users
                    where user.ID == userID
                    select new double[] {
                     user.UserAnswers.First(ans => ans.QuestionID == 1).AnswerID,
                     user.UserAnswers.First(ans => ans.QuestionID == 4).AnswerID,
#pragma warning disable CS0618 // Type or member is obsolete
                     code.Translate(string.Join("; ", user.UserAnswers.Where(ans => ans.QuestionID == 2)))[0],
#pragma warning restore CS0618 // Type or member is obsolete
                     user.UserAnswers.First(ans => ans.QuestionID == 3).AnswerID,
                     user.UserAnswers.First(ans => ans.QuestionID == 6).AnswerID,
                     user.ID
                 }).First();
        }
    }
}
