using System.Text;

namespace Lab15
{
    static partial class TPL
    {
        public async static void Task8()
        {
            using (FileStream fstream = new FileStream("file.txt", FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.Default.GetBytes("SomeText");
                await fstream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
