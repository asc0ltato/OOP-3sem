using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WrongType : Exception
{
    public WrongType(string message) : base(message) 
    { 
    }
}