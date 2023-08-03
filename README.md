# Desafio para Vaga de Desenvolvedor .NET
![SmartOnline Logo](https://www.smartonline.app/logo.a3cd84b4d14610f7.png)

[![NPM](https://img.shields.io/npm/l/react)](https://github.com/YGSilva/Iniflex/blob/master/LICENCE) 

# Descrição do Projeto

O projeto requer o desenvolvimento de uma aplicação web que realize as seguintes tarefas:

1. Aceitar o upload de um arquivo [CNAB](https://github.com/YGSilva/smartOnlineDesafio/blob/master/CNAB.txt) por meio de uma interface intuitiva.
2. Analisar e interpretar os dados do arquivo CNAB, normalizando as informações.
3. Armazenar corretamente as transações financeiras em um banco de dados à sua escolha (pode ser relacional ou NoSQL).
4. Verificar a validade dos números de CPF presentes nos dados.
5. Exibir uma lista de operações importadas por loja, incluindo um totalizador de saldo em conta.
6. Apresentar uma lista das operações que não foram processadas corretamente.
7. A aplicação deve ser desenvolvida em .NET, preferencialmente utilizando versões recentes como .NET Core ou ASP.NET MVC.
8. O processo de configuração e execução da aplicação deve ser simples e utilizar apenas tecnologias e bibliotecas de código aberto.
9. O banco de dados pode ser PostgreSQL, MySQL, SQL Server ou MongoDB (NoSQL, opcional).
10. Um arquivo README deve acompanhar o projeto, fornecendo instruções claras para configuração e execução, além de informações sobre como consumir a API.

## Observações adicionais sobre o projeto

1. API Documentada.
2. Inclusão de teste unitários.
3. Foi utilizado docker-compose na aplicação e no banco de daodos.
4. Tambem foi incluido dentro do docker-compose uma imagem do pgadmin, assim podendo averiguar o armazenamento de dados na tabela em tempo real, o link para o acesso é (http://localhost:15440/), na pagina de longin user o Email: teste@gmail.com e a Senha: pgadmin.

## Tecnologias utilizadas
- .Net6
- Entity Framework
- Swagger
- PostgresSQL
- Docker

## NuGets Packages utilizado
- Microsoft.AspNetCore.Mvc.NewtonsoftJson v6.0.12
- Microsoft.EntityFrameworkCore v7.0.9
- Microsoft.EntityFrameworkCore.Design v7.0.9
- Microsoft.EntityFrameworkCore.Relational.Design v1.1.6
- Microsoft.EntityFrameworkCore.Tools v7.0.9
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets v1.15.1
- Npgsql.EntityFrameworkCore.PostgreSQL v7.0.4
- Swashbuckle.AspNetCore v6.5.0
- Swashbuckle.AspNetCore.Annotations v6.5.0

## EndPoints
- [POST] ProcessFile -> Enpoint onde será feita o update do arquivo [CNAB](https://github.com/YGSilva/smartOnlineDesafio/blob/master/CNAB.txt), nessa tela após a abertura do Swagger e de expandir o andpoint, você irá selecionar a opção "Try it out", na sequência ira aparecer a opção de upload do arquivo, com o arquivo selecionado a opção "Execute" deverá ser acionada, no "Response body" ira aparecer todos os dados do arquivo já formatos.
- [GET] ListWithTotalBalance -> Endpoint onde será feito o retorno dos dados importados por loja e agrupados pelo tipo de operação juntamente com o somatorio total do valor, como CPF e data serão validados nem todos os dados inputados serão demonstrados, pois não foram processados corretamente.
- [GET] OperationsWrong -> Endpoint onde será demonstrado os dados que não foram processados corretamente, CPF ou data que vieram no arquivo não estavam corretos.
- [DELETE] DeleteAllData -> Endpoint responsável por limpar toda a base de dados, tomei a liberdade de coloca-ló para fazer teste com somente os 21 dados passados no upload, após algumas inserções muitos dados eram mostrado, assim alterando no resultado final dos métodos GET
## Avaliação
Yago Gonçalves da Sivla