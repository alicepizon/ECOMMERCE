# 🛒 Projeto E-commerce Backend - AV2

Este repositório contém a implementação de uma API RESTful para um sistema de E-commerce, desenvolvida em **C# .NET 9.0**.

O projeto foi construído seguindo rigorosamente os princípios de **DDD (Domain-Driven Design)**, **SOLID** e **Orientação a Objetos**, com foco em encapsulamento, polimorfismo e tratamento de exceções.

---

## 👥 Integrantes do Grupo

* **Alice Maximo** (Matrícula: 06012338)

---


## 🚀 Como Rodar o Projeto

### Pré-requisitos
* .NET SDK (Versão 8.0 ou 9.0) instalado.

### Passo a Passo Simplificado

1.  Abra o terminal na pasta raiz do projeto.
2.  Execute a aplicação com o comando:
    ```bash
    dotnet run --project MinhaAPI
    ```
3.  Aguarde a mensagem `Now listening on: http://localhost:XXXX` no terminal.

---

## 🧪 Como Testar a API

Você tem duas opções para validar o funcionamento do sistema:

### Opção A: Postman (Recomendado) 🏆
Incluímos um arquivo de coleção pronto para uso.
1.  No Postman, clique em **Import**.
2.  Selecione o arquivo `Ecommerce_AV2.postman_collection.json` localizado na raiz deste projeto.
3.  A coleção já contém todas as requisições organizadas (Usuário, Produto, Carrinho, Pedido) e prontas para executar.

### Opção B: Swagger (Navegador)
Acesse a documentação interativa gerada automaticamente:
* **Link:** `http://localhost:XXXX/swagger` (Substitua XXXX pela porta exibida no seu terminal, ex: 5021).

---

## 🏆 Justificativa dos Critérios de Avaliação (AV2)

### 1. Modelagem de Classes e Domínio
* **Implementação:** Classes `Produto`, `Carrinho`, `CarrinhoItem`, `Pedido` e `Usuario` implementadas.
* **Destaque:** A classe `Carrinho` possui uma relação forte de composição com `CarrinhoItem`, gerenciando a lista internamente (`private readonly List`).

### 2. Herança e Polimorfismo
* **Implementação:** Classe abstrata `Pagamento` e derivadas `PagamentoPix` / `PagamentoCartao`.
* **Destaque:** No `PedidoService`, utilizamos um **Factory Method** para instanciar a estratégia de pagamento e executamos o método `.Processar()` de forma polimórfica.

### 3. Encapsulamento e Coesão
* **Implementação:** Uso estrito de `private set` em todas as propriedades.
* **Destaque:** Regras de negócio (como baixar estoque ou calcular total) estão dentro das Entidades, não espalhadas pelo código.

### 4. Tratamento de Exceções
* **Implementação:** Blocos `try/catch` nos Controllers e `throw` nas Entidades.
* **Destaque:** Uso de exceções semânticas (`ArgumentException` para validação, `InvalidOperationException` para regras de estado) retornando HTTP 400 ou 409.

### 5. Arquitetura (DTO, Service, Repository)
* **Implementação:** Separação física em pastas/projetos (`API`, `Application`, `Domain`, `Infrastructure`).
* **Destaque:** Uso de DTOs para blindar o domínio e Repositórios para abstrair a persistência (simulada em memória com Singleton).

---

## 📊 Diagrama de Classes

O diagrama UML completo, contendo a visibilidade dos métodos, tipos e relacionamentos, encontra-se no arquivo anexo: **`diagrama.mmd`** (ou visualize na imagem incluída no repositório).

---

## 🔄 Roteiro de Teste (Fluxo Feliz)

Para verificar a integridade do sistema, siga esta ordem de execução:

1.  **Registrar Usuário:** Crie um cliente e copie o `id` gerado.
2.  **Criar Categoria:** Crie uma categoria (ex: "Eletrônicos") e copie o `id`.
3.  **Cadastrar Produto:** Use o ID da categoria e defina um estoque inicial (ex: 10). Copie o `id` do produto.
4.  **Adicionar ao Carrinho:** Use os IDs de usuário e produto.
5.  **Visualizar Carrinho:** Confirme se o valor total foi calculado corretamente.
6.  **Finalizar Pedido:** Escolha o método de pagamento ("pix" ou "cartao").
7.  **Validar Estoque:** Liste os produtos novamente e verifique se o estoque foi debitado automaticamente.           
