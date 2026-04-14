Console.WriteLine("Nhap Brainfuck: "); 
string input = Console.ReadLine()!; 
var interpreter = new BrainfuckInterpreter();
interpreter.Interpret(input!); 