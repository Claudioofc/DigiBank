using System;
using System.Linq;
using System.Text;

namespace DigiBank.Classes
{
    public static class Utils
    {
        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;
            
            cpf = cpf.Replace(".", "").Replace("-", "").Trim();
            
            if (cpf.Length != 11) return false;
            if (cpf.All(c => c == cpf[0])) return false;
            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            
            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;
            
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];
            
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;
            
            tempCpf += digito1;
            soma = 0;
            
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];
            
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;
            
            return cpf.EndsWith(digito1.ToString() + digito2.ToString());
        }
        
        public static bool ValidarValor(double valor)
        {
            return valor > 0;
        }
        
        public static string CriptografarSenha(string senha)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(senha);
            return Convert.ToBase64String(bytes);
        }
        
        public static bool VerificarSenha(string senha, string hash)
        {
            return CriptografarSenha(senha) == hash;
        }
        
        public static string FormatarValor(double valor)
        {
            return valor.ToString("C2");
        }
        
        public static string FormatarData(DateTime data)
        {
            return data.ToString("dd/MM/yyyy HH:mm:ss");
        }
        
        public static void MostrarLoading(string mensagem)
        {
            Console.Write(mensagem);
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine(" OK");
        }
        
        public static void LimparTela()
        {
            try
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                
                try
                {
                    int width = Console.WindowWidth;
                    int height = Console.WindowHeight;
                    
                    for (int i = 0; i < height; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write(new string(' ', width));
                    }
                    Console.SetCursorPosition(0, 0);
                }
                catch
                {
                }
            }
            catch
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        
        public static void LimparBuffer()
        {
            try
            {
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
            }
            catch
            {
            }
        }
        
        public static void MostrarCabecalho(string titulo)
        {
            Console.WriteLine();
            Console.WriteLine($"  {titulo}");
            Console.WriteLine("  " + new string('=', titulo.Length + 2));
            Console.WriteLine();
        }
        
        public static void MostrarMenu(string[] opcoes, string prompt = "Digite sua opcao: ")
        {
            Console.WriteLine();
            foreach (string opcao in opcoes)
            {
                Console.WriteLine($"  {opcao}");
            }
            Console.WriteLine();
            Console.Write($"  {prompt}");
        }
        
        public static void MostrarErro(string mensagem)
        {
            Console.WriteLine();
            Console.WriteLine($"  ERRO: {mensagem}");
            Console.WriteLine();
        }
        
        public static void MostrarSucesso(string mensagem)
        {
            Console.WriteLine();
            Console.WriteLine($"  OK: {mensagem}");
            Console.WriteLine();
        }
        
        public static void Pausar(int segundos = 2)
        {
            Thread.Sleep(segundos * 1000);
        }
        
        public static string LerTexto(string prompt)
        {
            LimparBuffer();
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }
        
        public static double LerValor(string prompt)
        {
            while (true)
            {
                try
                {
                    LimparBuffer();
                    Console.Write(prompt);
                    double valor = double.Parse(Console.ReadLine() ?? "0");
                    if (ValidarValor(valor))
                        return valor;
                    MostrarErro("Valor deve ser maior que zero!");
                }
                catch (FormatException)
                {
                    MostrarErro("Digite um valor valido!");
                }
            }
        }
        
        public static int LerOpcao(string prompt = "")
        {
            while (true)
            {
                try
                {
                    LimparBuffer();
                    if (!string.IsNullOrEmpty(prompt))
                        Console.Write(prompt);
                    return int.Parse(Console.ReadLine() ?? "0");
                }
                catch (FormatException)
                {
                    MostrarErro("Digite apenas numeros!");
                }
            }
        }
        
        public static bool Confirmar(string mensagem)
        {
            Console.WriteLine($"{mensagem} (S/N)");
            return Console.ReadLine()?.ToUpper() == "S";
        }
    }
}
