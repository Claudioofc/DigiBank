using DigiBank.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
        public IConta Conta { get; set; }

        public void SetNome(string nome)
        {
            this.Nome = nome;
        }

        public void SetCPF(string cpf)
        {
            this.CPF = cpf;
        }

        public void SetSenha(string senha)
        {
            this.Senha = Utils.CriptografarSenha(senha);
        }
        
        public bool VerificarSenha(string senha)
        {
            return Utils.VerificarSenha(senha, this.Senha);
        }
    }
}
