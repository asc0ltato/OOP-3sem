using System.Text;

enum grades
{
    one = 1,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    ten
}

struct Student
{
    public string firstName;
    public string lastName;
    public int course;
    public grades grade;

    public void PrintNameAndCourse()
    {
        Console.WriteLine($"{firstName} {lastName} - {course} course \ngrade - {grade}");
    }
};

interface ICheckable
{
    StringBuilder PrintAllChallenge();
    void Check();
}

interface ICloneable
{
    object DoClone();
}

public abstract class Challenge : ICloneable
{
    public string? subjectName { get; set; }

    public bool passOrNo;
    public abstract bool tryToPass();
    public abstract object DoClone();
}

class Printer
{
    public virtual void IAmPrinting(Challenge obj)
    {
        Console.WriteLine(obj.GetType());
        Console.WriteLine(obj.ToString());
    }
}
public partial class Question : Challenge
{
    public string? question { get; set; }
    public string? questionAnswer { get; set; }
    public string? guess { get; set; }

    public Question()
    {
        question = questionAnswer = guess = null;
    }
    public Question(string question, string questionAnswer)
    {
        this.question = question;
        this.questionAnswer = questionAnswer;
    }

    public override bool tryToPass()
    {
        if (questionAnswer == guess)
        {
            passOrNo = true;
            return passOrNo;
        }
        passOrNo = false;
        return passOrNo;
    }
}

public class Task : Challenge
{
    public string? task { get; set; }
    public string? taskAnswer { get; set; }
    public string? guess { get; set; }

    public Task()
    {
        task = taskAnswer = guess = null;
    }
    public Task(string question, string questionAnswer)
    {
        this.task = question;
        this.taskAnswer = questionAnswer;
    }

    public override bool tryToPass()
    {
        if (taskAnswer == guess)
        {
            passOrNo = true;
            return passOrNo;
        }
        passOrNo = false;
        return passOrNo;
    }

    public override Task DoClone()
    {
        return new Task(task, taskAnswer);
    }

    public override string ToString()
    {
        return task;
    }
}

class Test : Challenge, ICheckable
{
    static Test()
    {
        Console.WriteLine("Для того, чтобы сдать тест, надо ответить правильно как минимум на половину вопросов");
    }
    public int? numberOfQuestions { get; set; }
    private int? numberOfCorrectQuestions;

    public List<Question> questions = new List<Question>();

    public Test()
    {
        numberOfQuestions = 0;
        numberOfCorrectQuestions = 0;
    }
    public Test(List<Question> questions)
    {
        this.questions = questions;
    }
    public void Check()
    {
        Console.WriteLine(" ");
        Console.WriteLine("Проверка результатов...");
        for (int i = 0; i < numberOfQuestions; i++)
        {
            if (questions[i].questionAnswer == questions[i].guess)
            {
                numberOfCorrectQuestions++;
            }
        }
    }
    public void addQuestion(string question, string questionAnswer)
    {
        questions.Add(new Question(question, questionAnswer.ToLower()));
        numberOfQuestions++;
    }
    public StringBuilder PrintAllChallenge()
    {
        var text = new StringBuilder(32);
        for (int i = 0; i < questions.Count; i++)
        {
            text.Append($"{i + 1}." + questions[i].question + '\n');
        }
        return text;
    }
    public override bool tryToPass()
    {
        if (numberOfQuestions - numberOfCorrectQuestions <= numberOfCorrectQuestions)
        {
            Console.WriteLine("Вы сдали");
            passOrNo = true;
            return passOrNo;
        }
        Console.WriteLine("Вы не сдали");
        passOrNo = false;
        return passOrNo;
    }


    public override Test DoClone()
    {
        return new Test(this.questions);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(GetType() + "\n");
        sb.Append(PrintAllChallenge());
        string? text = sb.ToString();
        return text;
    }
}

public class Exam : Challenge, ICheckable
{
    static Exam()
    {
        Console.WriteLine();
        Console.WriteLine("Для того, чтобы сдать экзамен, надо сдать два любых испытания из трех");
    }
    public Question question1;
    public Question question2;
    public Task task;
    public int result = 0;

    public Exam()
    {
        question1 = new Question();
        question2 = new Question();
        task = new Task();
    }
    public Exam(Question question1, Question question2, Task task)
    {
        this.question1 = question1;
        this.question2 = question2;
        this.task = task;
    }

