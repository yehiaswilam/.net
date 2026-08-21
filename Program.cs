using ExaminationSystem;

namespace ExaminationSystem
{
    public enum QuestionLevel
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public enum ExamType
    {
        Practical = 1,
        Final = 2
    }

    public abstract class Question
    {
        public string Header { get; set; }
        public int Marks { get; set; }
        public QuestionLevel Level { get; set; }

        public Question(
            string header,
            int marks,
            QuestionLevel level)
        {
            Header = header;
            Marks = marks;
            Level = level;
        }

        public abstract void Display();

        public abstract bool CheckAnswer(string answer);
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        ExamSystem examSystem =
            new ExamSystem();

        examSystem.Run();
    }
}
