using System;
using System.Collections.Generic;
using System.Text;

namespace Edit
{

    interface IEdit
    {
        void Delete();
    }

    public abstract class Redactor
    {
        public abstract void Delete();
        public StringBuilder? Text { get; set; }
    }

    public class Document : Redactor, IEdit
    {
        public Document(string text)
        {
            this.Text = new StringBuilder(text);
        }

        public override void Delete()
        {
            string[] words = Text.ToString().Split(' ');
            string firstWord = words[0];
            Text.Clear();
            Text.Append(firstWord);
        }

        void IEdit.Delete()
        {
            string trimmedText = System.Text.RegularExpressions.Regex.Replace(Text.ToString(), @"\s+", " ");
            Text.Clear();
            Text.Append(trimmedText);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Document other = (Document)obj;
            return this.Text == other.Text;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual void Print(string filePath)
        {
            File.WriteAllText(filePath, Text.ToString());
        }
    }

    public class Book : Document
    {
        public Book(string text) : base(text)
        {
        }

        public override void Print(string filePath)
        {
            File.WriteAllText(filePath, Text.ToString());
        }
    }

    public static class BookExtensions
    {
        public static void ToBeContinue(this Book book)
        {
            book.Text.Append("To be continued...");
        }
    }

    class Programm 
    {
        static void Main(string[] args)
        {
            Document doc = new Document("текст         для задания");
            ((IEdit)doc).Delete();
            doc.Print("doc_output.txt");
            doc.Delete();
            doc.Print("doc_deleted_output.txt");

            Document doc1 = new Document("mama моя    лучшая");
            Document doc2 = new Document("mama моя    лучшая");
            Document doc3 = new Document("mama моя самая лучшая");

            Book book1 = new Book("Алиса в пограничье. Ты красотка");
            Book book2 = new Book("Алиса в зазеркалье");

            List<Document> archive = new List<Document>();
            archive.Add(doc1);
            archive.Add(doc2);
            archive.Add(doc3);
            archive.Add(book1);
            archive.Add(book2);

            foreach (Document item in archive)
            {
                ((IEdit)item).Delete();
                item.Print($"{DateTime.Now:yyyyMMdd}_output.txt");
                item.Delete();
                item.Print($"{DateTime.Now:yyyyMMdd}_deleted_output.txt");

                if (item is Book book)
                {
                    book.ToBeContinue();
                    book.Print($"{DateTime.Now:yyyyMMdd}_continued_output.txt");
                }
            }
        }
    }
}

 