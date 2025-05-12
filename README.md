# Hackaton - Fiap.Health.Med.User.Manager

Projeto criado pelo **Grupo 15** do curso de **Arquitetura de Sistemas .NET com Azure** da Fiap para atender o Hackaton.

> O User Manager tem como função realizar todos os processos referente ao contexto de usuários(médico e paciente), sendo um serviço separado e exclusivo para essas funções


## Autores

- Grupo 15

|Integrantes
|--|
| Caio Vinícius Moura Santos Maia |
| Evandro Prates Silva |
| Guilherme Castro Batista Pereira |
| Luis Gustavo Gonçalves Reimberg |


## Stack utilizada

|Tecnologia utilizada|
|--|
|.Net 8|
|Docker|
|FluentValidation|
|BCypt|
|Swagger|
|XUnit|
|Moq|
|Dapper|
|FluentMigrator|


## Funcionalidades

- Ações de médico(listar, filtrar, criar, excluir, atualizar, buscar por uf+crm)
- Ações de paciente(listar, criar, excluir, atualizar, buscar por documento)


## Build do projeto
Vá até o diretório da solução e execute o seguinte comando para realizar o build da imagem docker a ser utilizada.

``` shell
    docker build -f ./infrastructure/docker/api/Dockerfile -t schedule-app .
```

Ou se estiver na pasta de infra

``` shell
    docker build -f ../fiap-health-med-user-manager/infrastructure/docker/api/Dockerfile -t user-app ../fiap-health-med-user-manager/
```