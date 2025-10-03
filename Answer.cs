namespace _07_Mini_Project
{
    class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public override string ToString()
        {
            return $"{Text}";
        }
    }
    class AnswerList: List<Answer> { }
}
