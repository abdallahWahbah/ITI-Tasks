namespace _07_Mini_Project
{
    class Subject
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public enum ExamMode { Starting, Queued, Finished }

    abstract class Exam
    {
        public int Time { get; set; }
        public int NumOfQuestions { get; set; }
        public Subject Subject { get; set; }
        public Dictionary<Question, AnswerList> QA { get; set; }
        public ExamMode Mode {  get; set; }
        public Action<string> OnExamStart;

        public Exam(int _time,  int _numOfQuestions, Subject _subject, Dictionary<Question, AnswerList> _qa)
        {
            Time = _time;
            NumOfQuestions = _numOfQuestions;
            Subject = _subject;
            QA = _qa;
            Mode = ExamMode.Queued;
        }

        public abstract void ShowExam();

        protected void RunExam(bool showResultAfterAnswer)
        {
            Mode = ExamMode.Starting;
            OnExamStart?.Invoke($"{Subject.Name} exam is starting");

            int totalScore = 0;
            int totalPoints = QA.Keys.Sum(q => q.Marks);

            foreach (var question in QA.Keys)
            {
                question.Display();
                if (question is ChooseAllQuestion) // Choose all correct answers
                {
                    Console.Write("Your Answer in the format (x, x, x): ");
                    string answerInput = Console.ReadLine();
                    string[] answersStringArray = answerInput.Split(',');
                    HashSet<int> selectedNumbers = new HashSet<int>();
                    HashSet<int> correctIndices = new HashSet<int>();

                    // user indecis
                    foreach (var item in answersStringArray)
                    {
                        if (int.TryParse(item.Trim(), out int num))
                            selectedNumbers.Add(num);
                    }

                    // correct indecis (1-based) to match user input indecis
                    for (var i = 0; i < question.Answers.Count; i++)
                    {
                        if (question.Answers[i].IsCorrect) correctIndices.Add(i + 1);
                    }

                    if (selectedNumbers.SetEquals(correctIndices))
                    {
                        if(showResultAfterAnswer)
                            Console.WriteLine($"Correct Answer {question.Marks}/{question.Marks}");
                        totalScore += question.Marks;
                    }
                    else
                    {
                        if(showResultAfterAnswer)
                            Console.WriteLine($"Wrong Answer {0}/{question.Marks}");
                    }
                }
                else // True-False ((or)) Choose one
                {
                    Console.Write("Your Answer (number): ");
                    string answerInput = Console.ReadLine();
                    int answerNum;
                    if (int.TryParse(answerInput, out answerNum))
                    {
                        if (answerNum > 0 && answerNum <= question.Answers.Count) // the student wrote a valid num for the answers
                        {
                            Answer tempAnswer = question.Answers[answerNum - 1];
                            if (tempAnswer.IsCorrect)
                            {
                                if (showResultAfterAnswer)
                                    Console.WriteLine($"Correct Answer {question.Marks}/{question.Marks}");
                                totalScore += question.Marks;
                            }
                            else
                            {
                                if (showResultAfterAnswer)
                                    Console.WriteLine($"Wrong Answer {0}/{question.Marks}");
                            }
                        }
                    }
                }
                Console.WriteLine("-----------------------");
            }
            Mode = ExamMode.Finished;
            OnExamStart?.Invoke($"{Subject.Name} exam has finished");

            Console.WriteLine($"Your final score is {totalScore}/{totalPoints}");
        }

    }


    class PracticeExam: Exam
    {
        public PracticeExam(int _time, int _numOfQuestions, Subject _subject, Dictionary<Question, AnswerList> _qa) 
            : base(_time, _numOfQuestions, _subject, _qa) { }

        public override void ShowExam() => RunExam(true);
    }
    class FinalExam : Exam
    {
        public FinalExam(int _time, int _numOfQuestions, Subject _subject, Dictionary<Question, AnswerList> _qa)
            : base(_time, _numOfQuestions, _subject, _qa) { }

        public override void ShowExam() => RunExam(false);
    }
}
