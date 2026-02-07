# 📌 Sistema de Cadastro de Pedidos — Teste Técnico

Aplicação desenvolvida em **C# WPF (.NET Framework 4.7.2)** utilizando o padrão **MVVM**, persistência em **JSON** e filtros com **LINQ**, conforme solicitado no teste técnico.

---

# ✅ Funcionalidades Implementadas

## 👤 Cadastro de Pessoas

- CRUD completo (Incluir, Editar, Excluir)
- Campos:
  - Nome (obrigatório)
  - CPF (obrigatório)
  - Endereço (opcional)
- Filtro de busca utilizando LINQ (Nome ou CPF)
- Botão **Incluir Pedido** conforme especificação do teste

---

## 📦 Cadastro de Produtos

- CRUD completo
- Campos:
  - Nome
  - Código
  - Valor
- Filtro LINQ por:
  - Nome
  - Código
  - Faixa de valor (mínimo e máximo)

---

## 🛒 Cadastro de Pedidos

- Pedido iniciado a partir da tela de Pessoas
- Seleção de múltiplos produtos
- Produto repetido soma quantidade automaticamente
- Valor total calculado automaticamente
- Status inicial automático: **Pendente**
- Pedido pode ser finalizado, bloqueando alterações

---

## 📋 Consulta e Status de Pedidos

- Tela "Pedidos" no menu principal
- Lista todos os pedidos cadastrados
- Alteração de status somente após finalização:

Status disponíveis:

- Pago
- Enviado
- Recebido

---

# 💾 Persistência de Dados

Os dados são armazenados em arquivos JSON localizados na pasta:

