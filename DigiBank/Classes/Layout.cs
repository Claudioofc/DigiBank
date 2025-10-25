using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int opcao = 0;
        public static void TelaPrincipal()
        {
            Utils.LimparTela();
            MostrarLogo();
            Console.WriteLine("  Bem-vindo ao DigiBank - Seu banco digital!");
            Console.WriteLine();
            
            string[] opcoes = { "1 - Criar nova conta", "2 - Fazer login", "0 - Sair do sistema" };
            Utils.MostrarMenu(opcoes);

            try
            {
                opcao = Utils.LerOpcao();
            }
            catch (FormatException)
            {
                Utils.MostrarErro("Digite apenas numeros!");
                Utils.Pausar();
                TelaPrincipal();
                return;
            }

            switch (opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;
                case 2:
                    TelaLogin();
                    break;
                case 0:
                    Console.WriteLine();
                    Console.WriteLine("  Obrigado por usar o DigiBank!");
                    Environment.Exit(0);
                    break;
                default:
                    Utils.MostrarErro("Opcao invalida!");
                    Utils.Pausar();
                    TelaPrincipal();
                    break;
            }
        }
        
        private static void MostrarLogo()
        {
            Console.WriteLine();
            Console.WriteLine("  +===============================================+");
            Console.WriteLine("  |                                               |");
            Console.WriteLine("  |              DIGIBANK                          |");
            Console.WriteLine("  |         Seu banco digital                     |");
            Console.WriteLine("  |                                               |");
            Console.WriteLine("  +===============================================+");
            Console.WriteLine();
        }

        private static void TelaCriarConta()
        {
            Utils.LimparTela();
            Utils.MostrarCabecalho("CRIAR NOVA CONTA");
            Console.WriteLine();

            string nome = LerNome();
            string cpf = LerCPF();
            string senha = LerSenha();

            if (pessoas.Any(p => p.CPF == cpf))
            {
                Utils.MostrarErro("CPF ja cadastrado!");
                Utils.Pausar();
                TelaPrincipal();
                return;
            }

            Utils.MostrarLoading("Criando conta");
            Pessoa pessoa = CriarPessoa(nome, cpf, senha);
            pessoas.Add(pessoa);

            MostrarContaCriada(pessoa);
            Utils.Pausar(3);
            TelaContaLogada(pessoa);
        }
        
        private static string LerNome()
        {
            while (true)
            {
                string nome = Utils.LerTexto("Digite seu nome completo: ");
                if (!string.IsNullOrWhiteSpace(nome))
                    return nome;
                Utils.MostrarErro("Nome e obrigatorio!");
            }
        }
        
        private static string LerCPF()
        {
            while (true)
            {
                string cpf = Utils.LerTexto("Digite seu CPF (apenas numeros): ");
                if (Utils.ValidarCPF(cpf))
                    return cpf;
                Utils.MostrarErro("CPF invalido! Digite apenas numeros.");
            }
        }
        
        private static string LerSenha()
        {
            while (true)
            {
                string senha = Utils.LerTexto("Digite sua senha (minimo 6 caracteres): ");
                if (!string.IsNullOrWhiteSpace(senha) && senha.Length >= 6)
                    return senha;
                Utils.MostrarErro("Senha deve ter pelo menos 6 caracteres!");
            }
        }
        
        private static Pessoa CriarPessoa(string nome, string cpf, string senha)
        {
            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();
            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;
            return pessoa;
        }
        
        private static void MostrarContaCriada(Pessoa pessoa)
        {
            Utils.LimparTela();
            Utils.MostrarCabecalho("CONTA CRIADA!");
            Console.WriteLine($"Nome: {pessoa.Nome}");
            Console.WriteLine($"CPF: {pessoa.CPF}");
            Console.WriteLine($"Banco: {pessoa.Conta.GetCodigoBanco()}");
            Console.WriteLine($"Agencia: {pessoa.Conta.GetNumeroAgencia()}");
            Console.WriteLine($"Conta: {pessoa.Conta.GetNumeroConta()}");
            Console.WriteLine();
            Console.WriteLine("Bem-vindo ao DigiBank!");
        }

        private static void TelaLogin()
        {
            Utils.LimparTela();
            Utils.MostrarCabecalho("FAZER LOGIN");
            Console.WriteLine();

            string cpf = Utils.LerTexto("Digite seu CPF: ");
            string senha = Utils.LerTexto("Digite sua senha: ");

            Utils.MostrarLoading("Verificando credenciais");

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.VerificarSenha(senha));

            if (pessoa != null)
            {
                Utils.LimparTela();
                TelaBoasVindas(pessoa);
                TelaContaLogada(pessoa);
            }
            else
            {
                MostrarLoginFalhou();
            }
        }
        
        private static void MostrarLoginFalhou()
        {
            Utils.LimparTela();
            Utils.MostrarCabecalho("LOGIN FALHOU");
            Utils.MostrarErro("CPF ou senha incorretos!");
            Console.WriteLine("Verifique se digitou corretamente ou crie uma nova conta.");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            TelaPrincipal();
        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {
            Console.WriteLine();
            Console.WriteLine("  +===============================================+");
            Console.WriteLine("  |              BEM-VINDO!                        |");
            Console.WriteLine("  +===============================================+");
            Console.WriteLine();
            Console.WriteLine($"  Cliente: {pessoa.Nome}");
            Console.WriteLine($"  Banco: {pessoa.Conta.GetCodigoBanco()} - DigiBank");
            Console.WriteLine($"  Agencia: {pessoa.Conta.GetNumeroAgencia()}");
            Console.WriteLine($"  Conta: {pessoa.Conta.GetNumeroConta()}");
            Console.WriteLine($"  Saldo: {Utils.FormatarValor(pessoa.Conta.ConsultaSaldo())}");
            Console.WriteLine();
        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Utils.LimparTela();
            TelaBoasVindas(pessoa);

            Utils.MostrarCabecalho("MENU PRINCIPAL");
            string[] opcoes = {
                "1 - Realizar deposito",
                "2 - Realizar saque", 
                "3 - Consultar saldo",
                "4 - Ver extrato",
                "5 - Transferir para outra conta",
                "6 - Relatorio de saldo",
                "0 - Sair"
            };
            Utils.MostrarMenu(opcoes);

            try
            {
                opcao = Utils.LerOpcao();
            }
            catch (FormatException)
            {
                Utils.MostrarErro("Digite apenas numeros!");
                Utils.Pausar();
                TelaContaLogada(pessoa);
                return;
            }

            switch (opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                    break;
                case 2:
                    TelaSaque(pessoa);
                    break;
                case 3:
                    TelaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaTransferencia(pessoa);
                    break;
                case 6:
                    TelaRelatorioSaldo(pessoa);
                    break;
                case 0:
                    TelaPrincipal();
                    break;
                default:
                    Console.WriteLine("ERRO: Opcao invalida!");
                    Thread.Sleep(2000);
                    TelaContaLogada(pessoa);
                    break;
            }
        }

        private static void TelaDeposito(Pessoa pessoa)
        {
            Utils.LimparTela();
            TelaBoasVindas(pessoa);
            Utils.MostrarCabecalho("DEPOSITO");
            Console.WriteLine();

            double valor = Utils.LerValor("Digite o valor do deposito: R$ ");

            if (!Utils.Confirmar($"Confirma deposito de {Utils.FormatarValor(valor)}?"))
            {
                Console.WriteLine("Deposito cancelado!");
                Utils.Pausar();
                TelaContaLogada(pessoa);
                return;
            }

            Utils.MostrarLoading("Processando deposito");
            pessoa.Conta.Deposita(valor);

            MostrarResultadoOperacao(pessoa, "DEPOSITO REALIZADO!", 
                $"Valor depositado: {Utils.FormatarValor(valor)}");
        }

        private static void MostrarResultadoOperacao(Pessoa pessoa, string titulo, string detalhes)
        {
            Utils.LimparTela();
            TelaBoasVindas(pessoa);
            Utils.MostrarCabecalho(titulo);
            Console.WriteLine(detalhes);
            Console.WriteLine($"Saldo atual: {Utils.FormatarValor(pessoa.Conta.ConsultaSaldo())}");
            Console.WriteLine($"Data/Hora: {Utils.FormatarData(DateTime.Now)}");
            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaSaque(Pessoa pessoa)
        {
            Utils.LimparTela();
            TelaBoasVindas(pessoa);
            Utils.MostrarCabecalho("SAQUE");
            Console.WriteLine();

            double valor = Utils.LerValor("Digite o valor do saque: R$ ");

            if (valor > pessoa.Conta.ConsultaSaldo())
            {
                Utils.MostrarErro("Saldo insuficiente!");
                Console.WriteLine($"Saldo disponivel: {Utils.FormatarValor(pessoa.Conta.ConsultaSaldo())}");
                Console.WriteLine($"Valor solicitado: {Utils.FormatarValor(valor)}");
                Utils.Pausar(3);
                TelaContaLogada(pessoa);
                return;
            }

            if (!Utils.Confirmar($"Confirma saque de {Utils.FormatarValor(valor)}?"))
            {
                Console.WriteLine("Saque cancelado!");
                Utils.Pausar();
                TelaContaLogada(pessoa);
                return;
            }

            Utils.MostrarLoading("Processando saque");
            bool okSaque = pessoa.Conta.Saca(valor);

            if (okSaque)
            {
                MostrarResultadoOperacao(pessoa, "SAQUE REALIZADO!", 
                    $"Valor sacado: {Utils.FormatarValor(valor)}");
            }
            else
            {
                Utils.LimparTela();
                TelaBoasVindas(pessoa);
                Utils.MostrarCabecalho("SAQUE FALHOU");
                Utils.MostrarErro("Saldo insuficiente para realizar o saque!");
                Console.WriteLine($"Saldo disponivel: {Utils.FormatarValor(pessoa.Conta.ConsultaSaldo())}");
            OpcaoVoltarLogado(pessoa);
            }
        }

        private static void TelaSaldo(Pessoa pessoa)
        {
            Utils.LimparTela();
            TelaBoasVindas(pessoa);
            Utils.MostrarCabecalho("CONSULTA SALDO");
            
            Console.WriteLine($"  Saldo atual: {Utils.FormatarValor(pessoa.Conta.ConsultaSaldo())}");
            Console.WriteLine($"  Data/Hora: {Utils.FormatarData(DateTime.Now)}");
            Console.WriteLine();
            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Utils.LimparTela();
            TelaBoasVindas(pessoa);
            Utils.MostrarCabecalho("EXTRATO");

            if(pessoa.Conta.Extrato().Any())
            {
                Console.WriteLine("  +===============================================+");
                Console.WriteLine("  |        HISTORICO DE MOVIMENTACOES           |");
                Console.WriteLine("  +===============================================+");
                Console.WriteLine();

                foreach(Extrato extrato in pessoa.Conta.Extrato().OrderByDescending(e => e.Data))
                {
                    Console.WriteLine($"  Data: {extrato.FormatarData()}");
                    Console.WriteLine($"  Descricao: {extrato.Descricao}");
                    Console.WriteLine($"  Valor: {extrato.FormatarValor()} {extrato.ObterTipoOperacao()}");
                    Console.WriteLine("  " + new string('-', 47));
                }

                Console.WriteLine();
                Console.WriteLine($"  Saldo atual: {Utils.FormatarValor(pessoa.Conta.ConsultaSaldo())}");
                Console.WriteLine($"  Total de movimentacoes: {pessoa.Conta.Extrato().Count}");
            }
            else 
            {
                Console.WriteLine("  +===============================================+");
                Console.WriteLine("  |        NENHUMA MOVIMENTACAO                  |");
                Console.WriteLine("  +===============================================+");
                Console.WriteLine();
                Console.WriteLine("  Nao ha movimentacoes para exibir.");
                Console.WriteLine("  Faca seu primeiro deposito para comecar!");
            }

            Console.WriteLine();
            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaTransferencia(Pessoa pessoa)
        {
            Utils.LimparTela();
            TelaBoasVindas(pessoa);
            Utils.MostrarCabecalho("TRANSFERENCIA");

            string numeroContaDestino = Utils.LerTexto("Digite o numero da conta de destino: ");
            var pessoaDestino = pessoas.FirstOrDefault(p => p.Conta.GetNumeroConta() == numeroContaDestino);
            
            if (pessoaDestino == null)
            {
                Utils.MostrarErro("Conta de destino nao encontrada!");
                Utils.Pausar();
                TelaContaLogada(pessoa);
                return;
            }

            if (pessoaDestino == pessoa)
            {
                Utils.MostrarErro("Nao e possivel transferir para a propria conta!");
                Utils.Pausar();
                TelaContaLogada(pessoa);
                return;
            }

            double valor = Utils.LerValor("Digite o valor da transferencia: R$ ");

            if (valor > pessoa.Conta.ConsultaSaldo())
            {
                Utils.MostrarErro("Saldo insuficiente!");
                Console.WriteLine($"Saldo disponivel: {Utils.FormatarValor(pessoa.Conta.ConsultaSaldo())}");
                Utils.Pausar(3);
                TelaContaLogada(pessoa);
                return;
            }

            if (!Utils.Confirmar($"Confirma transferencia de {Utils.FormatarValor(valor)} para {pessoaDestino.Nome}?"))
            {
                Console.WriteLine("Transferencia cancelada!");
                Utils.Pausar();
                TelaContaLogada(pessoa);
                return;
            }

            Utils.MostrarLoading("Processando transferencia");
            bool sucesso = ((Conta)pessoa.Conta).Transferir((Conta)pessoaDestino.Conta, valor);

            if (sucesso)
            {
                MostrarResultadoOperacao(pessoa, "TRANSFERENCIA REALIZADA!", 
                    $"Valor transferido: {Utils.FormatarValor(valor)}\nDestinatario: {pessoaDestino.Nome}\nConta destino: {pessoaDestino.Conta.GetNumeroConta()}");
            }
            else 
            {
                Utils.LimparTela();
                TelaBoasVindas(pessoa);
                Utils.MostrarCabecalho("TRANSFERENCIA FALHOU");
                Utils.MostrarErro("Transferencia falhou!");
                OpcaoVoltarLogado(pessoa);
            }
        }

        private static void TelaRelatorioSaldo(Pessoa pessoa)
        {
            Utils.LimparTela();
            TelaBoasVindas(pessoa);
            Utils.MostrarCabecalho("RELATORIO DE SALDO");
            
            Console.WriteLine("  +===============================================+");
            Console.WriteLine("  |              DADOS DA CONTA                  |");
            Console.WriteLine("  +===============================================+");
            Console.WriteLine();
            Console.WriteLine($"  Cliente: {pessoa.Nome}");
            Console.WriteLine($"  CPF: {pessoa.CPF}");
            Console.WriteLine($"  Banco: {pessoa.Conta.GetCodigoBanco()} - DigiBank");
            Console.WriteLine($"  Agencia: {pessoa.Conta.GetNumeroAgencia()}");
            Console.WriteLine($"  Conta: {pessoa.Conta.GetNumeroConta()}");
            Console.WriteLine($"  Saldo atual: {Utils.FormatarValor(pessoa.Conta.ConsultaSaldo())}");
            Console.WriteLine($"  Data/Hora: {Utils.FormatarData(DateTime.Now)}");
            Console.WriteLine($"  Total de movimentacoes: {pessoa.Conta.Extrato().Count}");
            Console.WriteLine();

            OpcaoVoltarLogado(pessoa);
        }

        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Utils.MostrarCabecalho("OPCOES");
            string[] opcoes = { "1 - Voltar para minha conta", "0 - Sair do sistema" };
            Utils.MostrarMenu(opcoes);

            try
            {
                opcao = Utils.LerOpcao();
            }
            catch (FormatException)
            {
                Utils.MostrarErro("Digite apenas numeros!");
                Utils.Pausar();
                OpcaoVoltarLogado(pessoa);
                return;
            }

            if (opcao == 1)
                TelaContaLogada(pessoa);
            else if (opcao == 0)
                TelaPrincipal();
            else
            {
                Utils.MostrarErro("Opcao invalida!");
                Utils.Pausar();
                OpcaoVoltarLogado(pessoa);
            }
        }

        private static void OpcaoVoltarDeslogado()
        {
            Utils.MostrarCabecalho("OPCOES");
            string[] opcoes = { "1 - Voltar para o menu principal", "0 - Sair do sistema" };
            Utils.MostrarMenu(opcoes);

            try
            {
                opcao = Utils.LerOpcao();
            }
            catch (FormatException)
            {
                Utils.MostrarErro("Digite apenas numeros!");
                Utils.Pausar();
                OpcaoVoltarDeslogado();
                return;
            }

            if (opcao == 1)
                TelaPrincipal();
            else if (opcao == 0)
            {
                Console.WriteLine();
                Console.WriteLine("  Obrigado por usar o DigiBank!");
                Environment.Exit(0);
            }
            else
            {
                Utils.MostrarErro("Opcao invalida!");
                Utils.Pausar();
                OpcaoVoltarDeslogado();
            }
        }
    }
}
