Console.WriteLine("Enter Brainfuck: "); 
string input = Console.ReadLine()!; 
var interpreter = new BrainfuckInterpreter();
interpreter.Interpret(input!); 