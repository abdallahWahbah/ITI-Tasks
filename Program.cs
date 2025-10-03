namespace _07_Mini_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Subject subject1 = new Subject() { Name = "OOP", Code = "111"};
            Student student1 = new Student() { Name = "Abdallah" };

            Dictionary<Question, AnswerList> practiceExamDictionary = new Dictionary<Question, AnswerList>();

            // questions and answers
            TrueFasleQuestion q1 = new TrueFasleQuestion(5, "C# is OOP language ?", "Q1. ");
            AnswerList answers1 = new AnswerList();
            answers1.Add(new Answer() { Text = "True", IsCorrect = true });
            answers1.Add(new Answer() { Text = "False", IsCorrect = false });
            q1.Answers = answers1;

            TrueFasleQuestion q2 = new TrueFasleQuestion(5, "JavaScript can't run in the browser?", "Q2. ");
            AnswerList answers2 = new AnswerList();
            answers2.Add(new Answer() { Text = "True", IsCorrect = false });
            answers2.Add(new Answer() { Text = "False", IsCorrect = true });
            q2.Answers = answers2;

            ChooseOneQuestion q3 = new ChooseOneQuestion(5, "Which of these is a value type?", "Q3. ");
            AnswerList answers3 = new AnswerList();
            answers3.Add(new Answer() { Text = "string", IsCorrect = false });
            answers3.Add(new Answer() { Text = "int", IsCorrect = true });
            answers3.Add(new Answer() { Text = "object", IsCorrect = false });
            q3.Answers = answers3;

            ChooseAllQuestion q4 = new ChooseAllQuestion(10, "Which of the following are access modifiers in C#?", "Q4. ");
            AnswerList answers4 = new AnswerList();
            answers4.Add(new Answer() { Text = "public", IsCorrect = true });
            answers4.Add(new Answer() { Text = "private", IsCorrect = true });
            answers4.Add(new Answer() { Text = "virtual", IsCorrect = false });
            answers4.Add(new Answer() { Text = "protected", IsCorrect = true });
            q4.Answers = answers4;

            // Adding questions and answers to the dictionary
            practiceExamDictionary.Add(q1, answers1);
            practiceExamDictionary.Add(q2, answers2);
            practiceExamDictionary.Add(q3, answers3);
            practiceExamDictionary.Add(q4, answers4);


            // Asking the user to choose exam type
            Console.WriteLine("Select Exam Type:");
            Console.WriteLine("1. Practice Exam");
            Console.WriteLine("2. Final Exam");
            Console.Write("Enter choice (1 or 2): ");

            string choice = Console.ReadLine().Trim();
            Exam exam;

            if (choice == "1")
            {
                exam = new PracticeExam(20, practiceExamDictionary.Count, subject1, practiceExamDictionary);
            }
            else
            {
                exam = new FinalExam(20, practiceExamDictionary.Count, subject1, practiceExamDictionary);
            }
            Console.WriteLine("---------------------------");
            exam.OnExamStart += student1.Notify;
            exam.ShowExam();
        }
    }
}
