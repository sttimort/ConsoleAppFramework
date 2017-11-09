using System;

namespace Application.UI
{
    public partial class AppUI
    {
        public void ShowAccuracy(double? Accuracy)
        {
            Console.Write("Accuracy: ");
            if (!Accuracy.HasValue)
                Console.WriteLine("?");
            else
                Console.WriteLine(Accuracy.Value);
            Console.WriteLine();
        }

        public void ShowBounds(double? upper, double? lower)
        {
            Console.WriteLine("Integration bounds are:");
            if (upper.HasValue && lower.HasValue)
            {
                if (upper.Value > lower.Value)
                    Console.WriteLine($"{lower.Value} >>>>> {upper.Value}");
                else if (upper.Value == lower.Value)
                    Console.WriteLine($"{lower.Value} ===== {upper.Value}");
                else
                    Console.WriteLine($"{upper.Value} <<<<< {lower.Value}");
            }
            else
            {
				var lowerStr = lower.HasValue ? lower.Value.ToString() : "?";
				var upperStr = upper.HasValue ? upper.Value.ToString() : "?";
                
                Console.WriteLine($"{lowerStr} ----- {upperStr}");
            }

            Console.WriteLine(); // additional empty line
        }
    }
}
