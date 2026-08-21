using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Linq;

namespace ExaminationSystem
{
    public class MultipleChoiceQuestion : Question
    {
        public string[] Choices { get; set; }
        public int[] CorrectChoices { get; set; }

        public MultipleChoiceQuestion(
            string header,
            int marks,
            QuestionLevel level,
            string[] choices,
            int[] correctChoices)
            : base(header, marks, level)
        {
            Choices = choices;
            CorrectChoices = correctChoices;
        }

        public override void Display()
        {
            Console.WriteLine();
            Console.WriteLine(Header);

            for (int i = 0; i < Choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Choices[i]}");
            }

            Console.WriteLine(
                "Choose multiple answers separated by comma.");
        }

        public override bool CheckAnswer(string answer)
        {
            try
            {
                int[] studentAnswers = answer
                    .Split(',')
                    .Select(x => int.Parse(x.Trim()))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToArray();

                int[] correctAnswers = CorrectChoices
                    .Distinct()
                    .OrderBy(x => x)
                    .ToArray();

                return studentAnswers.SequenceEqual(correctAnswers);
            }
            catch
            {
                return false;
            }
        }
    }
}