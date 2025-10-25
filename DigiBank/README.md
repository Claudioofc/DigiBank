# DigiBank - Sistema Bancário Digital

Sistema bancário desenvolvido em C# .NET 6.0 que simula operações bancárias básicas através de interface de console.

## Funcionalidades

- Criacao de contas bancarias
- Login com CPF e senha
- Depositos e saques
- Consulta de saldo
- Extrato de movimentacoes
- Transferencias entre contas
- Relatorio de saldo
- Interface azul profissional

## Tecnologias Utilizadas

- C# .NET 6.0
- Console Application
- Programacao Orientada a Objetos
- Interfaces e Heranca
- Criptografia de senhas (Base64)
- Validacao de CPF

## Habilidades Tecnicas Demonstradas

### Programacao Orientada a Objetos
- **Abstracao**: Classes abstratas (Banco, Conta)
- **Heranca**: ContaCorrente herda de Conta
- **Polimorfismo**: Interface IConta implementada
- **Encapsulamento**: Propriedades privadas e metodos publicos

### Estruturas de Dados
- **Listas**: List<Pessoa>, List<Extrato>
- **LINQ**: Consultas e filtros de dados
- **Collections**: Manipulacao de colecoes

### Validacoes e Seguranca
- **Validacao de CPF**: Algoritmo completo de verificacao
- **Criptografia**: Codificacao Base64 para senhas
- **Tratamento de Erros**: Try-catch em operacoes criticas
- **Validacao de Entrada**: Verificacao de dados do usuario

### Padroes de Desenvolvimento
- **DRY (Don't Repeat Yourself)**: Metodos utilitarios centralizados
- **Single Responsibility**: Cada classe tem uma responsabilidade
- **Interface Segregation**: Interface IConta bem definida
- **Dependency Inversion**: Uso de interfaces

### Programacao Funcional
- **Lambda Expressions**: Consultas LINQ
- **Delegates**: Metodos de callback
- **Extension Methods**: Metodos de extensao

### Gerenciamento de Estado
- **Singleton Pattern**: Lista global de pessoas
- **State Management**: Controle de fluxo da aplicacao
- **Session Management**: Controle de usuario logado

### Interface e UX
- **Console UI**: Interface de linha de comando
- **Color Management**: Controle de cores do terminal
- **Input Validation**: Validacao de entrada do usuario
- **Error Handling**: Tratamento amigavel de erros

### Arquitetura de Software
- **Separation of Concerns**: Separacao de responsabilidades
- **Layered Architecture**: Camadas bem definidas
- **Clean Code**: Codigo limpo e legivel
- **Modular Design**: Componentes independentes

## Estrutura do Projeto

```
DigiBank/
├── Classes/
│   ├── Banco.cs          # Classe abstrata base
│   ├── Conta.cs          # Classe abstrata de conta
│   ├── ContaCorrente.cs  # Implementacao de conta corrente
│   ├── Pessoa.cs         # Cliente do banco
│   ├── Extrato.cs        # Movimentacoes financeiras
│   ├── Layout.cs         # Interface do usuario
│   └── Utils.cs          # Utilitarios e validacoes
├── Contratos/
│   └── IConta.cs         # Interface de operacoes bancarias
└── Program.cs            # Ponto de entrada
```

## Como Executar

### Pre-requisitos
- .NET 6.0 SDK instalado
- Terminal ou prompt de comando

### Passos para Execucao

1. Clone ou baixe o projeto
2. Abra o terminal na pasta do projeto
3. Execute o comando:
```bash
dotnet run
```

### Navegacao no Sistema

1. **Tela Principal**: Escolha entre criar conta, fazer login ou sair
2. **Criar Conta**: Informe nome, CPF e senha
3. **Login**: Digite CPF e senha cadastrados
4. **Menu Principal**: Acesse todas as operacoes bancarias

## Operacoes Disponiveis

### Criacao de Conta
- Nome completo
- CPF (validado automaticamente)
- Senha (criptografada)

### Operacoes Bancarias
- **Deposito**: Adiciona valor ao saldo
- **Saque**: Remove valor do saldo (verifica disponibilidade)
- **Saldo**: Consulta valor atual
- **Extrato**: Historico de movimentacoes
- **Transferencia**: Envia dinheiro para outra conta
- **Relatorio**: Resumo completo da conta

## Caracteristicas Tecnicas

### Validacoes Implementadas
- CPF: Algoritmo completo de validacao
- Valores: Apenas numeros positivos
- Senhas: Criptografia Base64
- Entrada: Tratamento de erros robusto

### Seguranca
- Senhas criptografadas
- Validacao de CPF
- Verificacao de saldo antes de saques
- Confirmacao para operacoes criticas

### Interface
- Design azul profissional
- Navegacao intuitiva
- Mensagens claras
- Tratamento de erros amigavel

## Estrutura de Classes

### Banco (Classe Abstrata)
- Codigo do banco
- Nome da instituicao

### Conta (Classe Abstrata)
- Saldo
- Numero da agencia
- Numero da conta
- Lista de movimentacoes
- Operacoes basicas (depositar, sacar, consultar)

### Pessoa
- Nome
- CPF
- Senha (criptografada)
- Conta associada

### Extrato
- Data e hora da operacao
- Descricao da movimentacao
- Valor (positivo ou negativo)
- Metodos de formatacao

## Padroes de Desenvolvimento

### DRY (Don't Repeat Yourself)
- Metodos utilitarios centralizados
- Reutilizacao de codigo
- Interface consistente

### Tratamento de Erros
- Try-catch em operacoes criticas
- Validacao de entrada
- Mensagens de erro claras

### Interface Limpa
- Sem caracteres especiais
- Layout profissional
- Navegacao fluida

## Exemplo de Uso

1. Execute o programa
2. Escolha "1 - Criar nova conta"
3. Preencha os dados solicitados
4. Faça login com CPF e senha
5. Acesse o menu de operacoes
6. Realize depositos, saques ou consultas
7. Visualize o extrato das movimentacoes

## Funcionalidades Avancadas

### Transferencias
- Envio de dinheiro entre contas
- Verificacao de saldo
- Confirmacao obrigatoria
- Registro em ambas as contas

### Relatorio de Saldo
- Dados completos da conta
- Informacoes do cliente
- Saldo atual
- Total de movimentacoes

### Extrato Detalhado
- Historico cronologico
- Valores formatados
- Tipos de operacao
- Data e hora precisas

## Desenvolvimento

### Adicionando Novas Funcionalidades
1. Estenda a interface IConta
2. Implemente na classe Conta
3. Adicione interface no Layout
4. Teste todas as operacoes

### Manutencao
- Codigo limpo e documentado
- Estrutura modular
- Facil manutencao
- Testes de validacao

## Consideracoes Finais

Este sistema demonstra conceitos fundamentais de programacao orientada a objetos, validacao de dados, criptografia basica e desenvolvimento de interfaces de console. O codigo foi desenvolvido seguindo boas praticas de programacao e esta pronto para uso educacional ou como base para sistemas mais complexos.

Para duvidas ou sugestoes, consulte o codigo fonte que esta bem estruturado e comentado para facilitar o entendimento.
