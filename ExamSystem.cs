using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem
{
    public class ExamSystem
    {
        private readonly List<Question> questionBank;

        public ExamSystem()
        {
            questionBank = new List<Question>();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("");
                Console.WriteLine("EXAMINATION SYSTEM");
                Console.WriteLine("");
                Console.WriteLine("1. Doctor Mode");
                Console.WriteLine("2. Student Mode");
                Console.WriteLine("3. Exit");
                Console.WriteLine("");

                int choice = ReadInt(
                    "Enter your choice: ",
                    1,
                    3);

                switch (choice)
                {
                    case 1:
                        DoctorMode();
                        break;

                    case 2:
                        StudentMode();
                        break;

                    case 3:
                        Console.WriteLine("Goodbye!");
                        return;
                }
            }
        }

        // =====================================
        // Doctor Mode
        // =====================================

        public void DoctorMode()
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("          DOCTOR MODE");
            Console.WriteLine("");

            int numberOfQuestions =
                ReadPositiveInt(
                    "Enter number of questions to add: ");

            for (int i = 0; i < numberOfQuestions; i++)
            {
                Console.Clear();

                Console.WriteLine(
                    $"Question {i + 1} of {numberOfQuestions}");

                Console.WriteLine("---------------------------------");

                AddQuestion();
            }

            Console.WriteLine();
            Console.WriteLine("Questions added successfully!");

            Pause();
        }

        private void AddQuestion()
        {
            Console.WriteLine("Select Question Type:");
            Console.WriteLine("1. True / False");
            Console.WriteLine("2. Choose One");
            Console.WriteLine("3. Multiple Choice");

            int type = ReadInt(
                "Enter type: ",
                1,
                3);

            QuestionLevel level = ReadLevel();

            Console.Write("Enter question header: ");

            string header = Console.ReadLine() ?? "";

            int marks = ReadPositiveInt(
                "Enter marks: ");

            switch (type)
            {
                case 1:
                    AddTrueFalseQuestion(
                        header,
                        marks,
                        level);
                    break;

                case 2:
                    AddChooseOneQuestion(
                        header,
                        marks,
                        level);
                    break;

                case 3:
                    AddMultipleChoiceQuestion(
                        header,
                        marks,
                        level);
                    break;
            }
        }

        // =====================================
        // Add True / False Question
        // =====================================

        private void AddTrueFalseQuestion(
            string header,
            int marks,
            QuestionLevel level)
        {
            Console.WriteLine();
            Console.WriteLine("Correct Answer:");
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");

            int answer = ReadInt(
                "Enter correct answer: ",
                1,
                2);

            bool correctAnswer = answer == 1;

            Question question =
                new TrueFalseQuestion(
                    header,
                    marks,
                    level,
                    correctAnswer);

            questionBank.Add(question);
        }

        // =====================================
        // Add Choose One Question
        // =====================================

        private void AddChooseOneQuestion(
            string header,
            int marks,
            QuestionLevel level)
        {
            string[] choices = ReadChoices();

            int correctChoice =
                ReadInt(
                    "Enter correct choice number: ",
                    1,
                    4);

            Question question =
                new ChooseOneQuestion(
                    header,
                    marks,
                    level,
                    choices,
                    correctChoice);

            questionBank.Add(question);
        }

        // =====================================
        // Add Multiple Choice Question
        // =====================================

        private void AddMultipleChoiceQuestion(
            string header,
            int marks,
            QuestionLevel level)
        {
            string[] choices = ReadChoices();

            int[] correctChoices;

            while (true)
            {
                Console.Write(
                    "Enter correct choices separated by comma (e.g. 1,3): ");

                string input =
                    Console.ReadLine() ?? "";

                try
                {
                    correctChoices = input
                        .Split(',')
                        .Select(x => int.Parse(x.Trim()))
                        .Distinct()
                        .ToArray();

                    if (correctChoices.Length == 0 ||
                        correctChoices.Any(
                            x => x < 1 || x > 4))
                    {
                        Console.WriteLine(
                            "Please enter valid choices between 1 and 4.");

                        continue;
                    }

                    break;
                }
                catch
                {
                    Console.WriteLine(
                        "Invalid input. Example: 1,3");
                }
            }

            Question question =
                new MultipleChoiceQuestion(
                    header,
                    marks,
                    level,
                    choices,
                    correctChoices);

            questionBank.Add(question);
        }

        // =====================================
        // Read Choices
        // =====================================

        private string[] ReadChoices()
        {
            string[] choices = new string[4];

            Console.WriteLine();
            Console.WriteLine("Enter 4 choices:");

            for (int i = 0; i < 4; i++)
            {
                Console.Write(
                    $"Choice {i + 1}: ");

                choices[i] =
                    Console.ReadLine() ?? "";
            }

            return choices;
        }

        // =====================================
        // Student Mode
        // =====================================

        public void StudentMode()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("         STUDENT MODE");
            Console.WriteLine("=================================");

            if (questionBank.Count == 0)
            {
                Console.WriteLine(
                    "No questions available.");

                Console.WriteLine(
                    "Please ask the doctor to add questions first.");

                Pause();

                return;
            }

            ExamType examType =
                ReadExamType();

            QuestionLevel level =
                ReadLevel();

            List<Question> availableQuestions =
                questionBank
                    .Where(q => q.Level == level)
                    .ToList();

            if (availableQuestions.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "There are no questions for this level.");

                Pause();

                return;
            }

            List<Question> examQuestions;

            if (examType == ExamType.Practical)
            {
                int numberOfQuestions =
                    Math.Max(
                        1,
                        availableQuestions.Count / 2);

                examQuestions =
                    availableQuestions
                        .Take(numberOfQuestions)
                        .ToList();
            }
            else
            {
                examQuestions =
                    availableQuestions;
            }

            StartExam(
                examQuestions,
                examType,
                level);
        }

        // =====================================
        // Start Exam
        // =====================================

        private void StartExam(
            List<Question> examQuestions,
            ExamType examType,
            QuestionLevel level)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("             EXAM");
            Console.WriteLine("=================================");

            Console.WriteLine(
                $"Exam Type : {examType}");

            Console.WriteLine(
                $"Level     : {level}");

            Console.WriteLine(
                $"Questions : {examQuestions.Count}");

            Console.WriteLine("=================================");

            Pause();

            int studentScore = 0;

            int totalMarks =
                examQuestions.Sum(q => q.Marks);

            for (int i = 0;
                 i < examQuestions.Count;
                 i++)
            {
                Question question =
                    examQuestions[i];

                Console.Clear();

                Console.WriteLine(
                    $"Question {i + 1} of {examQuestions.Count}");

                Console.WriteLine(
                    $"Marks: {question.Marks}");

                Console.WriteLine("---------------------------------");

                // Polymorphism
                question.Display();

                Console.WriteLine();

                Console.Write("Your Answer: ");

                string answer =
                    Console.ReadLine() ?? "";

                // Polymorphism
                bool isCorrect =
                    question.CheckAnswer(answer);

                if (isCorrect)
                {
                    studentScore += question.Marks;
                }

                Console.WriteLine();

                if (isCorrect)
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Wrong!");
                }

                Pause();
            }

            ShowResult(
                studentScore,
                totalMarks);
        }

        // =====================================
        // Show Result
        // =====================================

        private void ShowResult(
            int score,
            int totalMarks)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("          EXAM RESULT");
            Console.WriteLine("=================================");

            Console.WriteLine();

            Console.WriteLine(
                $"Your Result: {score} / {totalMarks}");

            if (totalMarks > 0)
            {
                double percentage =
                    (double)score / totalMarks * 100;

                Console.WriteLine(
                    $"Percentage: {percentage:F2}%");
            }

            Console.WriteLine();
            Console.WriteLine("=================================");

            Pause();
        }

        // =====================================
        // Read Level
        // =====================================

        private QuestionLevel ReadLevel()
        {
            Console.WriteLine();
            Console.WriteLine("Select Level:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");

            int choice =
                ReadInt(
                    "Enter level: ",
                    1,
                    3);

            return (QuestionLevel)choice;
        }

        // =====================================
        // Read Exam Type
        // =====================================

        private ExamType ReadExamType()
        {
            Console.WriteLine();
            Console.WriteLine("Select Exam Type:");
            Console.WriteLine("1. Practical");
            Console.WriteLine("2. Final");

            int choice =
                ReadInt(
                    "Enter exam type: ",
                    1,
                    2);

            return (ExamType)choice;
        }

        // =====================================
        // Read Integer
        // =====================================

        private int ReadInt(
            string message,
            int min,
            int max)
        {
            while (true)
            {
                Console.Write(message);

                string input =
                    Console.ReadLine() ?? "";

                if (int.TryParse(
                    input,
                    out int value) &&
                    value >= min &&
                    value <= max)
                {
                    return value;
                }

                Console.WriteLine(
                    $"Please enter a number between {min} and {max}.");
            }
        }

        // =====================================
        // Read Positive Integer
        // =====================================

        private int ReadPositiveInt(
            string message)
        {
            while (true)
            {
                Console.Write(message);

                string input =
                    Console.ReadLine() ?? "";

                if (int.TryParse(
                    input,
                    out int value) &&
                    value > 0)
                {
                    return value;
                }

                Console.WriteLine(
                    "Please enter a positive number.");
            }
        }

        // =====================================
        // Pause
        // =====================================

        private void Pause()
        {
            Console.WriteLine();
            Console.WriteLine(
                "Press any key to continue...");

            Console.ReadKey();
        }
    }
}