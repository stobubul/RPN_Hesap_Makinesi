using System;
using System.Collections.Generic;
using System.IO;

namespace RPN_Calculator
{
    // Calculator sınıfı RPN hesaplamalarını yürütür.
    internal class Calculator
    {
        // Operatörlerin sembol ile sınıf eşlemesini tutar
        public readonly Dictionary<string, Operator> _operators;

        public Calculator()
        {
            // '+', '-', '*', '/' operatörleri ilgili sınıflarla eşleniyor.
            _operators = new Dictionary<string, Operator>()
            {
                { "+", new Adder() },
                { "-", new Subtracter() },
                { "*", new Multiplier() },
                { "/", new Divider() }
            };
        }

        // Kullanıcıdan alınan ifadeyi değerlendirir.
        public void Evaluate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                // Boş ifade hatası
                Console.WriteLine("! HATA: Boş ifade girildi.");
                Logger("! HATA: Boş ifade girildi.", "Evaluate()", input);
                return;
            }

            Stack<Operand> stack = new Stack<Operand>(); // RPN işlemleri için stack
            string[] tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> errorMessages = new List<string>(); // Tüm hataları biriktirmek için list yapısı

            foreach (string token in tokens)
            {
                // Sayıysa operand olarak stack'e eklenir
                if (double.TryParse(token, out double number))
                {
                    stack.Push(new Operand(number));
                }
                // Geçerli operatörse işlem yapılır
                else if (_operators.ContainsKey(token))
                {
                    if (stack.Count < 2)
                    {
                        // Yetersiz operand sayısı
                        errorMessages.Add("! HATA: Yetersiz sayı");
                        continue;
                    }

                    double b = stack.Pop().Value;
                    double a = stack.Pop().Value;

                    // Sıfıra bölme kontrolü
                    if (token == "/" && b == 0)
                    {
                        errorMessages.Add("! HATA: Sıfıra bölme hatası");
                        continue;
                    }

                    // İşlem sonucu tekrar stack'e eklenir
                    double result = _operators[token].Execute(a, b);
                    stack.Push(new Operand(result));
                }
                else
                {
                    // Tanımsız operatör hatası
                    errorMessages.Add($"! HATA: '{token}' tanımsız bir operatör");
                }
            }

            // İşlem tamamlandığında stack kontrolü
            if (errorMessages.Count == 0)
            {
                if (stack.Count == 1)
                {
                    // Doğru sonuç
                    Console.WriteLine($"=> {stack.Pop().Value}");
                }
                else if (stack.Count > 1)
                {
                    // Fazla operand kalmış
                    errorMessages.Add("! HATA: İşlem sonunda birden fazla değer kaldı.");
                }
                else
                {
                    // Hiç operand kalmamış
                    errorMessages.Add("! HATA: İşlem sonunda hiç değer kalmadı.");
                }
            }

            // Hatalar varsa hem ekrana yaz hem logla
            if (errorMessages.Count > 0)
            {
                foreach (string msg in errorMessages)
                {
                    Console.WriteLine(msg);
                    Logger(msg, "Evaluate()", input);
                }
            }
        }

        // Hataları log dosyasına yazan yardımcı metot
        public static void Logger(string message, string context = null, string input = null)
        {
            // 'log.txt' dosyası /.../RNP_Calculator/bin/Debug dizininde bulunabilir.
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string logEntry = $"[{timestamp}]";

                if (!string.IsNullOrEmpty(context))
                    logEntry += $" [{context}]";

                if (!string.IsNullOrEmpty(input))
                    logEntry += $" İfade: '{input}'";

                logEntry += $" → {message}\n";

                File.AppendAllText("log.txt", logEntry);
            }
            catch
            {
                Console.WriteLine("Log dosyasına yazılamadı.");
            }
        }
    }
}
