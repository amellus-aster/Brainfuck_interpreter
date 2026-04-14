using System.Text;

public class BrainfuckInterpreter
{
    private const int MEMORY_SIZE = 30000;
    private readonly byte[] _memory;
    private int _dataPointer;
    private int _instructionPointer;
    private string _code;
    private char[] validCommands = { '>', '<', '+', '-', '.', ',', '[', ']' };
    public BrainfuckInterpreter()
    {
        _memory = new byte[MEMORY_SIZE];
        _dataPointer = 0;
        _instructionPointer = 0;
        _code = string.Empty;
    }
    public void Interpret(string rawCode)
    {
        int openedBrackets = 0;
        int closedBrackets = 0;
        StringBuilder sb = new StringBuilder();
        foreach (char c in rawCode)
        {
            if (validCommands.Contains(c)) sb.Append(c);
            if (c == '[') openedBrackets++;
            if (c == ']') closedBrackets++;
        }
        _code = sb.ToString();
        if (openedBrackets != closedBrackets) throw new ArgumentException("Invalid Command");
        while (_instructionPointer < _code.Length)
        {
            char cmd = _code[_instructionPointer];
            ExecuteInstruction(cmd);
            if (cmd == '[' || cmd == ']')
            {
                continue; 
            }
            _instructionPointer++;
        }
    
        


    }
    private void ExecuteInstruction(char cmd)
    {

        switch (cmd)
        {
            case '>':
                _dataPointer++;
                break;
            case '<':
                _dataPointer--;
                break;
            case '+':
                _memory[_dataPointer]++;
                break;
            case '-':
                _memory[_dataPointer]--;
                break;
            case '.':
                Console.Write((char) _memory[_dataPointer]);
                break;
            case ',':
                var key = Console.ReadKey(true);
                _memory[_dataPointer] = (byte)key.KeyChar;
                break;
            case '[':
                if (_memory[_dataPointer] == 0)
                {
                    _instructionPointer = FindMatchingForwardBracket(_instructionPointer);
                }else _instructionPointer ++; 

                break;
            case ']':
                if (_memory[_dataPointer] != 0)
                {
                    _instructionPointer = FindMatchingBackwardBracket(_instructionPointer);
                } else _instructionPointer ++; 
                break;
            default: return;
        }
    }
    private int FindMatchingForwardBracket(int instructionPointer)
    {
        int depth = 1; 
        for (int i = instructionPointer + 1; i < _code.Length; i++)
        {
            if(_code[i] == '[') depth++; 
            else if (_code[i] == ']') depth--; 
            if(depth == 0) return i + 1; 
        }
        throw new ArgumentException("No Closed Bracket");
    }
    private int FindMatchingBackwardBracket(int instructionPointer)
    {
        int depth = -1; 
        for (int i = instructionPointer - 1; i > 0; i--)
        {
            if(_code[i] == ']') depth --; 
            else if (_code[i] == '[') depth++; 
            if(depth == 0) return i + 1;
        }
        throw new ArgumentException("No Open Bracket");
    }
}