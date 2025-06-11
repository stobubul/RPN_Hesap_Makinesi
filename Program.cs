using System;

namespace RPN_Calculator
{
    // Program sınıfı uygulamanın başlangıç noktasıdır.
    class Program
    {
        static void Main()
        {
            var calculator = new Calculator(); // Hesaplayıcı nesnesi oluşturuluyor

            Console.WriteLine("çıkmak için 'exit'\n\n\n" +
                "=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n" +
                "=-=-=- RNP Hesap Makinesi -=-=-=\n" +
                "=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=" +
                "\nOperatörler: \t+ \t- \t* \t/" +
                "\nÖrnek Format: 15 7 1 1 + − / 3 * 2 1 1 + + −" +
                "\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n\n");

            while (true)
            {
                Console.Write("\n==> "); // Kullanıcıdan giriş istenir
                string input = Console.ReadLine();

                if (input.ToLower() == "exit") // 'exit' ile çıkış yapılır
                    break;

                calculator.Evaluate(input); // Girdi işlenir
            }
        }
    }
}
