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
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace DJMatch
{
    public class LearningEngine
    {
        //private DecisionTree dtree;
        private DJMatchEntities db = new DJMatchEntities();
        dynamic machine;
        private Codification code = new Codification();

        public LearningEngine()
        {
            // Do asynchrously
            this.Learn();
        }

        private void Learn()
        {
            double[][] inputs = db.Users.Where(user => user.Events.Count > 0)
                .AsEnumerable()
                .Select(user => this.UserToVector(user.ID))
                .ToArray();

            List<int> outputList = new List<int>();

            foreach (double[] currVect in inputs)
            {
                outputList.Add(db.Users.Find(currVect.Last()).Events.GroupBy(ev => ev.DJId).
                  OrderByDescending(grp => grp.Count()).
                  Select(u => u.Key).
                  FirstOrDefault());
            }

            int[] outputs = outputList.ToArray();

            // Create a learning algorithm
            // Create a one-vs-one multi-class SVM learning algorithm 

            var teacher = new RandomForestLearning()
            {
                NumberOfTrees = 5,
                //SampleRatio = 1.0
            };

            machine = teacher.Learn(inputs, outputs);

            // Compute the error in the learning
            double error = new ZeroOneLoss(outputs).Loss(machine.Decide(inputs));
            Console.WriteLine("ERROR - " + error);
        }

        public DJ GetRecommendedDJ(int userID)
        {
            try
            {
                int chosenOne = this.machine.Decide(this.UserToVector(userID));

                return db.DJs.Find(chosenOne);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private double[] UserToVector(int userID)
        {
            List<double> vector = new List<double>();
            User user = db.Users.FirstOrDefault((usr) => usr.ID == userID);

            var genres = string.Join(";", user.UserAnswers.Where(ans => ans.QuestionID == 2).Select(ua => ua.Answer.Text)).GetHashCode();

            var ans1 = user.UserAnswers.FirstOrDefault(ans => ans.QuestionID == 1);
            var ans4 = user.UserAnswers.FirstOrDefault(ans => ans.QuestionID == 4);
            var ans3 = user.UserAnswers.FirstOrDefault(ans => ans.QuestionID == 3);
            var ans6 = user.UserAnswers.FirstOrDefault(ans => ans.QuestionID == 6);

            vector.Add(ans1 != null ? ans1.AnswerID : -999);
            vector.Add(ans4 != null ? ans4.AnswerID : -999);
            vector.Add(genres);
            vector.Add(ans3 != null ? ans3.AnswerID : -999);
            vector.Add(ans6 != null ? ans6.AnswerID : -999);
            vector.Add(user.ID);

            return vector.ToArray();
        }
    }
}


