namespace _07_Mini_Project
{
    abstract class Question: ICloneable, IComparable<Question>
    {
        public int Marks { get; set; }
        public string Body { get; set; }
        public string Header { get; set; }
        public AnswerList Answers { get; set; }

        public Question(int _marks, string _body, string _header)
        {
            Marks = _marks;
            Body = _body;
            Header = _header;
            Answers = new AnswerList();
        }

        public abstract void Display();

        // interfaces methods
        public object Clone() => this.MemberwiseClone();
        public int CompareTo(Question q)
        {
            return Header.CompareTo(q.Header);
        }
        // overrides
        public override string ToString()
        {
            return $"{Header}{Body} ({Marks} Marks)";
        }
        public override int GetHashCode()
        {
            return Marks.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            Question q = obj as Question;
            if (q == null) return false;
            return Header == q.Header;
        }
    }


    class TrueFasleQuestion: Question
    {
        public TrueFasleQuestion(int _marks, string _body, string _header) : base(_marks, _body, _header) { }

        public override void Display()
        {
            Console.WriteLine(this);
            for(var i = 0; i < Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Answers[i]}");
            }
        }
    }


    class ChooseOneQuestion: Question
    {
        public ChooseOneQuestion(int _marks, string _body, string _header) : base(_marks, _body, _header) { }
        public override void Display()
        {
            Console.WriteLine(this);
            for (var i = 0; i < Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Answers[i].Text}");
            }
        }
    }


    class ChooseAllQuestion : Question
    {
        public ChooseAllQuestion(int _marks, string _body, string _header) : base(_marks, _body, _header) { }
        public override void Display()
        {
            Console.WriteLine(this);
            for (var i = 0; i < Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Answers[i].Text}");
            }
        }
    }


    class QuestionList : List<Question>
    {
        string filePath;
        public QuestionList(string path)
        {
            filePath = path;
        }

        public void Add(Question q)
        {
            base.Add(q);
            File.AppendAllText(filePath, q.ToString() + Environment.NewLine);
        }
    }

}
