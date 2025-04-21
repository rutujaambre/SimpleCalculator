
using System;
    class Program
    {
        /// <summary>
        /// Entry point: Parses and evaluates a simple arithmetic expression passed via command-line.
        /// </summary>

        static void Main(string[] args)     //accepts command-line-argument
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Arguments not Matched");
                Console.WriteLine("Usage: dotnet run \"expression\"");
                Environment.Exit(1);  // Exit with non-zero status
            }

            string input = args[0];    //stores i/p in variable "input var"

            string numbersTemp = "";            //temp hold digit while parsing
            int[] numbers = new int[100];       //store parsed numbers
            char[] operators = new char[100];   //stores parsed operators
            int numCount = 0, opCount = 0;      //counter number of digits&operators

            // Validate and separate numbers/operators
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (char.IsDigit(c))
                {
                    numbersTemp += c;
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    if (numbersTemp == "")
                    {
                        Console.WriteLine("Error:Operator without starting number");
                        return;
                    }

                    numbers[numCount++] = int.Parse(numbersTemp); // change
                    operators[opCount++] = c;
                    numbersTemp = "";
                }
                else
                {
                    Console.WriteLine("Error: Invalid character found.");
                    return;
                }
            }

            if (numbersTemp == "")
            {
                Console.WriteLine("Error: Expression ends with an operator.");
                return;
            }
            numbers[numCount++] = int.Parse(numbersTemp);

            // First pass: handle * and /
            int[] firstPassNumbers = new int[100];      //store the updated numbers after evaluating all * and /
            char[] firstPassOperators = new char[100];      //stores only the remaining operators after * and / are already evaluated.
            int idx = 0;                                //pointer (index) to keep track of the position in the firstPassNumbers array
            firstPassNumbers[idx++] = numbers[0];       //copies the first number into the new array

            for (int i = 0; i < opCount; i++)
            {
                if (operators[i] == '*')
                {
                    firstPassNumbers[idx - 1] *= numbers[i + 1];
                }
                else if (operators[i] == '/')
                {
                    if (numbers[i + 1] == 0)
                    {
                        Console.WriteLine("Error: Division by zero.");
                        return;
                    }
                    firstPassNumbers[idx - 1] /= numbers[i + 1];
                }
                else
                {
                    firstPassOperators[idx - 1] = operators[i];
                    firstPassNumbers[idx++] = numbers[i + 1];
                }
            }

            // Second pass: handle + and -
            int result = firstPassNumbers[0];
            for (int i = 0; i < idx - 1; i++)
            {
                if (firstPassOperators[i] == '+')
                    result += firstPassNumbers[i + 1];
                else if (firstPassOperators[i] == '-')
                    result -= firstPassNumbers[i + 1];
            }

            Console.WriteLine("Result: " + result);
        }
    }


