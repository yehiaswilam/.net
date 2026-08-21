using System;
using System.Collections.Generic;
using System.Text;


using System;

namespace ExaminationSystem
{
    public class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }

        public TrueFalseQuestion(
            string header,
            int marks,
            QuestionLevel level,
            bool correctAnswer)
            : base(header, marks, level)
        {
            CorrectAnswer = correctAnswer;
        }

        public override void Display()
        {
            Console.WriteLine();
            Console.WriteLine(Header);
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");
        }

        public override bool CheckAnswer(string answer)
        {
            answer = answer.Trim().ToLower();

            bool studentAnswer;

            if (answer == "1" || answer == "true")
            {
                studentAnswer = true;
            }
            else if (answer == "2" || answer == "false")
            {
                studentAnswer = false;
            }
            else
            {
                return false;
            }

            return studentAnswer == CorrectAnswer;
        }
    }
}