    public StringBuilder PrintAllChallenge()
    {
        var text = new StringBuilder("", 32);
        text.Append($"1." + question1.question + '\n');
        text.Append($"2." + question2.question + '\n');
        text.Append($"3." + task.task + '\n');
        return text;
    }
    public void Check()
    {
        Console.WriteLine(" ");
        Console.WriteLine("Проверка результатов...");
        if (question1.guess == question1.questionAnswer && question2.guess == question2.questionAnswer && task.guess == task.taskAnswer)
        {
            result = 9;
        }
        else if ((question1.guess == question1.questionAnswer && question2.guess == question2.questionAnswer) || (task.guess == task.taskAnswer && question2.guess == question2.questionAnswer) || (task.guess == task.taskAnswer && question1.guess == question1.questionAnswer))
        {
            result = 6;
        }
        else if (question1.guess == question1.questionAnswer || question2.guess == question2.questionAnswer || task.guess == task.taskAnswer)
        {
            result = 3;
        }
    }
    public override bool tryToPass()
    {
        if (result >= 6)
        {
            Console.WriteLine("Вы сдали");
            passOrNo = true;
            return passOrNo;
        }
        Console.WriteLine("Вы не сдали");
        passOrNo = false;
        return passOrNo;
    }

    public override Exam DoClone()
    {
        return new Exam(question1, question2, task);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(PrintAllChallenge() + "\n");
        string? text = sb.ToString();
        return text;
    }
}

sealed class FinalExam : Exam
{
    public new int id = 0;
    public FinalExam(Question question1, Question question2, Task task)
    {
        id++;
        this.question1 = question1;
        this.question2 = question2;
        this.task = task;
    }

    static FinalExam()
    {
        Console.WriteLine("Для того, чтобы сдать финальный экзамен, надо сдать все три испытания");
    }

    public override bool tryToPass()
    {
        if (result == 9)
        {
            Console.WriteLine("Вы сдали");
            passOrNo = true;
            return passOrNo;
        }
        Console.WriteLine("Вы не сдали");
        passOrNo = false;
        return passOrNo;
    }

    public override FinalExam DoClone()
    {
        return new FinalExam(question1, question2, task);
    }
}

public class Container
{
    public List<object> containerList { get; set; }

    public Container()
    {
        containerList = new List<object>();
    }
    public void Add(object ob)
    {
        containerList.Add(ob);
    }
    public void Remove(object ob)
    {
        containerList.Remove(ob);
    }
    public string PrintList()
    {
        string text = "";
        for (int i = 0; i < containerList.Count; i++)
        {
            text = text + containerList[i].ToString();
        }
        return text;
    }

}

public class Control
{
    Container session { get; set; }

    public Control(Container session)
    {
        this.session = session;
    }

    public int GetQuantityBySubject(string name)
    {
        int counter = 0;
        for (int i = 0; i < session.containerList.Count; i++)
        {
            if (session.containerList[i] is Exam || session.containerList[i] is FinalExam)
            {
                Exam? obj1 = session.containerList[i] as Exam;
                if (obj1.subjectName == name)
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    public int GetTotalExamsAndFinalExamsCount()
    {
        int counter = 0;
        foreach (var item in session.containerList)
        {
            if (item is Exam || item is FinalExam)
            {
                counter++;
            }
        }
        return counter;
    }

    public int GetTestsByQuestionCount(int questionCount)
    {
        int counter = 0;
        foreach (var item in session.containerList)
        {
            if (item is Test)
            {
                Test? test = item as Test;
                if (test.numberOfQuestions == questionCount)
                {
                    counter++;
                }
            }
        }
        return counter;
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Student sveta;
        sveta.firstName = "Zhuk";
        sveta.lastName = "Sveltana";
        sveta.course = 2;
        sveta.grade = grades.eight;
        sveta.PrintNameAndCourse();

        Exam exam = new Exam(new Question("Какое любимле число у Чайки?", "7"), new Question("В каком учебном заведении Вы учитесь?", "Бгту"), new Task("5+1", "6"));
        exam.subjectName = "ОАиП";

        FinalExam finalExam = new FinalExam(new Question("Сколько жизней у кота?", "7"), new Question("Какая фамилия у Белодеда?", "Белодед"), new Task("2+3", "5"));
        finalExam.subjectName = "ОАиП";

        Test test = new Test();
        test.addQuestion("Ты учишься в бгту?", "Да");
        test.addQuestion("Тебе нравится?", "Нет");
        test.subjectName = "ООП";

        Container container = new Container();
        container.Add(exam);
        container.Add(finalExam);
        container.Add(test);
        Console.WriteLine(container.PrintList());

        Control session = new Control(container);
        Console.WriteLine("Количество экзаменов по предмету 'ОАиП': " + session.GetQuantityBySubject("ОАиП"));
        Console.WriteLine("Общее количество экзаменов и финальных экзаменов: " + session.GetTotalExamsAndFinalExamsCount());
        Console.WriteLine("Количество тестов с 2 вопросами: " + session.GetTestsByQuestionCount(2));
    }
}