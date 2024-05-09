using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace Lab13
{
    public class CustomSerializer
    {
        public void Serialize<T>(string filename, T obj, string format)
        {
            if (format.ToLower() == "json")
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    AllowTrailingCommas = true
                };
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    JsonSerializer.Serialize<T>(fs, obj, options);
                }
            }
            else if (format.ToLower() == "soap")
            {
                SoapFormatter formatterSoap = new SoapFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    formatterSoap.Serialize(fs, obj);
                }
            }
            else if (format.ToLower() == "xml")
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, obj);
                }
            }
            else if (format.ToLower() == "bit")
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    formatter.Serialize(fs, obj);
                }
            }
            else
            {
                Console.WriteLine("Формат не поддерживается для сериализации");
                return;
            }
            Console.WriteLine("Объект сериализован");
        }

        public T Deserialize<T>(string filename, string format)
        {
            T result = default(T);
            if (format.ToLower() == "json")
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    result = JsonSerializer.Deserialize<T>(fs);
                }
            }
            else if (format.ToLower() == "bit")
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    result = (T)formatter.Deserialize(fs);
                }
            }
            else if (format.ToLower() == "soap")
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    SoapFormatter formatterSoap = new SoapFormatter();
                    result = (T)formatterSoap.Deserialize(fs);
                }
            }
            else if (format.ToLower() == "xml")
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    result = (T)xmlSerializer.Deserialize(fs);
                }
            }
            else
            {
                Console.WriteLine("Формат не поддерживается для десериализации");
            }
            Console.WriteLine("Объект десериализован");
            return result;
        }
    }

    [Serializable]
    public class Question
    {
        [NonSerialized]
        public int id = 1;
        public string question { get; set; }
        public string questionAnswer { get; set; }

        [XmlIgnore]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Question()
        {
            question = questionAnswer = null;
        }
        public Question(string question, string questionAnswer)
        {
            this.question = question;
            this.questionAnswer = questionAnswer;
        }

        public override string ToString()
        {
            return question;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------Задание 1-----------------------");
            Question que = new Question("Как дела ? ", "Норм");
            CustomSerializer customSerializer = new CustomSerializer();

            customSerializer.Serialize("question.json", que, "json");
            customSerializer.Serialize("question.soap", que, "soap");
            customSerializer.Serialize("question.xml", que, "xml");
            customSerializer.Serialize("question.bit", que, "bit");

            Question deserializedJson = customSerializer.Deserialize<Question>("question.json", "json");
            Question deserializedSoap = customSerializer.Deserialize<Question>("question.soap", "soap");
            Question deserializedXml = customSerializer.Deserialize<Question>("question.xml", "xml");
            Question deserializedBit = customSerializer.Deserialize<Question>("question.bit", "bit");

            Console.WriteLine($"Десериализованный из JSON: {deserializedJson?.id}, {deserializedJson?.question}, {deserializedJson?.questionAnswer}");
            Console.WriteLine($"Десериализованный из SOAP: {deserializedSoap?.id}, {deserializedSoap?.question}, {deserializedSoap?.questionAnswer}");
            Console.WriteLine($"Десериализованный из XML: {deserializedXml?.id}, {deserializedXml?.question}, {deserializedXml?.questionAnswer}");
            Console.WriteLine($"Десериализованный из Binary: {deserializedBit?.id}, {deserializedBit?.question}, {deserializedBit?.questionAnswer}");

            Console.WriteLine("-----------------------Задание 2-----------------------");
            List<Question> questions = new List<Question>
             {
                 new Question("Как дела?", "Норм"),
                 new Question("Что нового?", "Всё хорошо")
             };

            customSerializer.Serialize("questions.xml", questions, "xml");

            List<Question> deserializedQuestions = customSerializer.Deserialize<List<Question>>("questions.xml", "xml");

            foreach (Question q in deserializedQuestions)
            {
                Console.WriteLine($"Десериализованный вопрос: {q.id}, {q.question}, {q.questionAnswer}");
            }

            Console.WriteLine("-----------------------Задание 3-----------------------");
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("questions.xml");
            XmlElement? xRoot = xDoc.DocumentElement;

            XmlNodeList? nodes = xRoot?.SelectNodes("Question");
            if (nodes is not null)
            {
                foreach (XmlNode node in nodes)
                    Console.WriteLine(node.OuterXml);
            }

            Console.WriteLine("-----------------------Задание 4-----------------------");
            XDocument newDoc = new XDocument(
                new XElement("Questions",
                    from q in questions
                    select new XElement("Question",
                        new XElement("question", q.question),
                        new XElement("questionAnswer", q.questionAnswer)
                    )
                )
            );

            newDoc.Save("new_questions.xml");

            XElement root = newDoc.Root;
            IEnumerable<XElement> selectedQuestions = root.Elements("Question");

            foreach (XElement q in selectedQuestions)
            {
                Console.WriteLine($"Вопрос: {q.Element("question").Value}, Ответ: {q.Element("questionAnswer").Value}");
            }
        }
    }
}