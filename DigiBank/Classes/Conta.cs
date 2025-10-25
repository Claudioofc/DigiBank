using DigiBank.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public abstract class Conta : Banco, IConta
    {
        public Conta()
        {
            this.NumeroAgencia = "0001";
            Conta.NumeroDaContaSequencial++;
            this.Movimentacoes = new List<Extrato>();
        }

        public double Saldo { get; protected set; }
        public string NumeroAgencia { get; private set; }
        public string NumeroConta { get; protected set; }
        public static int NumeroDaContaSequencial { get; private set; }
        private List<Extrato> Movimentacoes;

        public double ConsultaSaldo()
        {
            return this.Saldo;
        }

        public void Deposita(double valor)
        {
            DateTime dataAtual = DateTime.Now;
            this.Movimentacoes.Add(new Extrato(dataAtual, "Deposito", valor));
            this.Saldo += valor;
        }

        public bool Saca(double valor)
        {
            if (valor > this.ConsultaSaldo())
                return false;

            DateTime dataAtual = DateTime.Now;
            this.Movimentacoes.Add(new Extrato(dataAtual, "Saque", -valor));

            this.Saldo -= valor;
            return true;
        }

        public string GetCodigoBanco()
        {
            return this.CodigoDoBanco;
        }

        public string GetNumeroAgencia()
        {
            return this.NumeroAgencia;
        }

        public string GetNumeroConta()
        {
            return this.NumeroConta;
        }

        public List<Extrato> Extrato()
        {
            return this.Movimentacoes;
        }
        
        public bool Transferir(Conta contaDestino, double valor)
        {
            if (valor <= 0)
                return false;
                
            if (this.Saca(valor))
            {
                contaDestino.Deposita(valor);
                
                DateTime dataAtual = DateTime.Now;
                this.Movimentacoes.Add(new Extrato(dataAtual, $"Transferência para {contaDestino.NumeroConta}", -valor));
                contaDestino.Movimentacoes.Add(new Extrato(dataAtual, $"Transferência de {this.NumeroConta}", valor));
                
                return true;
            }
            return false;
        }
        
        public List<Extrato> ObterExtrato(DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            var extrato = this.Movimentacoes.AsQueryable();
            
            if (dataInicio.HasValue)
                extrato = extrato.Where(e => e.Data >= dataInicio.Value);
                
            if (dataFim.HasValue)
                extrato = extrato.Where(e => e.Data <= dataFim.Value);
            
            return extrato.OrderByDescending(e => e.Data).ToList();
        }
    }
}
